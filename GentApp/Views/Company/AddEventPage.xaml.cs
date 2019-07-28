using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddEventPage : Page
	{
		public AddEventPage()
		{
			InitializeComponent();
			horStackPanel.DataContext = SimpleIoc.Default.GetInstance<BranchesViewModel>().MySelectedBranch;
			DataContext = SimpleIoc.Default.GetInstance<BranchViewModel>();
		}

		private void SaveEventBtn_Click(object sender, RoutedEventArgs e)
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
			if ( isValid )
			{
				Event newEvent = new Event() {
					Title = Title.Text,
					Description = Description.Text,
					StartDate = StartDatePicker.Date.Value.DateTime,
					EndDate = EndDatePicker.Date.Value.DateTime,
					Branch = SimpleIoc.Default.GetInstance<CompanyViewModel>().SelectedBranch};
				SimpleIoc.Default.GetInstance<BranchViewModel>().AddEvent(newEvent);
				Frame.Navigate(typeof(BranchEventsPage));
			}
		}
	}
}
