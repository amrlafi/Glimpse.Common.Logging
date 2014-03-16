using Glimpse.Core.Extensibility;

namespace Glimpse.Common.Logging
{
    public class CommonLoggingInspector : IInspector
    {
        public void Setup(IInspectorContext context)
        {
            GlimpseSimpleLogger.Initialize(context.MessageBroker, context.TimerStrategy);
        }
    }
}