using Hanami.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Hanami
{
    class ModuleList<T> : IEnumerable<T>
        where T : IModule
    {
        private Dictionary<string, T> modules;

        public ModuleList()
        {
            modules = new Dictionary<string, T>();
        }

        public void AddModule(T module)
        {
            var id = Helper.CombineIdentifier(module.Plugin.IdentifiableName, module.IdentifiableName);
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

        public T this[string id]
        {
            get
            {
                return modules[id.ToLower()];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return modules.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
