namespace AppiumPOMProject.Page
{
    public abstract class LoginPage : BasePage
    {
            protected abstract string EmailInputField { get; }
            protected abstract string PasswordInputField { get; }
            protected abstract string LoginButton { get; }
            protected abstract string DemoModeButton { get; }

            public void InputEmail(string userEmail)
            {
                SendKeysToElementById(EmailInputField, userEmail);
            }

            public void InputPassword(string userPassword)
            {
                SendKeysToElementById(PasswordInputField, userPassword);
            }

            public void SelectLoginButton()
            {
                SelectElementById(LoginButton);
            }

            public void SelectDemoModeButton()
            {
                SelectElementById(DemoModeButton);
            }

            public void IsLoginPageDisplayed() => WaitForElementByText("Welcome to Gilgen Connect Mobile");
    }
}
