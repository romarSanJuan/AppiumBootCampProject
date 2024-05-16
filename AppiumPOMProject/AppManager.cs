// Reference: https://github.com/NetanelMosheCohen/AppiumCSharp

using AppiumPOMProject.Constants;
using AppiumPOMProject.Page;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System.Linq;

namespace AppiumPOMProject
{
    public class AppManager
    {
        private const string AppiumServerUrl = "http://127.0.0.1:4723";

        private AndroidDriver<AndroidElement> InitializeDriver()
        {
            Uri serverUri = new Uri(AppiumServerUrl);
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("platformName", "Android");
            options.AddAdditionalCapability("appium:automationName", "uiautomator2");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, AppInfo.AppId);
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".MainActivity");            
            BasePage.driver.Value = new AndroidDriver<AndroidElement>(serverUri, options);
            StartApp();
            return BasePage.GetDriver();
        }
        

        public void StartApp()
        {
            BasePage.GetDriver().ActivateApp(AppInfo.AppId);
        }

        private void CloseDriver()
        {
            BasePage.GetDriver().TerminateApp(AppInfo.AppId);
            BasePage.GetDriver().Dispose();
        }

        private void SetUpDriver()
        {
            BasePage.driver.Value = InitializeDriver();
        }

        [SetUp]
        public void SetUp()
        {
            SetUpDriver();
        }

        [TearDown]
        public void TearDown()
        {
            CloseDriver();
        }
    }
}
