using AppiumPOMProject.Page.PageElement;

namespace AppiumPOMProject.AutomatedTestCase
{
    public class Tests : AppManager
    {
        [Test]
        public void Test_Case_1234_Title()
        {
            LoginPageElement loginPage = new();
            loginPage.InputEmail("test");
        }
    }
}