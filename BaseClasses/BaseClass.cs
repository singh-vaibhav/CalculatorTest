using log4net;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System;
using UiAutomationTests.ComponentHelper;
using UiAutomationTests.Settings;
using UiAutomationTests.Configuration;

namespace UiAutomationTests.BaseClasses
{


    public class BaseClass
    {
       
           
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(BaseClass));

        public static Environments UiApp {get; set;}



        private static FirefoxDriver GetFirefoxDriver()
        {
            if (!UiApp.headless)

            {
                FirefoxDriver driver = new FirefoxDriver(AppContext.BaseDirectory);
                Logger.Info(" Using Firefox Driver  ");
                return driver;

            }
            else
            {
                var options = new FirefoxOptions();
                options.AddArguments("--headless");
                FirefoxDriver driver = new FirefoxDriver(AppContext.BaseDirectory,options);
                Logger.Info(" Using Headless Firefox Driver  ");
                return driver;

            }

        }

        private static ChromeDriver GetChromeDriver()
        {

            if (!UiApp.headless)
            {
                ChromeDriver driver = new ChromeDriver(AppContext.BaseDirectory);
                Logger.Info(" Using Chrome Driver  ");

                return driver;

            }
            else
            {
                var options = new ChromeOptions();
                options.AddArguments("headless");
                options.AddArguments("disable-gpu");
                options.AddArguments("--start-maximized");
                Logger.Info(" Using Chrome Options ");
                ChromeDriver driver = new ChromeDriver(AppContext.BaseDirectory,options);
                Logger.Info(" Using Headless Chrome Driver  ");
                return driver;


            }
        }

        private static InternetExplorerDriver GetIEDriver()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver(AppContext.BaseDirectory);
            return driver;
        }

    
        private static InternetExplorerOptions GetIEOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.EnsureCleanSession = true;
            options.ElementScrollBehavior = InternetExplorerElementScrollBehavior.Bottom;
            Logger.Info(" Using Internet Explorer Options ");
            return options;
        }

        private static EdgeDriver GetEdgeDriver()
        {
            var driver = new EdgeDriver();
           
            return driver;
        }

        [SetUp]
        public static void InitWebdriver()
        {
            Console.WriteLine("1");
             UiApp = AutomationEnvironment.GetEnvironment();
            ObjectRepository.Config = new AppConfigReader();




            switch (ObjectRepository.Config.GetBrowser())
            {
                case BrowserType.FIREFOX:
                    ObjectRepository.Driver = GetFirefoxDriver();

                    break;

                case BrowserType.CHROME:
                    ObjectRepository.Driver = GetChromeDriver();
                    break;

                case BrowserType.IEXPLORER:
                    ObjectRepository.Driver = GetIEDriver();
                    break;

                case BrowserType.EDGE:
                    ObjectRepository.Driver = GetEdgeDriver();
                    break;


                default:

                    throw new Exception("Driver Not Found : " );

            }
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            BrowserHelper.BrowserMaximize();
            NavigationHelper.NavigateToUrl(ObjectRepository.Config.GetUrl());


        }


        [TearDown]
        public static void TearDown()
        {
            if (ObjectRepository.Driver != null)
            {
                GenericHelper.TakeScreenShot();
                Logger.Info(" Stopping the Driver  ");
                ObjectRepository.Driver.Close();
                ObjectRepository.Driver.Quit();

            }
            else
            {
                Logger.Info(" Driver was not invoked");
            }
            DisposeDriverService.FinishDriver();

        }

       


    }
}
