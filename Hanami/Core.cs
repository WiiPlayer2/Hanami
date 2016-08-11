using Hanami.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanami
{
    class Core : ICore
    {
        private ModuleList<IModule> modules;
        private ModuleList<ICheckProvider> checkProviders;
        private Configuration config;

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
            var stream = new FileStream(path, FileMode.CreateNew);
            config.Save(stream);
            stream.Close();
        }

        public void Start()
        {
        }
    }
}
