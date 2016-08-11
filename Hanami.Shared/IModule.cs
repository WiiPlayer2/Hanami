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

        void Start();
        void Stop();
        void Reload();
    }
}