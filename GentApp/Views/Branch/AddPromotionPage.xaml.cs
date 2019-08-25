using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GentApp.Views
{
	public sealed partial class AddPromotionPage : Page
	{
		public AddPromotionPage()
		{
			InitializeComponent();
		}

		private void SavePromotionBtn_Click(object sender, RoutedEventArgs e)
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
			if ( Title.Text?.Length == 0 )
			{
				TitleValidationErrorTextBlock.Text = "This field is required.";
				TitleValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			else if (Title.Text.Length > 200)
			{
				TitleValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				TitleValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			if ( Description.Text?.Length == 0 )
			{
				DescriptionValidationErrorTextBlock.Text = "This field is required.";
				DescriptionValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			else if (Description.Text.Length > 200)
			{
				DescriptionValidationErrorTextBlock.Text = "The maximum length of this field is 200 characters.";
				DescriptionValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			if (!StartDatePicker.Date.HasValue)
			{
				StartDateValidationErrorTextBlock.Text = "This field is required.";
				StartDateValidationErrorTextBlock.Visibility = Visibility.Visible;

				isValid = false;
			}
			if (!EndDatePicker.Date.HasValue)
			{
				EndDateValidationErrorTextBlock.Text = "This field is required.";
				EndDateValidationErrorTextBlock.Visibility = Visibility.Visible;
				isValid = false;
			}
			if ( isValid )
			{
				Promotion newPromotion = new Promotion() {
					Title = Title.Text,
					Description = Description.Text,
					StartDate = StartDatePicker.Date.Value.DateTime,
					EndDate = EndDatePicker.Date.Value.DateTime,
					AllBranches = false,
					UsesCoupon = usesCouponCheckBox.IsChecked == true
				};
				SimpleIoc.Default.GetInstance<BranchViewModel>().AddPromotion(newPromotion);
				SimpleIoc.Default.GetInstance<CompanyViewModel>().NotifySubscribers(false);
			}
		}
	}
}
