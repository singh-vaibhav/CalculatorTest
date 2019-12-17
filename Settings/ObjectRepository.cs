using OpenQA.Selenium;
using UiAutomationTests.Interfaces;

namespace UiAutomationTests.Settings
{
    public class ObjectRepository
    {
        public static IConfig Config { get; set; }
        public static IWebDriver Driver { get; set; }

        public static Calculator_Addition calculator_Addition;
       


    }
}
