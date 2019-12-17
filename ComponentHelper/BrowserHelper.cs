﻿using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using UiAutomationTests.Settings;

namespace UiAutomationTests.ComponentHelper
{
    public class BrowserHelper
    {
        public static void BrowserMaximize()
        {
            ObjectRepository.Driver.Manage().Window.Maximize();

        }

        public static void GoBack()
        {
            ObjectRepository.Driver.Navigate().Back();

        }

        public static void Forward()
        {
            ObjectRepository.Driver.Navigate().Forward();
        }

        public static void RefreshPage()
        {
            ObjectRepository.Driver.Navigate().Refresh();
        }

        public static void SwitchToWindow(int index = 0)
        {
            Thread.Sleep(1000);
            ReadOnlyCollection<string> windows = ObjectRepository.Driver.WindowHandles;

            if ((windows.Count - 1) < index)
            {
                throw new NoSuchWindowException("Invalid Browser Window Index" + index);
            }


            ObjectRepository.Driver.SwitchTo().Window(windows[index]);
            Thread.Sleep(1000);
            BrowserMaximize();

        }


        public static void SwitchToParent()
        {
            var windowIds = ObjectRepository.Driver.WindowHandles;


            for (int i = windowIds.Count - 1; i > 0;)
            {
                ObjectRepository.Driver.Close();
                i = i - 1;
                Thread.Sleep(2000);
                ObjectRepository.Driver.SwitchTo().Window(windowIds[i]);
            }
            ObjectRepository.Driver.SwitchTo().Window(windowIds[0]);

        }

        public static void SwitchToFrame(By locator)
        {
            ObjectRepository.Driver.SwitchTo().Frame(ObjectRepository.Driver.FindElement(locator));
        }
    }
}