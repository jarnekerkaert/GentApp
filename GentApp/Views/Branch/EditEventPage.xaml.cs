using GalaSoft.MvvmLight.Ioc;
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
	public sealed partial class EditEventPage : Page
	{
		public EditEventPage()
		{
			InitializeComponent();
			StartDatePicker.Date = SimpleIoc.Default.GetInstance<BranchViewModel>().SelectedEvent.StartDate;
			EndDatePicker.Date = SimpleIoc.Default.GetInstance<BranchViewModel>().SelectedEvent.EndDate;
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			TitleValidationErrorTextBlock.Text = "";
			DescriptionValidationErrorTextBlock.Text = "";
			StartDateValidationErrorTextBlock.Text = "";
			EndDateValidationErrorTextBlock.Text = "";
			DateValidationErrorTextBlock.Text = "";
			ValidateInput();
		}

		private async void ValidateInput()
		{
			var isValid = true;
			if ( Title.Text?.Length == 0 )
			{
				TitleValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Title.Text.Length > 200)
			{
				TitleValidationErrorTextBlock.Text = "The maximum length of this field is 600 characters.";
				isValid = false;
			}
			if ( Description.Text?.Length == 0 )
			{
				DescriptionValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if (Description.Text.Length > 200)
			{
				DescriptionValidationErrorTextBlock.Text = "The maximum length of this field is 1000 characters.";
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
			if ( isValid )
			{
				await SimpleIoc.Default.GetInstance<BranchViewModel>().EditEvent(
					Title.Text, 
					Description.Text,
					StartDatePicker.Date.Value.DateTime, 
					EndDatePicker.Date.Value.DateTime);
			}
		}

		private async void DeleteIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ContentDialog deleteEventDialog = new ContentDialog()
			{
				Title = "Delete an event",
				Content = "Are you sure you want to delete this event?",
				PrimaryButtonText = "Yes",
				SecondaryButtonText = "No"
			};
			ContentDialogResult result = await deleteEventDialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				await SimpleIoc.Default.GetInstance<BranchViewModel>().DeleteEvent();
			}
		}
	}
}
