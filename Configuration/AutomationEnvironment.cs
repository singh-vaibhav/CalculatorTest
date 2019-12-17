using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using UiAutomationTests.ComponentHelper;

namespace UiAutomationTests
{
    public class AutomationEnvironment 
    {
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(AutomationEnvironment));

        public static Environments _environment { get; set; }

        public static AppConfig appConfig { get; set; }


        public static Environments GetEnvironment()
        {
            if (_environment != null)
                return _environment;

            var configurations = new AppConfig();

            if (configurations != null)
            {
                _environment = new Environments()
                {
                    url = configurations.Url,
                    browser = configurations.Browser,
                    headless = configurations.Headless,

                };
            }
            else
            {
                throw new ArgumentNullException("Could not parse Environments details");
            }
            Logger.Info("****Environment Details: ****"  + '\n' + "url: " + _environment.url + '\n' + "browser: " + _environment.browser
                + '\n' + "Headless: " + _environment.headless + '\n' + "=====" + '\n');
            return _environment;
            
        }

    }
}
