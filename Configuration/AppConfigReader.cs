
using System;
using UiAutomationTests.BaseClasses;
using UiAutomationTests.Interfaces;

namespace UiAutomationTests.Configuration
{
    public class AppConfigReader :IConfig
    {
        public BrowserType? GetBrowser()
        {
            string browser = BaseClass.UiApp.browser.ToUpper();
            try
            {
                return (BrowserType)Enum.Parse(typeof(BrowserType), browser);
            }
            catch (Exception)
            {
                return null;
            }
        }


      
        public string GetUrl()
        {
            return BaseClass.UiApp.url;
        }
        public int GetElementLoadTimeOut()
        {
           
                return 10;
        }

        public int GetPageLoadTimeOut()
        {
            return 10;
        }
    }
}