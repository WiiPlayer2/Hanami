using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public interface IModule
    {
        string FriendlyName { get; }
        string IdentifiableName { get; }
        IPlugin Plugin { get; }
        ModuleState State { get; set; }
        string ErrorMessage { get; set; }

        void Start(Configuration config);
        void Stop();
        void Reload(Configuration config);
    }
}