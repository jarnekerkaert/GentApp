using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace GentApp.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class EditBranchPage : Page {
		public EditBranchPage() {
			InitializeComponent();
			var _enumval = Enum.GetValues(typeof(BranchType)).Cast<BranchType>().ToList();
			_enumval.Remove(BranchType.NONE);
			Type.ItemsSource = _enumval;
			Type.SelectedItem = SimpleIoc.Default.GetInstance<CompanyViewModel>().SelectedBranch.Type;
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e) {
			NameValidationErrorTextBlock.Text = "";
			OpeningHoursValidationErrorTextBlock.Text = "";
			TypeValidationErrorTextBlock.Text = "";
			AddressValidationErrorTextBlock.Text = "";
			validateInput();
		}

		private void validateInput() {
			var comboBoxItem = Type.SelectedValue;
			var isValid = true;
			if ( Name.Text?.Length == 0 ) {
				NameValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if ( Name.Text.Length > 200 ) {
				NameValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if ( OpeningHours.Text?.Length == 0 ) {
				OpeningHoursValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if ( OpeningHours.Text.Length > 200 ) {
				OpeningHoursValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if ( Address.Text?.Length == 0 ) {
				AddressValidationErrorTextBlock.Text = "This field is required.";
				isValid = false;
			}
			else if ( Address.Text.Length > 200 ) {
				AddressValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				isValid = false;
			}
			if ( comboBoxItem == null ) {
				TypeValidationErrorTextBlock.Text = "You have to choose a type";
				isValid = false;
			}
			if ( isValid ) {
				SimpleIoc.Default.GetInstance<CompanyViewModel>().EditBranch(
					Name.Text,
					Address.Text,
					OpeningHours.Text,
					(BranchType) comboBoxItem);
			}
		}

		private async void DeleteIcon_Tapped(object sender, TappedRoutedEventArgs e) {
			ContentDialog deleteBranchDialog = new ContentDialog() {
				Title = "Delete a branch",
				Content = "Are you sure you want to delete this branch?",
				PrimaryButtonText = "Yes",
				SecondaryButtonText = "No"
			};
			ContentDialogResult result = await deleteBranchDialog.ShowAsync();
			if ( result == ContentDialogResult.Primary ) {
				SimpleIoc.Default.GetInstance<CompanyViewModel>().DeleteBranch();
			}
		}

		private void Promotions_Click(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(BranchPromotionsPage));
		}

		private void Events_Click(object sender, RoutedEventArgs e) {
			Frame.Navigate(typeof(BranchEventsPage));
		}
	}
}
