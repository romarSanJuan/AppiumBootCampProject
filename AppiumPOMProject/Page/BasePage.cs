using AppiumPOMProject.Constants;
using Gilgen.Mobile.AutomationTests.Reports;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using static System.Net.Mime.MediaTypeNames;

namespace AppiumPOMProject.Page
{
    public class BasePage
    {
        public static ThreadLocal<AndroidDriver<AndroidElement>> driver = new ThreadLocal<AndroidDriver<AndroidElement>>();
        public static AndroidDriver<AndroidElement> GetDriver() => driver.Value;

        protected string GetScreenshot()
        {
            var file = ((ITakesScreenshot)BasePage.GetDriver()).GetScreenshot();
            var image = file.AsBase64EncodedString;

            return image;
        }

        ///<summary>
        ///Create Element by adding App Id to Automation ID
        ///</summary>
        private static string CreateElementId(string id)
        {
            return $"{AppInfo.AppId}:id/{id}";
        }

         ///<summary>
        ///Get Element using Element Automation ID
        ///</summary>
        protected AndroidElement GetElementById(string elementLocator)
        {
            //ExtentReporting.LogScreenshot($"Get Element By Id: {elementLocator}", GetScreenshot());
            var elementId = CreateElementId(elementLocator);
            var element = GetDriver().FindElementById(elementId);
            return element;
        }

        ///<summary>
        ///Get Element using Element Attribute Resource ID
        ///</summary>
        protected AndroidElement GetElementByResourceId(string elementLocator)
        {
            //ExtentReporting.LogScreenshot($"Get Element By Resource Id: {elementLocator}", GetScreenshot());
            var ResourceId = CreateElementId(elementLocator);
            var element = GetDriver().FindElementByXPath($"//*[@resource-id='{ResourceId}']");
            return element;
        }

        ///<summary>
        ///Get Element using Element Attribute Text
        ///</summary>
        protected static AndroidElement GetElementByText(string text)
        {
            //ExtentReporting.LogScreenshot($"Get Element By Text: {text}", GetScreenshot());
            var element = GetDriver().FindElementByXPath($"//*[@text='{text}']");
            return element;
        }

        protected AndroidElement GetElementByResourceIdandText(string elementLocator, string text)
        {
            //ExtentReporting.LogScreenshot($"Get Element By Resource Id: {elementLocator} and Text: {text}", GetScreenshot());
            var resourceId = CreateElementId(elementLocator);
            var element = GetDriver().FindElementByXPath($"//*[@resource-id='{resourceId}' and @text='{text}']");
            return element;
        }

        protected void WaitForElementById(string elementLocator)
        {
            try
            {
                ExtentReporting.LogScreenshot($"Wait for Element By Id: {elementLocator}", GetScreenshot());
                var elementId = CreateElementId(elementLocator);
                var wait = new DefaultWait<AndroidDriver<AndroidElement>>(GetDriver())
                {
                    Timeout = TimeSpan.FromSeconds(10)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(driver => GetDriver().FindElementById(elementId));
            }
            catch (Exception e)
            {
                throw new Exception($"Wait For Element By Id {elementLocator} Failed: {e.Message}");
            }
        }

        protected void WaitForElementByText(string text)
        {
            try
            {
                ExtentReporting.LogScreenshot($"Wait for Element By Text: {text}", GetScreenshot());
                var wait = new DefaultWait<AndroidDriver<AndroidElement>>(GetDriver())
                {
                    Timeout = TimeSpan.FromSeconds(10)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(driver => GetDriver().FindElementByXPath($"//*[@text='{text}']"));
            }
            catch (Exception e)
            {
                throw new Exception($"Wait For Element By Text {text} Failed: {e.Message}");
            }
        }

        ///<summary>
        ///Sendkey function to Element Input Field using Automation ID
        ///</summary>
        public void SendKeysToElementById(string elementLocator, string text)
        {
            try
            {
                ExtentReporting.LogScreenshot($"Send Key to Element: {elementLocator} with Text: {text}", GetScreenshot());
                GetElementByResourceId(elementLocator).SendKeys(text);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to send keys to the element: {elementLocator}. {e.Message}");
            }
        }

        ///<summary>
        ///Selecting Element using Automation ID
        ///</summary>
        public void SelectElementById(string elementLocator)
        {
            try
            {
                ExtentReporting.LogScreenshot($"Select Element by Id: {elementLocator}", GetScreenshot());
                GetElementById(elementLocator).Click();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to select thee Element by Id: {elementLocator}. {e.Message}");
            }
        }

        public void SelectElementByText(string text)
        {
            try
            {
                ExtentReporting.LogScreenshot($"Select Element by Text: {text}", GetScreenshot());
                GetElementByText(text).Click();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to select the Element by Text: {text}. {e.Message}");
            }
        }

        public void SelectElementByResourceIdAndText(string elementLocator, string text)
        {
            try
            {
                ExtentReporting.LogScreenshot($"Select Element by Resource Id: {elementLocator} and Text: {text}", GetScreenshot());
                GetElementByResourceIdandText(elementLocator, text).Click();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to select the Element by Resource Id: {elementLocator} and Text: {text}. {e.Message}");
            }
        }        
    }
}
