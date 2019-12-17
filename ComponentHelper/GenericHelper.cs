using Allure.Commons;
using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using UiAutomationTests.Settings;

namespace UiAutomationTests.ComponentHelper
{
    public class GenericHelper
    {
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(GenericHelper));

        public static void TakeScreenShot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {

                Logger.Info(TestContext.CurrentContext.Test.MethodName + " test case execution failed");

                Logger.Error("screenshot taken of the failed test case: " + TestContext.CurrentContext.Result.Message);

                var screen = ObjectRepository.Driver.TakeScreenshot();
                var filename = TestContext.CurrentContext.Test.MethodName + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".Png";
                string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName) + @"\Screenshot";
                var datetime = DateTime.Today;
                var date = datetime.Date.ToString("yyyyMMdd");

                string subfolderPath = path + @"\" + date;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!Directory.Exists(subfolderPath))
                {
                    Directory.CreateDirectory(subfolderPath);
                }

                var file = subfolderPath + @"\" + filename;


                screen.SaveAsFile(file, ScreenshotImageFormat.Png);
                AllureLifecycle.Instance.AddAttachment(filename, "image", File.ReadAllBytes(file), "png");

                return;


            }



        }

        public static string GetText(By locator)
        {
           var value = WebElementHelper.GetElement(locator).Text;
            Logger.Info("Text value is : " + value);

            return value;
        }

        public static string GetTitle()
        {
            var value =ObjectRepository.Driver.Title;
            return value;
        }
    }
}
