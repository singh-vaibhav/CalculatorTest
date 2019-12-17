using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace UiAutomationTests.ComponentHelper
{
    public class ButtonHelper
    {
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(ButtonHelper));
        private static IWebElement _element { get; set; }

        public static void ButtonClick(By locator)
        {
            var element = WebElementHelper.GetElement(locator);

            var elementType = element.TagName;
            var custom = locator as CustomBy;
            Logger.Info("Clicking on " + custom.FriendlyName + " which is of type " + elementType);
            element.Click();
            WebElementHelper.WebdriverWait(TimeSpan.FromSeconds(10));

            //if (elementType.ToLower().Equals("button"))
            //{
            //    element.Click();
            //    var custom = locator as CustomBy;
            //    Logger.Info("Clicking on " + custom.FriendlyName + " " + elementType);

            //}

        }

        public static bool IsButtonEnabled(By locator)
        {
            var element = WebElementHelper.GetElement(locator);

            var elementType = element.TagName;

            if (elementType.ToLower().Equals("button"))
            { 
                var custom = locator as CustomBy;
                Logger.Info(custom.FriendlyName + " " + elementType + " is Not a button");
                return false;

            }
            else
            {
                var flag = WebElementHelper.GetElement(locator).Enabled;
                var custom = locator as CustomBy;
                Logger.Info(custom.FriendlyName + " " + elementType + " is enabled");
                return flag;
            }

           
        }

        public static string GetButtonText(By locator)
        {

            var element = WebElementHelper.GetElement(locator);

            var elementType = element.TagName;

            if (elementType.ToLower().Equals("button"))
            {
                var custom = locator as CustomBy;
                Logger.Info(custom.FriendlyName + " " + elementType + " is Not a button");
                return string.Empty;

            }
            else
            {
                var value = WebElementHelper.GetElement(locator).GetAttribute("value");
                var custom = locator as CustomBy;
                Logger.Info("Text of " + custom.FriendlyName + " " + elementType);
                return value;
            }
        }

      
    }
}
