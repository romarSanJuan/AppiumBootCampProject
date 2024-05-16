using AppiumPOMProject.Constants;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AppiumPOMProject.Page
{
    public class BasePage
    {
        public static ThreadLocal<AndroidDriver<AndroidElement>> driver = new ThreadLocal<AndroidDriver<AndroidElement>>();
        public static AndroidDriver<AndroidElement> GetDriver() => driver.Value;

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
            var elementId = CreateElementId(elementLocator);
            var element = GetDriver().FindElementById(elementId);
            return element;
        }

        ///<summary>
        ///Get Element using Element Attribute Resource ID
        ///</summary>
        protected AndroidElement GetElementByResourceId(string elementLocator)
        {
            var ResourceId = CreateElementId(elementLocator);
            var element = GetDriver().FindElementByXPath($"//*[@{"resource-id"}='{ResourceId}']");
            return element;
        }

        ///<summary>
        ///Get Element using Element Attribute Text
        ///</summary>
        protected AndroidElement GetElementByText(string elementLocator)
        {
            var element = GetDriver().FindElementByXPath($"//*[@{"text"}='{elementLocator}']");
            return element;
        }

        ///<summary>
        ///Sendkey function to Element Input Field using Automation ID
        ///</summary>
        public void SendKeysToElementById(string elementLocator, string value)
        {
            try
            {
                GetElementByResourceId(elementLocator).SendKeys(value);
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
                GetElementById(elementLocator).Click();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to select thee element: {elementLocator}. {e.Message}");
            }
        }
    }
}
