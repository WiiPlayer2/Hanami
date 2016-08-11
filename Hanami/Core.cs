using Hanami.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanami
{
    class Core : ICore
    {
        public IDatabase Database
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AddModule(IModule module)
        {
            throw new NotImplementedException();
        }

        public IModule GetModule(string module)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModule> GetModules()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModule> GetModules(string plugin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPlugins()
        {
            throw new NotImplementedException();
        }
    }
}
