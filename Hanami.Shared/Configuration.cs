using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Hanami.Shared
{
    public class Configuration
    {
        private Dictionary<string, IConvertible> values;

        public Configuration(Stream stream)
        {
            values = new Dictionary<string, IConvertible>();

            var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                var splitIndex = line.IndexOf('=');
                var name = line.Substring(0, splitIndex).Trim();
                var val = line.Substring(splitIndex + 1).Trim();

                values[name] = val;
            }
        }

        public T Get<T>(string name)
            where T : IConvertible
        {
            return (T)values[name].ToType(typeof(T), CultureInfo.InvariantCulture);
        }

        public T Ensure<T>(string name, T defaultValue)
            where T : IConvertible
        {
            return Ensure<T>(name, defaultValue, _ => true);
        }

        public T Ensure<T>(string name, T defaultValue, Func<T, bool> checkRange)
            where T : IConvertible
        {
            if (values.ContainsKey(name) && checkRange(Get<T>(name)))
            {
                return Get<T>(name);
            }
            values[name] = defaultValue;
            return defaultValue;
        }

        public void Set<T>(string name, T value)
            where T : IConvertible
        {
            values[name] = value;
        }

        public void Save(Stream stream)
        {
            var writer = new StreamWriter(stream);
            foreach (var kv in values)
            {
                writer.WriteLine("{0} = {1}", kv.Key, kv.Value);
            }
        }
    }
}