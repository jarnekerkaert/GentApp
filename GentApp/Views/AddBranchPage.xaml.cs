using System;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GentApp.Views
{

    public sealed partial class AddBranchPage : Page
    {
        public AddBranchPage()
        {
            InitializeComponent();
			var _enumval = Enum.GetValues(typeof(BranchType)).Cast<BranchType>().ToList();
			_enumval.Remove(BranchType.NONE);
			Type.ItemsSource = _enumval;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			NameValidationErrorTextBlock.Text = "";
			OpeningHoursValidationErrorTextBlock.Text = "";
			TypeValidationErrorTextBlock.Text = "";
			AddressValidationErrorTextBlock.Text = "";
			validateInput();
		}

		private void validateInput()
		{
			var comboBoxItem = Type.SelectedValue;
			var isValid = true;
			if (Name.Text == ""){
				NameValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if(Name.Text.Length > 200)
			{
				NameValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if (OpeningHours.Text == "")
			{
				OpeningHoursValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (OpeningHours.Text.Length > 200)
			{
				OpeningHoursValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if (Address.Text == "")
			{
				AddressValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Address.Text.Length > 200)
			{
				AddressValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if (comboBoxItem == null)
			{
				TypeValidationErrorTextBlock.Text = "You have to choose a type";
				isValid = false;
			}
			if (isValid)
			{
				BranchType selectedType = (BranchType)comboBoxItem;
				Branch newBranch = new Branch() { Name = Name.Text, Address = Address.Text, OpeningHours = OpeningHours.Text, Type = selectedType, CompanyId = SimpleIoc.Default.GetInstance<CompanyViewModel>().MyCompany.Id};
				SimpleIoc.Default.GetInstance<CompanyViewModel>().AddBranch(newBranch);
				Frame.Navigate(typeof(MyCompanyPage));
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
