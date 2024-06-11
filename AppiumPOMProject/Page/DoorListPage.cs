namespace AppiumPOMProject.Page
{
    public abstract class DoorListPage : BasePage
    {
        protected abstract string ControlDoorButton { get; }

        public void SelectDoor(string doorName)
        {
            WaitForElementByText(doorName);
            GetElementByText(doorName).Click();
        }

        public void SelectControlDoorButton()
        {
            WaitForElementByText("Device connected");
            GetElementById(ControlDoorButton).Click();
        }

        public void IsDoorListPageDisplayed() => WaitForElementByText("Door list");
    }
}
