﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hanami
{
    class Program
    {
        static void Main(string[] args)
        {
            var core = new Core();
            core.Start();
            Thread.Sleep(-1);
        }
    }
}
