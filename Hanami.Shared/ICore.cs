using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public interface ICore
    {
        IDatabase Database { get; }

        void AddModule(Hanami.Shared.IModule provider);
        IEnumerable<IModule> GetModules();
        IEnumerable<string> GetPlugins();
        IEnumerable<IModule> GetModules(string plugin);
        IModule GetModule(string module);
    }
}