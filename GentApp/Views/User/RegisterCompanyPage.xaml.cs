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
			await SimpleIoc.Default.GetInstance<CompanyViewModel>().CreateCompany();
		}	
	}
}
