﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public enum CheckState
    {
        Ok = 0,
        Warning = 1,
        Critical = 2,
        Error = 3,
        Unknown = 4
    }
}