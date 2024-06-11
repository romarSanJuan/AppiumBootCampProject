// Reference: https://github.com/NetanelMosheCohen/AppiumCSharp

using AppiumPOMProject.Constants;
using AppiumPOMProject.Page;
using AppiumPOMProject.Page.PageElement;
using AppiumPOMProject.TestData;
using Gilgen.Mobile.AutomationTests.Reports;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppiumPOMProject
{
    public class AppManager
    {
        private const string AppiumServerUrl = "http://127.0.0.1:4723";
        protected LoginPage loginPage;
        protected RolePickerPage rolePickerPage;
        protected DoorListPage doorListPage;

        protected UserCredentials userCredentials;

        private AndroidDriver<AndroidElement> InitializeDriver()
        {
            Uri serverUri = new Uri(AppiumServerUrl);
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("platformName", "Android");
            options.AddAdditionalCapability("appium:automationName", "uiautomator2");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, AppInfo.AppId);
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".MainActivity");            
            BasePage.driver.Value = new (serverUri, options);
            StartApp();

            loginPage = new LoginPageElement();
            rolePickerPage = new RolePickerPageElement();
            doorListPage = new DoorListPageElement();

            userCredentials = new UserCredentials();
            
            return BasePage.GetDriver();
       
      }
        
        private void StartApp()
        {
            ExtentReporting.CreateTest(TestContext.CurrentContext.Test.MethodName);
            BasePage.GetDriver().ActivateApp(AppInfo.AppId);
            //ExtentReporting.LogInfo(GetDeviceInfo());
            ExtentReporting.LogScreenshot("Starting Application", GetScreenshot());
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

        protected string GetScreenshot()
        {
            var file = ((ITakesScreenshot)BasePage.GetDriver()).GetScreenshot();
            var image = file.AsBase64EncodedString;

            return image;
        }

        public virtual void EndTest()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            switch (testStatus)
            {
                case TestStatus.Failed:
                    ExtentReporting.LogFail($"Test has failed: {message}", GetScreenshot());
                    break;
                case TestStatus.Skipped:
                    ExtentReporting.LogInfo($"Test skipped: {message}");
                    break;
                default:
                    break;
            }
            ExtentReporting.LogInfo("Ending test");
        }

        [SetUp]
        public void SetUp()
        {
            SetUpDriver();
        }

        [TearDown]
        public void TearDown()
        {
            EndTest();
            ExtentReporting.EndReporting();
            CloseDriver();
        }
    }
}
