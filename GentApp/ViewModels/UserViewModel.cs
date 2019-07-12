using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using GentApp.Views;
using MetroLog;

namespace GentApp.ViewModels {
	public class UserViewModel : ViewModelBase {
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompaniesViewModel>();
		private readonly INavigationService _navigationService;
		private readonly UserService _userService;

		private RegisterModel _registerModel = new RegisterModel();

		public RegisterModel RegisterModel {
			get {
				return _registerModel;
			}

			set {
				_registerModel = value;
				RaisePropertyChanged(nameof(RegisterModel));
			}
		}

		private User _currentUser;
		public User CurrentUser {
			get {
				return _currentUser ?? DummyDataSource.DefaultUser;
			}

			set {
				_currentUser = value;
				RaisePropertyChanged(nameof(CurrentUser));
			}
		}

		public UserViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			_userService = new UserService();
		}

		private RelayCommand _registerCommand;

		public RelayCommand RegisterCommand {
			get {
				return _registerCommand = new RelayCommand(async () => {
					CurrentUser = await _userService.Register(RegisterModel);
					_navigationService.NavigateTo(nameof(HomePage));
				});
			}
		}

		private RelayCommand _loginCommand;

		public RelayCommand LoginCommand {
			get {
				return _loginCommand = new RelayCommand(async () => {
					CurrentUser = await _userService.Login(RegisterModel);
					_navigationService.NavigateTo(nameof(HomePage));
				});
			}
		}

		public RelayCommand ToLogin {
			get {
				return new RelayCommand(() => _navigationService.NavigateTo(nameof(LoginPage)));
			}
		}

		public RelayCommand ToRegistration {
			get {
				return new RelayCommand(() => _navigationService.NavigateTo(nameof(RegistrationPage)));
			}
		}

		public RelayCommand ToClientRegistration {
			get {
				return new RelayCommand(() => _navigationService.NavigateTo(nameof(RegisterClientPage)));
			}
		}

		public RelayCommand ToCompanyRegistration {
			get {
				return new RelayCommand(() => _navigationService.NavigateTo(nameof(RegisterCompanyPage)));
			}
		}
	}
}
