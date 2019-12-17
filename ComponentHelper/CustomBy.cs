using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace UiAutomationTests.ComponentHelper
{
    public class CustomBy : By
    {
        public string FriendlyName { get; set; }
        public By By { get; set; }

        public static CustomBy XPath(string xPath, string friendlyName)
        {
            return new CustomBy { By = By.XPath(xPath), FriendlyName = friendlyName };
        }

        public static CustomBy CssSelector(string cssSelector, string friendlyName)
        {
            return new CustomBy { By = By.CssSelector(cssSelector), FriendlyName = friendlyName };
        }

        public static CustomBy ClassName(string className, string friendlyName)
        {
            return new CustomBy { By = By.ClassName(className), FriendlyName = friendlyName };
        }

        public static CustomBy Id(string id, string friendlyName)
        {
            return new CustomBy { By = By.Id(id), FriendlyName = friendlyName };
        }

        public static CustomBy Name(string name, string friendlyName)
        {
            return new CustomBy { By = By.Name(name), FriendlyName = friendlyName };
        }

        public static CustomBy TagName(string tagName, string friendlyName)
        {
            return new CustomBy { By = By.TagName(tagName), FriendlyName = friendlyName };
        }


    }


}
