using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;

namespace AppiumTestProject1
{
    public class Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private const string AppiumServerUrl = "http://127.0.0.1:4723";
        private const string AppToTest = "ch.erni.gilgen.mobile.dev";

        [SetUp]
        public void SetUp()
        {
            var serverUri = new Uri(AppiumServerUrl);
            var options = new AppiumOptions();
            options.AddAdditionalCapability("platformName", "Android");
            options.AddAdditionalCapability("appium:automationName", "uiautomator2");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "ch.erni.gilgen.mobile.dev");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".MainActivity");
            driver = new AndroidDriver<AndroidElement>(serverUri, options);
            driver.ActivateApp(AppToTest);
        }

        [TearDown]
        public void TearDown()
        {
            driver.TerminateApp(AppToTest);
            driver.Dispose();
        }

        [Test]
        public void BasicElementLocatorTest()
        {
            /*Element Locators
             * FindElementById
             * FindElementByText
             * FindElementByXpath
             */
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton").Click();
            driver.FindElementById($"{AppToTest}:id/WelcomeTemplate_LoginButton").Click();
            driver.FindElementByXPath("//android.widget.Button[@resource-id=\"ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton\"]").Click();
            driver.FindElementByXPath($"//*[@text='LOG IN']").Click();
            driver.FindElementByXPath($"//*[@resource-id='ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton']").Click();
        }

        [Test]
        public void BasicElementInteractionTest()
        {
            /* Common User Actions
             * Input = SendKey
             * Delete Input Field = Clear
             * Tap = Click 
             */
            driver.FindElementByXPath($"//*[@resource='ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton']").Click();
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/LoginEmail_InputField").SendKeys("technician.gilgen@betterask.erni");
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/LoginEmail_InputField").Clear();
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/LoginPassword_InputField").SendKeys("Gilgen!1");
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton").Click();


            AndroidElement EmailInputField = driver.FindElementByXPath($"//*[@text='LOG IN']");
            EmailInputField.Clear();
            EmailInputField.SendKeys("technician.gilgen@betterask.erni");

        }

        [Test]
        public void BasicElementAttribExtractionandValidationTest()
        {
            /* Extract Attributes + Simple Validation
             * Compare Element Text
             * Is Enabled
             * Is Displayed
             */
            string actualValue = driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton").Text;
            string expectedValue = "LOG IN";
            Assert.That(actualValue, Is.EqualTo(expectedValue), "Incorrect Element Value, Expected: " + expectedValue + " But was: " + actualValue);

            string falseValue = "LOG OUT";
            Assert.That(actualValue, Is.Not.EqualTo(falseValue), "Valdiation Message Here");

            bool isEnabled = driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton").Enabled;
            bool expectedState = true;
            Assert.That(isEnabled, Is.EqualTo(expectedState), "Incorrect Element Value, Expected: " + expectedState + " But was: " + isEnabled);

            bool isdisplayed = driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton").Displayed;
            bool expectedView = true;
            Assert.That(isdisplayed, Is.EqualTo(expectedView), "Incorrect Element Value, Expected: " + expectedView + " But was: " + isdisplayed);
        }

        [Test]
        public void WaitForElementTest()
        {
            /* Waiting for Element
             * Wait for Element to Disappear
             * Wait for Element to be Visible
             */

            var wait = new DefaultWait<AndroidDriver<AndroidElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(driver => driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton"));

            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/LoginEmail_InputField").SendKeys("technician.gilgen@betterask.erni");
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/LoginPassword_InputField").SendKeys("Gilgen!1");
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton").Click();
        }

        [Test]
        public void WaitForNoElementTest()
        {
            /* Waiting for Element
             * Wait for Element to Disappear
             * Wait for Element to be Visible
             */

            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/LoginEmail_InputField").SendKeys("technician.gilgen@betterask.erni");
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/LoginPassword_InputField").SendKeys("Gilgen!1");
            driver.FindElementById("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton").Click();

            var wait = new DefaultWait<AndroidDriver<AndroidElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
            };
            wait.Until(driver => !IsElementVisible("ch.erni.gilgen.mobile.dev:id/WelcomeTemplate_LoginButton"));
        }

        protected bool IsElementVisible(string locator)
        {
            try
            {
                return driver.FindElementById(locator).Displayed;

            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        [Test]
        public void PressDeviceKeyButtonsTest()
        {
            driver.PressKeyCode(AndroidKeyCode.Back);
        }

        [Test]
        public void TapByElementCoordinatesTest()
        {
            TouchAction t = new(driver);

            t.Tap(123, 456).Perform();
        }

        [Test]
        public void DragAndDropByElementCoordinatesTest()
        {
            TouchAction t = new(driver);

            t.LongPress(123, 456).MoveTo(789, 321).Release().Perform();
        }

        [Test]
        public void ScrollFunctionTest()
        {
            string elementId = "";
            driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView(new UiSelector().resourceId(\"" + elementId + "\"))"));
            driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView(new UiSelector().text(\"" + elementId + "\"))"));
        }

    }
}