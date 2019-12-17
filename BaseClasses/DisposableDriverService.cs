using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UiAutomationTests.ComponentHelper;

namespace UiAutomationTests.BaseClasses
{
    public static class DisposeDriverService
    {
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(DisposeDriverService));

        private static readonly List<string> _processesToCheck =
            new List<string>
            {
            "chromedriver",
            "geckodriver",
            "phantomjs",
            };
        public static DateTime? TestRunStartTime { get; set; }
        public static void FinishDriver()
        {
             List<string> _processesToCheck = new List<string>   {  "chromedriver", "geckodriver", "phantomjs" };
            foreach(var processName  in _processesToCheck)
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    try
                    {
                       if (process.ProcessName.ToLower().Contains(processName))
                            {
                                Logger.Info(process.ProcessName);
                                process.Kill();
                                break;
                            }
                        

                    }
                    catch (Exception e)
                    {
                        Logger.Error("Error details: " + e);

                    }
                }

            }

        }
    }
}