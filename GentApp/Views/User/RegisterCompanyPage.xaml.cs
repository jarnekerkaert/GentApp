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

		public void Save_Company(object sender, RoutedEventArgs e) {
			SimpleIoc.Default.GetInstance<CompanyViewModel>().SaveCompanyCommand.Execute("Company");
			SimpleIoc.Default.GetInstance<CompanyViewModel>().NavigateToCompany.Execute(null);
		}
	}
}
