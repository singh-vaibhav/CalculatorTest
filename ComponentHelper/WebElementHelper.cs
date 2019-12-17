using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UiAutomationTests.Settings;

namespace UiAutomationTests.ComponentHelper
{


    public class WebElementHelper
    {
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(WebElementHelper));

        private static Func<IWebDriver, bool> WaitForWebElementFunc(By locator)
        {
            return ((x) =>
            {
                if (x.FindElements(locator).Count == 1)
                    return true;
                return false;
            });
        }

        private static Func<IWebDriver, IList<IWebElement>> GetAllElements(By locator)
        {
            return ((x) =>
            {
                string friendlyName = string.Empty;
                var custom = locator as CustomBy;
                if (custom != null)
                {
                    friendlyName = custom.FriendlyName;
                    locator = custom.By;
                }
                else
                    friendlyName = locator.ToString();
                return x.FindElements(locator);

                throw new NoSuchElementException(friendlyName + "  element Not Found : ");

            });
        }

     

        private static Func<IWebDriver, IWebElement> WaitForWebElementInPageFunc(By locator)
        {
            return ((x) =>
            {
                if (x.FindElements(locator).Count == 1)
                    return x.FindElement(locator);
                return null;
            });
        }

        public static void SelecFromAutoSuggest(By autoSuggesLocator, string initialStr, string strToSelect,
            By autoSuggestistLocator)
        {
            var autoSuggest = GetElement(autoSuggesLocator);
            autoSuggest.SendKeys(initialStr);
            Thread.Sleep(1000);

            var wait = WebElementHelper.GetWebdriverWait(TimeSpan.FromSeconds(40));
            var elements = wait.Until(GetAllElements(autoSuggestistLocator));
            var select = elements.First((x => x.Text.Equals(strToSelect, StringComparison.OrdinalIgnoreCase)));
            select.Click();
            Thread.Sleep(1000);
        }

        public static WebDriverWait GetWebdriverWait(TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, timeout)
            {
                PollingInterval = TimeSpan.FromMilliseconds(5000),
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            return wait;
        }

        public static void WebdriverWait(TimeSpan time)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = time;
            
        }
        public static bool IsElementPresent(By locator)
        {
            string friendlyName = string.Empty;
            var custom = locator as CustomBy;
            bool flag = true;
            if (custom != null)
            {
                friendlyName = custom.FriendlyName;
                locator = custom.By;
            }
            else
                friendlyName = locator.ToString();

          
                if (ObjectRepository.Driver.FindElements(locator).Count > 0)
            {
                HighlightElement(locator);
                Logger.Info(friendlyName + " is present.");
                

            }

           else
            {
                flag = false;
                throw new NoSuchElementException(friendlyName + "  element Not Found. ");

            }

            return flag;
        }


        public static IWebElement GetElement(By locator)
        {
            var custom = locator as CustomBy;
            if (IsElementPresent(locator))
            {
                locator = custom.By;

                var element = ObjectRepository.Driver.FindElement(locator);
                return element;

            }

            throw new NoSuchElementException("NoSuchElementException occured. " + custom.FriendlyName + " element was not found. ");
        }

        public static string GetText(By locator)
        {

            var value = GetElement(locator).GetAttribute("value");
            if (value == null)
                value = GetElement(locator).GetAttribute("textContent");
            if (value == null)
                value = GetElement(locator).GetAttribute("innerHTML");
            if (value == null)
                value = GetElement(locator).Text;

            return value;
        }

        public static bool WaitForWebElement(By locator, TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            var wait = GetWebdriverWait(timeout);
            var flag = wait.Until(WaitForWebElementFunc(locator));
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut()));
            return flag;
        }

        public static IWebElement WaitForWebElementVisisble(By locator, TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            var wait = GetWebdriverWait(timeout);
            var flag = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut()));
            return flag;
        }

        public static IWebElement WaitForWebElementInPage(By locator, TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            var wait = GetWebdriverWait(timeout);
            var flag = wait.Until(WaitForWebElementInPageFunc(locator));
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut()));
            return flag;
        }

        public static IWebElement Wait(Func<IWebDriver, IWebElement> conditions, TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            var wait = GetWebdriverWait(timeout);
            var flag = wait.Until(conditions);
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut()));
            return flag;
        }

        public static void HighlightElement(By locator)
        {
            var jsDriver = (IJavaScriptExecutor)ObjectRepository.Driver;
            string highlightJavascript = @"$(arguments[0]).css({ ""border-width"" : ""3px"", ""border-style"" : ""solid"", ""border-color"" : ""red""});";
            jsDriver.ExecuteScript(highlightJavascript, new object[] {ObjectRepository.Driver.FindElement(locator)});


        }

        public static void MouseHover(By locator)
        {
            Actions action = new Actions(ObjectRepository.Driver);
            action.MoveToElement(GetElement(locator)).Perform();
            Logger.Info("Hovering on element");
        }

        public static void SelectFromDropDownByValue(By locator, string value)
        {
            var element = GetElement(locator);
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(value);
        }

       

    }
}
