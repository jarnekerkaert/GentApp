using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddPromotionPage : Page
	{
		public AddPromotionPage()
		{
			this.InitializeComponent();
			horStackPanel.DataContext = SimpleIoc.Default.GetInstance<BranchesViewModel>().MySelectedBranch;
			this.DataContext = SimpleIoc.Default.GetInstance<BranchViewModel>();

		}

		private void SavePromotionBtn_Click(object sender, RoutedEventArgs e)
		{
			TitleValidationErrorTextBlock.Text = "";
			DescriptionValidationErrorTextBlock.Text = "";
			StartDateValidationErrorTextBlock.Text = "";
			EndDateValidationErrorTextBlock.Text = "";
			DateValidationErrorTextBlock.Text = "";
			validateInput();
		}

		private void validateInput()
		{
			var isValid = true;
			if (Title.Text == "")
			{
				TitleValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Title.Text.Length > 200)
			{
				TitleValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if (Description.Text == "")
			{
				DescriptionValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Description.Text.Length > 200)
			{
				DescriptionValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if (!StartDatePicker.Date.HasValue)
			{
				StartDateValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			if (!EndDatePicker.Date.HasValue)
			{
				EndDateValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			if (isValid == true)
			{
				Promotion newPromotion = new Promotion() { Title = Title.Text, Description = Description.Text, StartDate = StartDatePicker.Date.Value.DateTime, EndDate = EndDatePicker.Date.Value.DateTime, BranchId = SimpleIoc.Default.GetInstance<CompanyViewModel>().SelectedBranch.Id, AllBranches = false };
				SimpleIoc.Default.GetInstance<BranchViewModel>().AddPromotion(newPromotion);
				Frame.Navigate(typeof(BranchPromotionsPage));
			}
		}
	}
}
