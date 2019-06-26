using GentApp.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddBranchPage : Page
    {
        public AddBranchPage()
        {
            this.InitializeComponent();
			this.DataContext = MainPage.ViewModel;
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
			if (isValid == true)
			{
				BranchType selectedType = (BranchType)comboBoxItem;
				Branch newBranch = new Branch() { Name = Name.Text, Address = Address.Text, OpeningHours = OpeningHours.Text, Type = selectedType };
				MainPage.ViewModel.SaveBranch(newBranch);
				// TODO: navigate through navigationService
				// TODO: send notification
				Frame.Navigate(typeof(MyCompanyPage));
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
