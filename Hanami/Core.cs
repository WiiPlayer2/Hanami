using Hanami.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Hanami
{
    class Core : ICore
    {
        private ModuleList<IModule> modules;
        private ModuleList<ICheckProvider> checkProviders;
        private Configuration config;
        private ModuleManager moduleManager;
        private Timer checkTimer;

        //Values from config;
        private string pluginsPath;
        private string configsPath;
        private int checkInterval;

        public Core()
        {
            modules = new ModuleList<IModule>();
            checkProviders = new ModuleList<ICheckProvider>();

            config = LoadConfiguration("core.cfg");
            pluginsPath = config.Ensure("plugins_path", "./plugins");
            configsPath = config.Ensure("configs_path", "./configs");
            checkInterval = config.Ensure("check_interval", 5, o => o > 0);
            SaveConfiguration("core.cfg", config);
        }

        #region Interface Methods
        public IDatabase Database { get; private set; }

        public void AddModule(IModule module)
        {
            if (module is IDatabase)
            {
                if (Database == null)
                {
                    Database = module as IDatabase;
                }
                else
                {
                    throw new InvalidOperationException("Database cannot be set twice.");
                }
            }

            if (module is ICheckProvider)
            {
                checkProviders.AddModule(module as ICheckProvider);
                (module as ICheckProvider).CheckCompleted += Core_CheckCompleted;
            }

            modules.AddModule(module);
        }

        public IModule GetModule(string module)
        {
            return modules[module.ToLower()];
        }

        public IEnumerable<IModule> GetModules()
        {
            return modules;
        }

        public IEnumerable<IModule> GetModules(string plugin)
        {
            return modules.Where(o => o.Plugin.IdentifiableName.ToLower() == plugin.ToLower());
        }

        public IEnumerable<string> GetPlugins()
        {
            return modules.GetPlugins();
        }
        #endregion

        private Configuration LoadConfiguration(string path)
        {
            var stream = default(Stream);
            EnsureDirectory(Path.GetDirectoryName(path));
            if (File.Exists(path))
            {
                stream = new FileStream(path, FileMode.Open);
            }
            else
            {
                stream = Stream.Null;
            }
            var config = new Configuration(stream);
            stream.Close();
            return config;
        }

        private void SaveConfiguration(string path, Configuration config)
        {
            var stream = new FileStream(path, FileMode.Create);
            config.Save(stream);
            stream.Close();
        }

        private void LoadPlugin(Type pluginType)
        {
            try
            {
                var plugin = Activator.CreateInstance(pluginType) as IPlugin;
                plugin.Load(this);
            }
            catch (Exception) { }
        }

        private void EnsureDirectory(string path)
        {
            if (path == "")
            {
                return;
            }
            var parent = Path.GetDirectoryName(path);
            if (!Directory.Exists(parent))
            {
                EnsureDirectory(parent);
            }
            Directory.CreateDirectory(path);
        }

        public void Start()
        {
            EnsureDirectory(pluginsPath);
            EnsureDirectory(configsPath);

            foreach (var assFile in Directory.GetFiles(pluginsPath))
            {
                var ass = Assembly.LoadFrom(assFile);
                foreach (var pluginType in ass.ExportedTypes
                    .Where(o => typeof(IPlugin).IsAssignableFrom(o)))
                {
                    LoadPlugin(pluginType);
                }
            }

            moduleManager = new ModuleManager(modules);
            foreach (var m in modules)
            {
                moduleManager.Start(Helper.CombineIdentifier(m), o =>
                {
                    return LoadConfiguration(Path.Combine(pluginsPath, o.Plugin.IdentifiableName, o.IdentifiableName));
                });
            }

            if (Database == null || Database.State != ModuleState.Started)
            {
                throw new InvalidOperationException("Database is not initialized");
            }

            checkTimer = new Timer(checkInterval * 1000 * 60);
            checkTimer.Elapsed += CheckTimer_Elapsed;
            checkTimer.Start();
        }

        private void CheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var check in checkProviders
                .Where(o => o.State == ModuleState.Started))
            {
                try
                {
                    check.Check();
                }
                catch { }
            }
        }

        private void Core_CheckCompleted(object sender, CheckData checkData)
        {
            var service = Database.GetService(checkData.Host, checkData.Service);
            if (service != null)
            {
                Database.AddData(checkData);
                service.LastData = checkData;
                Database.UpdateService(service);
            }
        }
    }
}
