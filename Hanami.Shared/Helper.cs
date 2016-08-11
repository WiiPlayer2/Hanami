using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hanami.Shared
{
    public static class Helper
    {
        private static Regex identifierRegex = new Regex(@"^[a-z_]+[a-z0-9_]*$");

        public static bool CheckIdentifier(string identifier)
        {
            return identifierRegex.IsMatch(identifier);
        }
    }
}