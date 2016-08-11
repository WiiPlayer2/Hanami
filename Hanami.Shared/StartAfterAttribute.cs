using System;

namespace Hanami.Shared
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class StartAfterAttribute : Attribute
    {
        public StartAfterAttribute(string module)
        {
            Module = module.ToLower();
        }

        public string Module { get; private set; }
    }
}