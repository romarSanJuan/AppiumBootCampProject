namespace AppiumPOMProject.AutomatedTestCase
{
    public class Tests : AppManager
    {
        [Test]
        public void Test_Case_1234_Title()
        {
            loginPage.IsLoginPageDisplayed();
            loginPage.InputEmail(userCredentials.TechnicianUserName);
            loginPage.InputPassword(userCredentials.TechnicianPassword);
            loginPage.SelectLoginButton();
        }

        [Test]
        public void Test_Case_1234_Login_Technician()
        {
            loginPage.IsLoginPageDisplayed();
            loginPage.SelectDemoModeButton();
            rolePickerPage.IsRolePickerPageDisplayed();
            rolePickerPage.SelectUserRole("Technician");
            rolePickerPage.SelectNextButton();
            doorListPage.IsDoorListPageDisplayed();
            doorListPage.SelectDoor("Demo Door ");
            doorListPage.SelectControlDoorButton();
        }
    }
}x