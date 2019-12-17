using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace UiAutomationTests.ComponentHelper
{
    public class TextboxHelper
    {
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(TextboxHelper));

        public static void SendKeys(By locator, string text)
        {

            var element = WebElementHelper.GetElement(locator);
            if (element.GetAttribute("type") == "password")
            {
                element.SendKeys(text);
                text = "*******";
            }
            else
            {
                element.Click();
                element.SendKeys(text);
            }
            
            var custom = locator as CustomBy;
            Logger.Info("Entering value:  " + text + " in " + custom.FriendlyName + " which is of type " + element.TagName);
        }

     
        public static void Clear(By locator)
        {
            var element = WebElementHelper.GetElement(locator);
            element.Click();
            element.Clear();
            var custom = locator as CustomBy;
            Logger.Info("Clearing the  " + custom.FriendlyName + " which is of type " + element.TagName);



        }

        public static void Click(By locator)
        {
            var element = WebElementHelper.GetElement(locator);
            if (!element.TagName.ToLower().Equals("button"))
            {
                var custom = locator as CustomBy;
                Logger.Info("Clicking on " + custom.FriendlyName + " which is of type " + element.TagName);
                element.Click();
            }


        }

       
    }
}