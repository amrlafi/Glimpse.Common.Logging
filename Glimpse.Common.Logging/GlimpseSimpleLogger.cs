using System;
using System.Diagnostics;

using Common.Logging;
using Common.Logging.Simple;
using Glimpse.Core.Extensibility;

namespace Glimpse.Common.Logging
{
    public class GlimpseSimpleLogger : AbstractSimpleLogger
    {
        private static IMessageBroker _messageBroker;
        private static Func<IExecutionTimer> _timerStrategy;

        [ThreadStatic]
        private static Stopwatch _fromLastWatch;

        #region Ctors

        public GlimpseSimpleLogger(string logName, LogLevel logLevel, bool showlevel, bool showDateTime, bool showLogName, string dateTimeFormat) : base(logName, logLevel, showlevel, showDateTime, showLogName, dateTimeFormat)
        {
        }

        #endregion

        #region AbstractLogger Implementation

        /// <summary>
        /// Actually sends the message to the underlying log system.
        /// </summary>
        /// <param name="level">the level of this log event.</param>
        /// <param name="message">the message to log</param>
        /// <param name="exception">the exception to log (may be null)</param>
        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            IExecutionTimer timer = null;

            try
            {
                timer = _timerStrategy();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.Message);
            }

            if (timer == null || _messageBroker == null)
                return;

            _messageBroker.Publish(new CommonLoggingMessage
            {
                Logger = Name,
                Level = level,
                Message = message.ToString(),              
                FromFirst = timer.Point().Offset,
                FromLast = CalculateFromLast(timer)
            });
        }

        #endregion

        #region Private Methods

        private TimeSpan CalculateFromLast(IExecutionTimer timer)
        {
            if (_fromLastWatch == null)
            {
                _fromLastWatch = Stopwatch.StartNew();
                return TimeSpan.FromMilliseconds(0);
            }

            // Timer started before this request, reset it
            if (DateTime.Now - _fromLastWatch.Elapsed < timer.RequestStart)
            {
                _fromLastWatch = Stopwatch.StartNew();
                return TimeSpan.FromMilliseconds(0);
            }

            var result = _fromLastWatch.Elapsed;
            _fromLastWatch = Stopwatch.StartNew();
            return result;
        }

        #endregion


        #region Public Static Methods

        public static void Initialize(IMessageBroker messageBroker, Func<IExecutionTimer> timerStrategy)
        {
            _messageBroker = messageBroker;
            _timerStrategy = timerStrategy;
        }

        #endregion
    }
}