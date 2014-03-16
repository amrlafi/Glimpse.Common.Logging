using System;

using Common.Logging;
using Common.Logging.Simple;
using Common.Logging.Configuration;

namespace Glimpse.Common.Logging
{
    public class GlimpseLoggerFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {

        #region Ctors

        public GlimpseLoggerFactoryAdapter() : base(null)
        {
            
        }

        public GlimpseLoggerFactoryAdapter(NameValueCollection properties) : base(properties)
        {
        }

        public GlimpseLoggerFactoryAdapter(LogLevel level, bool showDateTime, bool showLogName, bool showLevel, string dateTimeFormat) : base(level, showDateTime, showLogName, showLevel, dateTimeFormat)
        {
        }

        #endregion


        #region AbstractCachingLoggerFactoryAdapter implementation


        #endregion

        protected override ILog CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
        {
            var log = new GlimpseSimpleLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat);
            return log;
        }
    }
}
