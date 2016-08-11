using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public interface IPlugin : IModule
    {
        void Load(ISystem system);
    }
}