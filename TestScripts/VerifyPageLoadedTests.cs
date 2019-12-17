using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UiAutomationTests.BaseClasses;
using UiAutomationTests.Settings;

namespace UiAutomationTests.TestScripts
{
    [TestFixture]
    [AllureNUnit]
    [Category("smoke")]
    public class VerifyPageLoadedTests : BaseClass
    {
        [Test]
        [TestCase(TestName = "Verify page loaded correctly")]

        public void VerifyCalculationWithInValidInput()
        {
            var calculator = new Calculator_Addition(ObjectRepository.Driver);
            Assert.IsTrue(calculator.VerifyImportantElementLoaded());
        }
    }
}
