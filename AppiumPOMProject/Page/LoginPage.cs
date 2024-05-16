namespace AppiumPOMProject.Page
{
    public abstract class LoginPage : BasePage
    {
        protected abstract string EmailInputField { get; }

        public void InputEmail(string userEmail)
        {
            SendKeysToElementById(EmailInputField, userEmail);
        }
    }
}
