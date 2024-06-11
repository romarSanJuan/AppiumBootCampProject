namespace AppiumPOMProject.Page.PageElement
{
    public class LoginPageElement : LoginPage
    {
        protected override string EmailInputField => "LoginEmail_InputField";
        protected override string PasswordInputField => "LoginPassword_InputField";
        protected override string LoginButton => "WelcomeTemplate_LoginButton";
        protected override string DemoModeButton => "WelcomeTemplate_DemoButton";
    }
}
