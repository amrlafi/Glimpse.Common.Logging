using System;

using Common.Logging;

namespace Glimpse.Common.Logging
{
    public class CommonLoggingMessage
    {
        public string Logger { get; set; }
        public LogLevel Level { set; get; }
        public string Message { get; set; }
        public TimeSpan FromFirst { get; set; }
        public TimeSpan FromLast { get; set; }
    }
}