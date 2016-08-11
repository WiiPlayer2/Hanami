using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public class Host
    {
        public Host(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}