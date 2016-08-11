using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(string module)
        {
            Module = module;
        }

        public string Module { get; private set; }
    }
}