using log4net;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Threading;
using UiAutomationTests.BaseClasses;
using UiAutomationTests.ComponentHelper;

namespace UiAutomationTests
{
    public class Calculator_Addition : PageBase
    {
        public new IWebDriver driver;
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(Calculator_Addition));


        public const string firstNumber_textBox_Xpath = "//input[@class='form-control'and @name='firstNumber']";
        public const string secondNumber_textBox_Xpath = "//input[@class='form-control'and @name='secondNumber']";
        public const string submit_btn_Xpath = "//input[@class='btn btn-default' and @type='submit']";
        public const string results_text_Xpath = "//h3[contains(text(),'Results')]";
        public const string finalResult_text_Xpath = "//div[@class='col-md-6']/div";
        public const string companyLogo_Xpath = "//img[@src='/img/logo.png']";




        public Calculator_Addition(IWebDriver _driver) 
        {
            this.driver = _driver;
        }


        #region WebElement
        public By CompanyLogo = CustomBy.XPath(companyLogo_Xpath, "Logo");

        public By FirstNumber = CustomBy.XPath(firstNumber_textBox_Xpath, "First Number");
        public By SecondNumber = CustomBy.XPath(secondNumber_textBox_Xpath, "Second Number");

        public By SubmitBtn = CustomBy.XPath(submit_btn_Xpath, "Submit button");
        public By ResultsText = CustomBy.XPath(results_text_Xpath, "Result text");
        public By FinalResult = CustomBy.XPath(finalResult_text_Xpath, "Final Result");








        #endregion


        #region Actions



        public void SetInputForCalculation(string firstNumber, string secondNumber)
        {
            TextboxHelper.Clear(FirstNumber);
            TextboxHelper.SendKeys(FirstNumber, firstNumber);
            TextboxHelper.Clear(SecondNumber);
            TextboxHelper.SendKeys(SecondNumber, secondNumber);

        }

        public void ClickOnSubmitButton()
        {

            ButtonHelper.ButtonClick(SubmitBtn);
            Thread.Sleep(1000);
        }

        public string GetResults()
        {
          var result = WebElementHelper.GetText(FinalResult);
            Logger.Info("Final Results " + result);

            return ReplaceWhiteSpacesAndNewLines(result);
        }
      
        public string ReplaceWhiteSpacesAndNewLines(string input)
        {
            var replaceNewLine = input.Replace("\r\n", "");
            var replaceWhiteSpacesAndNewLines = Regex.Replace(replaceNewLine, @"\s+", "");
            return replaceWhiteSpacesAndNewLines;
        }

        public bool VerifyImportantElementLoaded()
        {
            return (WebElementHelper.IsElementPresent(FirstNumber) &&
                WebElementHelper.IsElementPresent(SecondNumber) &&
                WebElementHelper.IsElementPresent(ResultsText) &&
                WebElementHelper.IsElementPresent(CompanyLogo));
        }
        #endregion

    }
}