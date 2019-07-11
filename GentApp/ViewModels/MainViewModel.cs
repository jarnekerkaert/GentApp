using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GentApp.Helpers;
using GentApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace GentApp.ViewModels
{
	public class MainViewModel : ViewModelBase {
		private INavigationService _navigationService;

		public MainViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
		}

		private RelayCommand _loadedCommand;
		public RelayCommand LoadedCommand {
			get {
				return _loadedCommand
					?? (_loadedCommand = new RelayCommand(
					() => _navigationService.NavigateTo("CompaniesPage")));
			}
		}

		private readonly List<(string Tag, string Page)> _pages = new List<(string Tag, string Page)>
		{
			("Companies", nameof(CompaniesPage)),
			("Your Company", nameof(MyCompanyPage)),
			("Logout",nameof(LoginPage))
		};

		private RelayCommand<NavigationViewItemInvokedEventArgs> _navigateCommand;

		public RelayCommand<NavigationViewItemInvokedEventArgs> NavigateCommand {
			get {
				return _navigateCommand =
					new RelayCommand<NavigationViewItemInvokedEventArgs>((page) =>
						_navigationService.NavigateTo(_pages.FirstOrDefault(p => p.Tag.Equals(page.InvokedItem.ToString())).Page));
			}
		}
	}
}
