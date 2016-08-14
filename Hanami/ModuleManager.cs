using Hanami.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanami
{
    class ModuleManager
    {
        class ModInfo
        {
            public ModInfo()
            {
                DependsOn = new List<ModInfo>();
                IsNeededBy = new List<ModInfo>();
            }

            public IModule Module { get; set; }
            public List<ModInfo> DependsOn { get; set; }
            public List<ModInfo> IsNeededBy { get; set; }
        }

        private Dictionary<string, ModInfo> mods;

        public ModuleManager(ModuleList<IModule> moduleList)
        {
            Modules = moduleList;

            mods = Modules.ToDictionary(o => Helper.CombineIdentifier(o), o => new ModInfo { Module = o });
            foreach (var m in mods)
            {
                var depends = GetDependencyHints(m.Value.Module).Where(o => mods.ContainsKey(o)).Concat(GetDependencies(m.Value.Module));
                foreach (var h in depends)
                {
                    var depMod = mods[h];
                    m.Value.DependsOn.Add(depMod);
                    depMod.IsNeededBy.Add(m.Value);
                }
            }
            foreach (var m in mods)
            {
                m.Value.DependsOn = m.Value.DependsOn.Distinct().ToList();
                m.Value.IsNeededBy = m.Value.IsNeededBy.Distinct().ToList();
            }
        }

        private IEnumerable<string> GetDependencies(IModule mod)
        {
            var type = mod.GetType();
            var atts = type.GetCustomAttributes(typeof(DependsOnAttribute), true);
            return atts.Cast<DependsOnAttribute>().Select(o => o.Module.ToLower());
        }

        private IEnumerable<string> GetDependencyHints(IModule mod)
        {
            var type = mod.GetType();
            var atts = type.GetCustomAttributes(typeof(StartAfterAttribute), true);
            return atts.Cast<StartAfterAttribute>().Select(o => o.Module.ToLower());
        }

        public ModuleList<IModule> Modules { get; private set; }

        public void Start(string module, Func<IModule, Configuration> getConfig)
        {
            Start(mods[module], getConfig);
        }

        private void Start(ModInfo module, Func<IModule, Configuration> getConfig)
        {
            if (module.Module.State == ModuleState.Started)
            {
                return;
            }
            foreach (var m in module.DependsOn
                .Where(o => o.Module.State != ModuleState.Started))
            {
                Start(m, getConfig);
            }
            module.Module.Start(getConfig(module.Module));
        }

        public void Stop(string module)
        {
            Stop(mods[module]);
        }

        private void Stop(ModInfo module)
        {
            if (module.Module.State == ModuleState.Stopped)
            {
                return;
            }
            foreach (var m in module.IsNeededBy
                .Where(o => o.Module.State != ModuleState.Stopped))
            {
                Stop(m);
            }
            module.Module.Stop();
        }
    }
}
