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

		public MainViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
		}

		public UserViewModel UserViewModel {
			get {
				return SimpleIoc.Default.GetInstance<UserViewModel>();
			}
		}

		public string Title {
			get {
				return "Welcome " + UserViewModel.CurrentUser.Firstname + " " + UserViewModel.CurrentUser.Lastname;
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
			("Login", nameof(LoginPage))
		};

		private RelayCommand<NavigationViewItemInvokedEventArgs> _navigateCommand;

		public RelayCommand<NavigationViewItemInvokedEventArgs> NavigateCommand {
			get {
				return _navigateCommand =
					new RelayCommand<NavigationViewItemInvokedEventArgs>((page) => {
						if ( page.InvokedItem.ToString().Equals("Logout") ) {
							UserViewModel.LogoutCommand.Execute(null);
						}
						else {
							_navigationService.NavigateTo(_pages.FirstOrDefault(p => p.Tag.Equals(page.InvokedItem.ToString())).Page);
						}
					});
			}
		}
	}
}
