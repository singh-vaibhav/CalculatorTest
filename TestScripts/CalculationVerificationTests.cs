using NUnit.Allure.Core;
using NUnit.Framework;
using System.Data;
using UiAutomationTests.BaseClasses;
using UiAutomationTests.ComponentHelper;
using UiAutomationTests.Settings;

namespace UiAutomationTests.TestScript
{
    [TestFixture]
    [AllureNUnit]
    [Category("Regression")]
    public class CalculationVerificationTests : BaseClass
    {
       
        [Test]
        [TestCase(TestName = "Verify calculation when valid data is passed")]
        
        public void VerifyCalculationWithValidInput()
        {
            var excel = new ExcelDataHelper();
            var dataTable =excel.ReadFromExcel();
            foreach(DataRow dr in dataTable.Rows)
            {
                var firstNumber = dr["FirstNumber"].ToString();
                var secondNumber = dr["SecondNumber"].ToString();
                var expectedBehavior = dr["ExpectedBehavior"].ToString();
                var calculator = new Calculator_Addition(ObjectRepository.Driver);
                calculator.SetInputForCalculation(firstNumber, secondNumber);
                calculator.ClickOnSubmitButton();
                Assert.AreEqual(expectedBehavior, calculator.GetResults());


            }




        }

        [Test]
        [TestCase(TestName = "Verify calculation when invalid data is passed")]

        public void VerifyCalculationWithInValidInput()
        {
            var excel = new ExcelDataHelper();
            var dataTable = excel.ReadFromExcel("InvalidValues");
            foreach (DataRow dr in dataTable.Rows)
            {
                var firstNumber = dr["FirstNumber"].ToString();
                var secondNumber = dr["SecondNumber"].ToString();
                var expectedBehavior = dr["ExpectedBehavior"].ToString();
                var calculator = new Calculator_Addition(ObjectRepository.Driver);
                calculator.SetInputForCalculation(firstNumber, secondNumber);
                calculator.ClickOnSubmitButton();
                Assert.AreEqual(expectedBehavior, calculator.GetResults());


            }




        }




    }
}