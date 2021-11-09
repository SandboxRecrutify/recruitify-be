using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;
using log4net;
using log4net.Config;

namespace Recrutify.Host.Configuration
{
    public static class Log4NetConfig
    {
        public static Hierarchy SetConfiguration()
        {
            var hierarchy = (Hierarchy)log4net.LogManager.GetRepository();
            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = "%date %level %message%newline"
            };

            var rollingFileAppender = new RollingFileAppender
            {
                File = "logfile.txt",
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaximumFileSize = "2MB",
                Layout = patternLayout,
            };

            hierarchy.Root.AddAppender(rollingFileAppender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            log4net.Config.BasicConfigurator.Configure(hierarchy);
            return hierarchy;
        }
    }
}
