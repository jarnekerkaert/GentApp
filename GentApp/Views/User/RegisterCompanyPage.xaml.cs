using GalaSoft.MvvmLight.Ioc;
using GentApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GentApp.Views
{
    public sealed partial class RegisterCompanyPage : Page
    {
        public RegisterCompanyPage()
        {
            InitializeComponent();
        }

		public async void Save_Company(object sender, RoutedEventArgs e) {
			ErrorMessage.Text = "";
			var isValid = true;
			if (CompanyNameTextBox.Text?.Length == 0 || CompanyAddressTextBox.Text?.Length == 0)
			{
				ErrorMessage.Text = "All fields must be filled in.";
				isValid = false;
			}
			else if (CompanyNameTextBox.Text.Length > 600 || CompanyAddressTextBox.Text.Length > 600)
			{
				ErrorMessage.Text = "The maximum length of these fields is 600 characters.";
				isValid = false;
			}
			if (isValid)
			{
				await SimpleIoc.Default.GetInstance<CompanyViewModel>().SaveCompany();
				SimpleIoc.Default.GetInstance<CompanyViewModel>().NavigateToCompany.Execute(null);
			}
		}	
	}
}
