using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UiAutomationTests.ComponentHelper
{
    public class Log4NetHelper
    {
        #region Field

        private static ILog _logger;
        private static ILog _xmlLogger;
        private static ConsoleAppender _consoleAppender;
        private static FileAppender _fileAppender;
        private static RollingFileAppender _rollingFileAppender;
        private static string _layout = "%date{dd-MMM-yyyy-HH:mm:ss} [%C] [%level] [%M] - %message%newline";

        #endregion

        #region Property

        public static string Layout
        {
            set { _layout = value; }
        }

        #endregion

        #region Private

        private static PatternLayout GetPatternLayout()
        {
            var patterLayout = new PatternLayout()
            {
                ConversionPattern = _layout
               
            };
            patterLayout.ActivateOptions();
            return patterLayout;
        }

        private static ConsoleAppender GetConsoleAppender()
        {
            var consoleAppender = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All
            };
            consoleAppender.ActivateOptions();
            return consoleAppender;
        }

        private static FileAppender GetFileAppender()
        {
            var fileAppender = new FileAppender()
            {
                Name = "FileAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                AppendToFile = true,
                File = "FileLogger.log",
            };
            fileAppender.ActivateOptions();
            return fileAppender;
        }

        private static RollingFileAppender GetRollingFileAppender()
        {
            var rollingAppender = new RollingFileAppender()
            {
                Name = "Rolling File Appender",
                AppendToFile = true,
                File = "UiAppLogs.txt",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                MaximumFileSize = "1MB",
                MaxSizeRollBackups = 15 //file1.log,file2.log.....file15.log
            };
            rollingAppender.ActivateOptions();
            return rollingAppender;
        }

        #endregion

        #region Public

        public static ILog GetLogger(Type type)
        {
            if (_consoleAppender == null)
                _consoleAppender = GetConsoleAppender();

            if (_fileAppender == null)
                _fileAppender = GetFileAppender();

            if (_rollingFileAppender == null)
                _rollingFileAppender = GetRollingFileAppender();

            if (_logger != null)
                return _logger;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            BasicConfigurator.Configure(logRepository,_consoleAppender, _rollingFileAppender);
            _logger = LogManager.GetLogger(type);
            return _logger;

        }

        public static ILog GetXmlLogger(Type type)
        {
            if (_xmlLogger != null)
                return _xmlLogger;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("App.config"));
            _xmlLogger = LogManager.GetLogger(type);
            return _xmlLogger;
        }

        #endregion
    }
}
