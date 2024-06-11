using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualBasic;
using System.IO;
using System.Reflection;

namespace Gilgen.Mobile.AutomationTests.Reports
{
    public class ExtentReporting
    {
        private static ExtentReports extentReports;
        private static ExtentTest extentTest;
        private static string testSchedule = DateAndTime.Now.ToString("dd-MM-yyyy_hhmmss");

        private static ExtentReports StartReporting()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"..\..\..\..\TestResult\";
            if (extentReports == null)
            {
                Directory.CreateDirectory(path);
                extentReports = new ExtentReports();

                var htmlReporter = new ExtentSparkReporter(path + "TestResult_" + testSchedule + ".html");
                extentReports.AttachReporter(htmlReporter);
            }

            return extentReports;
        }

        public static void CreateTest(string testName)
        {
            extentTest = StartReporting().CreateTest(testName);
        }

        public static void EndReporting()
        {
            StartReporting().Flush();
        }

        public static void LogInfo(string info)
        {
            extentTest.Info(info);
        }

        public static void LogFail(string info, string image)
        {
            extentTest.Fail(info, MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
        }

        public static void LogScreenshot(string info, string image)
        {
            extentTest.Info(info, MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
        }
    }
}
