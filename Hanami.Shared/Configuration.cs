using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public class Configuration
    {
        public Configuration(System.IO.Stream stream)
        {
            throw new System.NotImplementedException();
        }

        public T Get<T>(string name)
            where T : IConvertible
        {
            throw new System.NotImplementedException();
        }

        public T Ensure<T>(string name, T defaultValue)
            where T : IConvertible
        {
            throw new System.NotImplementedException();
        }

        public T Ensure<T>(string name, T defaultValue, Func<T, bool> checkRange)
            where T : IConvertible
        {
            throw new System.NotImplementedException();
        }

        public void Set<T>(string name, T value)
            where T : IConvertible
        {
            throw new System.NotImplementedException();
        }

        public void Save(System.IO.Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}