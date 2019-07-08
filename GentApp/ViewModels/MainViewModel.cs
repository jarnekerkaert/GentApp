using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GentApp.Helpers;
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

		private RelayCommand<NavigationViewItemInvokedEventArgs> _navigateCommand;

		public RelayCommand<NavigationViewItemInvokedEventArgs> NavigateCommand {
			get {
				return _navigateCommand =
					new RelayCommand<NavigationViewItemInvokedEventArgs>((page) =>
						_navigationService.NavigateTo(page.InvokedItem.ToString()));
			}
		}
	}
}
