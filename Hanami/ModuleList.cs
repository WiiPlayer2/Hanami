using Hanami.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Hanami
{
    class ModuleList : IEnumerable<IModule>
    {
        private Dictionary<string, IModule> modules;

        public ModuleList()
        {
            modules = new Dictionary<string, IModule>();
        }

        public void AddModule(string plugin, IModule module)
        {
            var id = Helper.CombineIdentifier(plugin, module.IdentifiableName);
            if (modules.ContainsKey(id))
            {
                throw new InvalidOperationException(string.Format("Module '{0}' is already in the list.", id));
            }
            modules[id] = module;
        }

        public IEnumerable<string> GetPlugins()
        {
            return modules.Keys.Select(o =>
            {
                var plugin = "";
                var module = "";
                Helper.CheckModuleIdentifier(o, out plugin, out module);
                return plugin;
            })
                .Distinct();
        }

        public IModule this[string id]
        {
            get
            {
                return modules[id.ToLower()];
            }
        }

        public IEnumerator<IModule> GetEnumerator()
        {
            return modules.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
