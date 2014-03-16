using System;
using Common.Logging;
using Glimpse.Core.Extensions;
using Glimpse.Core.Tab.Assist;
using Glimpse.Core.Extensibility;

namespace Glimpse.Common.Logging
{
    public class CommonLoggingTab : ITab, ITabLayout, ITabSetup, IKey
    {
        private static readonly object Layout = TabLayout.Create()
            .Row(r =>
            {                
                r.Cell(0).WidthInPercent(10);
                r.Cell(1).WidthInPercent(10);
                r.Cell(2).WidthInPercent(60);
                r.Cell(3).WidthInPercent(10).AlignRight().Class("mono");
                r.Cell(4).WidthInPercent(10).AlignRight().Class("mono");
            }).Build();

        public string Name
        {
            get { return "Common.Logging"; }
        }

        public RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest; }
        }

        public Type RequestContextType
        {
            get {return null; }
        }

        public string Key
        {
            get { return "glimpe_common_logging"; }
        }

        public object GetData(ITabContext context)
        {
            var section = Plugin.Create("Level", "Logger", "Message", "From Request Start", "From Last");

            foreach (var message in context.GetMessages<CommonLoggingMessage>())
            {
                section.AddRow()
                    .Column(message.Level.ToString())
                    .Column(message.Logger)
                    .Column(message.Message)
                    .Column(message.FromFirst.TotalMilliseconds.ToString("0.00"))
                    .Column(message.FromLast.TotalMilliseconds.ToString("0.00"))
                    .ApplyRowStyle(GetLogLevelRowStyle(message.Level));
            }

            return section.Build();
        }

        public object GetLayout()
        {
            return Layout;
        }

        public void Setup(ITabSetupContext context)
        {
            context.PersistMessages<CommonLoggingMessage>();
        }

        /// <summary>
        /// get the row style according to <see cref="LogLevel" />
        /// </summary>
        /// <param name="level">
        /// 
        /// </param>
        /// <returns></returns>
        private string GetLogLevelRowStyle(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.All:
                case LogLevel.Off:
                    return string.Empty;
                case LogLevel.Fatal:
                    return "fail";

                default:
                    return level.ToString().ToLower();
            }
        }

    }
}