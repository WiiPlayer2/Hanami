using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public struct CheckData
    {
        public string Host { get; }

        public string Service { get; }

        public double? PerformanceData { get; }

        public string Message { get; }

        public CheckState State { get; }

        public DateTime Timestamp
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public static CheckData CreateData(string host, string service, CheckState state = CheckState.Unknown, double? perfData = null, string message = null)
        {
            throw new System.NotImplementedException();
        }
    }
}