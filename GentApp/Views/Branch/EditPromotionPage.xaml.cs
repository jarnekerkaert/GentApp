using GalaSoft.MvvmLight.Ioc;
using GentApp.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

namespace GentApp.Views
{

	public sealed partial class EditPromotionPage : Page
	{
		public EditPromotionPage()
		{
			InitializeComponent();
			StartDatePicker.Date = SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion.StartDate;
			EndDatePicker.Date = SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion.EndDate;
			usesCouponCheckBox.IsChecked = SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion.UsesCoupon;
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			TitleValidationErrorTextBlock.Text = "";
			TitleValidationErrorTextBlock.Visibility = Visibility.Collapsed;

			DescriptionValidationErrorTextBlock.Text = "";
			TitleValidationErrorTextBlock.Visibility = Visibility.Collapsed;

			StartDateValidationErrorTextBlock.Text = "";
			StartDateValidationErrorTextBlock.Visibility = Visibility.Collapsed;

			EndDateValidationErrorTextBlock.Text = "";
			DateValidationErrorTextBlock.Visibility = Visibility.Collapsed;

			DateValidationErrorTextBlock.Text = "";
			DateValidationErrorTextBlock.Visibility = Visibility.Collapsed;

			ValidateInput();
		}

		private void ValidateInput()
		{
			var isValid = true;
			if ( Title.Text?.Length == 0 ) {
				TitleValidationErrorTextBlock.Text = "This field is required.";
				TitleValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			else if ( Title.Text.Length > 200 ) {
				TitleValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				TitleValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			if ( Description.Text?.Length == 0 ) {
				DescriptionValidationErrorTextBlock.Text = "This field is required.";
				DescriptionValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			else if ( Description.Text.Length > 200 ) {
				DescriptionValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				DescriptionValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			if ( !StartDatePicker.Date.HasValue ) {
				StartDateValidationErrorTextBlock.Text = "This field is required.";
				StartDateValidationErrorTextBlock.Visibility = Visibility.Visible;

				isValid = false;
			}
			if ( !EndDatePicker.Date.HasValue ) {
				EndDateValidationErrorTextBlock.Text = "This field is required.";
				EndDateValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			if ( isValid )
			{
				SimpleIoc.Default.GetInstance<BranchViewModel>().EditPromotion(
					Title.Text,
					Description.Text,
					StartDatePicker.Date.Value.DateTime,
					EndDatePicker.Date.Value.DateTime,
					usesCouponCheckBox.IsChecked == true);
			}
		}

		private async void DeleteIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ContentDialog deletePromotionDialog = new ContentDialog()
			{
				Title = "Delete a promotion",
				Content = "Are you sure you want to delete this promotion?",
				PrimaryButtonText = "Yes",
				SecondaryButtonText = "No"
			};
			ContentDialogResult result = await deletePromotionDialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				SimpleIoc.Default.GetInstance<BranchViewModel>().DeletePromotion();
			}
		}
	}
}
