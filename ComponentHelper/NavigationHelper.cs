using System;
using System.Collections.Generic;
using System.Text;
using UiAutomationTests.Settings;

namespace UiAutomationTests.ComponentHelper
{
    public class NavigationHelper
    {
        public static void NavigateToUrl(string Url)
        {
            ObjectRepository.Driver.Navigate().GoToUrl(Url);
        }
    }
} 