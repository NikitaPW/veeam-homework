using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;

namespace VeeamHMSelenium
{
    public class BaseClass
    {
        public Logger log = LogManager.GetLogger("MyAppLogger");
        public IWebDriver driver;
        [OneTimeSetUp]
        public void Open()
        {
            try
            {
                log.Info("Starting New Testing procedure.");
                log.Info("Launcing driver...");
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Url = "https://careers.veeam.ru/vacancies";
                log.Info("Drvier is up.");
            } 
            catch(Exception)
            {
                log.Error("The driver has not launched.");
                Environment.Exit(1);
            }
        }

        [OneTimeTearDown]
        public void Close()
        {
            log.Info("Testing procedure finished execution.");
            driver.Quit();
        }

        // parametrizing test data
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("Разработка продуктов", "Английский", 6);
                //yield return new TestCaseData("Продажи", "Немецкий", 2);
            }
        }    
    }
}
