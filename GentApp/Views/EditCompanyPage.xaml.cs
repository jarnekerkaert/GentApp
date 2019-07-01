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

namespace GentApp.Views
{
	public sealed partial class EditCompanyPage : Page
	{
		public EditCompanyPage()
		{
			this.InitializeComponent();
			this.DataContext = MainPage.ViewModel.MyCompany;
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			validateInput();
		}

		private void validateInput()
		{
			var isValid = true;
			if (Name.Text == "")
			{
				NameValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Name.Text.Length > 200)
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
			if (isValid == true)
			{
				MainPage.ViewModel.MyCompany.Name = Name.Text;
				MainPage.ViewModel.MyCompany.Address = Address.Text;
				MainPage.ViewModel.MyCompany.OpeningHours = OpeningHours.Text;
				//MainPage.ViewModel.EditCompany(MainPage.ViewModel.MyCompany.Id, )
			}
		}
	}
}
