﻿using GalaSoft.MvvmLight.Ioc;
using GentApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace GentApp.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddEventPage : Page {
		public AddEventPage() {
			InitializeComponent();
		}

		private void SaveEventBtn_Click(object sender, RoutedEventArgs e) {
			TitleValidationErrorTextBlock.Text = "";
			DescriptionValidationErrorTextBlock.Text = "";
			StartDateValidationErrorTextBlock.Text = "";
			EndDateValidationErrorTextBlock.Text = "";
			DateValidationErrorTextBlock.Text = "";
			ValidateInput();
		}

		private void ValidateInput() {
			var isValid = true;
			if ( Title.Text?.Length == 0 ) {
				TitleValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if ( Title.Text.Length > 200 ) {
				TitleValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if ( Description.Text?.Length == 0 ) {
				DescriptionValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if ( Description.Text.Length > 200 ) {
				DescriptionValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if ( !StartDatePicker.Date.HasValue ) {
				StartDateValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			if ( !EndDatePicker.Date.HasValue ) {
				EndDateValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			if ( isValid ) {

				SimpleIoc.Default.GetInstance<BranchViewModel>().AddEvent(new DataModel.Event {
					Title = Title.Text,
					Description = Description.Text,
					StartDate = StartDatePicker.Date.Value.DateTime,
					EndDate = EndDatePicker.Date.Value.DateTime
				});
				SimpleIoc.Default.GetInstance<CompanyViewModel>().NotifySubscribers(true);
			}
		}
	}
}
