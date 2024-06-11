using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumPOMProject.Page
{
    public abstract class RolePickerPage : BasePage
    {
        protected abstract string RolePickerDropdownField { get; }
        protected abstract string BackButton { get; }
        protected abstract string NextButton { get; }
        protected abstract string ListViewItems { get; }

        public void SelectUserRole(string userRole)
        {
            SelectElementById(RolePickerDropdownField);
            WaitForElementById(ListViewItems);
            SelectElementByResourceIdAndText(ListViewItems, userRole);
        }

        public void SelectBackButton()
        {
            SelectElementById(BackButton);
        }

        public void SelectNextButton()
        {
            SelectElementById(NextButton);
        }

        public void IsRolePickerPageDisplayed() => WaitForElementById(RolePickerDropdownField);
    }
}
