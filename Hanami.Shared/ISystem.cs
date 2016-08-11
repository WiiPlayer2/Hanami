using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public interface ISystem
    {
        void AddModule(Hanami.Shared.IModule provider);
    }
}