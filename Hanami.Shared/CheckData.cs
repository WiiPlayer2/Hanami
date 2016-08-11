using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public struct CheckData
    {
        private CheckData(string host, string service, CheckState state, double? perfData, string message, DateTime timestamp)
        {
            Host = host;
            Service = service;
            State = state;
            PerformanceData = perfData;
            Message = message;
            Timestamp = timestamp;
        }

        public string Host { get; }

        public string Service { get; }

        public double? PerformanceData { get; }

        public string Message { get; }

        public CheckState State { get; }

        public DateTime Timestamp { get; }

        public static CheckData CreateData(string host, string service, CheckState state = CheckState.Unknown, double? perfData = null, string message = null)
        {
            return new CheckData(host, service, state, perfData, message, DateTime.Now);
        }
    }
}