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

        public static bool CheckModuleIdentifier(string identifier, out string plugin, out string module)
        {
            plugin = null;
            module = null;

            if (!identifier.Contains('.'))
            {
                return false;
            }

            var splits = identifier.Split('.');
            if (splits.Length != 2)
            {
                return false;
            }

            if (!CheckIdentifier(splits[0])
                || CheckIdentifier(splits[1]))
            {
                return false;
            }

            plugin = splits[0].ToLower();
            module = splits[1].ToLower();
            return true;
        }

        public static string CombineIdentifier(string plugin, string module)
        {
            return string.Format("{0}.{1}", plugin.ToLower(), module.ToLower());
        }

        public static string CombineIdentifier(IModule module)
        {
            return CombineIdentifier(module.Plugin.IdentifiableName, module.IdentifiableName);
        }
    }
}