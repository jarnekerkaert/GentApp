using GalaSoft.MvvmLight.Ioc;
using GentApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GentApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterClientPage : Page
    {
        public RegisterClientPage()
        {
            InitializeComponent();
		}

		public void ToLoginPage(object sender, RoutedEventArgs args) {
			SimpleIoc.Default.GetInstance<UserViewModel>().ToLogin.Execute(sender);
		}
	}
}
