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

		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			ErrorMessage.Text = "";
			var isValid = true;
			if (UsernameTextBox.Text?.Length == 0 || FirstNameTextBox.Text?.Length == 0 || LastNameTextBox.Text?.Length == 0 || PasswordTextBox.Password?.Length == 0)
			{
				ErrorMessage.Text = "All fields must be filled in.";
				isValid = false;
			}
			else if (UsernameTextBox.Text.Length > 400 || FirstNameTextBox.Text.Length > 400 || LastNameTextBox.Text.Length > 400 || PasswordTextBox.Password.Length > 400)
			{
				ErrorMessage.Text = "The maximum length of these fields is 400 characters.";
				isValid = false;
			}
			else if (UsernameTextBox.Text.Contains(":"))
			{
				ErrorMessage.Text = "The username cannot contain the charachter ':'.";
				isValid = false;
			}
			if (isValid)
			{
				SimpleIoc.Default.GetInstance<UserViewModel>().RegisterCommand.Execute(null);
			}
		}
	}
}
