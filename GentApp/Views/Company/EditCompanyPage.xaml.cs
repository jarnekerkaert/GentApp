﻿using GalaSoft.MvvmLight.Ioc;
using GentApp.ViewModels;
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
			InitializeComponent();
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ValidateInput();
		}

		private async void ValidateInput()
		{
			var isValid = true;
			if (Name.Text?.Length == 0)
			{
				NameValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Name.Text.Length > 200)
			{
				NameValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if (Address.Text?.Length == 0)
			{
				AddressValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Address.Text.Length > 200)
			{
				AddressValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if (isValid)
			{
				await SimpleIoc.Default.GetInstance<CompanyViewModel>().EditCompany(Name.Text, Address.Text);
				Frame.Navigate(typeof(MyCompanyPage));
			}
		}
	}
}
