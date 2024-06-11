namespace AppiumPOMProject.Page.PageElement
{
    public class RolePickerPageElement : RolePickerPage
    {
        protected override string RolePickerDropdownField => "SelectRole_RolePicker";
        protected override string BackButton => "WizardTemplate_ButtonBack";
        protected override string NextButton => "WizardTemplate_ButtonNext";
        protected override string ListViewItems => "ListView_Items";

    }
}
