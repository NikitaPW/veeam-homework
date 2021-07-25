using System;
using NLog;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace VeeamHMSelenium
{
    [TestFixture]
    public class TestClass : BaseClass
    {
        [Test]
        [TestCaseSource(typeof(BaseClass), "TestCases")]
        public void CheckNumberOfVacancies(string department, string language, int expectedOutput)
        {  
            log.Info($"Test CheckNumberOfVacancies starts with input parameters: department - {department}, " +
                $"language - {language}, expected number of vacancies - {expectedOutput}");

            // setting department
            try
            {
                driver.FindElement(By.XPath("//*[@class='col-12 col-lg-4']/div/div[2]/div/div/button")).Click();
                driver.FindElement(By.XPath($"//*[@class='dropdown-item' and text()='{department}']")).Click();
            }
            catch (Exception)
            {
                log.Error($"Test Failed. Couln't locate department - {department}");
            }

            //setting language
            try
            {
                driver.FindElement(By.XPath("//*[@class='col-12 col-lg-4']/div/div[3]/div/div/button")).Click();
                driver.FindElement(By.XPath($"//*[@class='custom-control-label' and text()='{language}']")).Click();
            }
            catch (Exception)
            {
                log.Error($"Test Failed. Couln't locate language - {language}");
            }

            try
            {
                // count elements
                int count = driver.FindElements(By.XPath("//*[@class='card card-no-hover card-sm']")).Count;

                // checking the result
                Assert.AreEqual(count, expectedOutput);
                log.Info("Test passed successfully");
            }
            catch
            {
                log.Error("Test Failed. Test result is different from expected value");
            }

            // clearning selected fields for further run
            driver.Navigate().Refresh();
        }
    }
}
