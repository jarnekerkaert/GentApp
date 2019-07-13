using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Views;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace GentApp.ViewModels {
	public class MainViewModel : ViewModelBase {
		private INavigationService _navigationService;
		private readonly UserViewModel _userViewModel;

		public MainViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			_userViewModel = SimpleIoc.Default.GetInstance<UserViewModel>();
		}

		public string Title {
			get {
				return "Welcome " + User.Firstname + " " + User.Lastname;
			}
		}

		public User User {
			get {
				return _userViewModel.CurrentUser;
			}
		}

		private RelayCommand _loadCommand;
		public RelayCommand LoadCommand {
			get {
				return _loadCommand
					?? ( _loadCommand = new RelayCommand(
					() => _navigationService.NavigateTo(nameof(HomePage))) );
			}
		}

		private readonly List<(string Tag, string Page)> _pages = new List<(string Tag, string Page)>
		{
			("Home", nameof(HomePage)),
			("Companies", nameof(CompaniesPage)),
			("Your Company", nameof(MyCompanyPage)),
			("Login", nameof(LoginPage)),
			("Logout",nameof(LoginPage))
		};

		private RelayCommand<NavigationViewItemInvokedEventArgs> _navigateCommand;

		public RelayCommand<NavigationViewItemInvokedEventArgs> NavigateCommand {
			get {
				return _navigateCommand =
					new RelayCommand<NavigationViewItemInvokedEventArgs>((page) => {
						if(page.Equals("Logout")) {
							_navigationService.NavigateTo("HomePage");
						}
						else
							_navigationService.NavigateTo(_pages.FirstOrDefault(p => p.Tag.Equals(page.InvokedItem.ToString())).Page);
					});
			}
		}
	}
}
