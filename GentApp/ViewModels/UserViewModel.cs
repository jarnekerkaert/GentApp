using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using GentApp.Views;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace GentApp.ViewModels {
	public class UserViewModel : ViewModelBase {
		private readonly INavigationService _navigationService;
		private readonly UserService _userService;

		private RegisterModel _registerModel = new RegisterModel();
		private bool _registerCompany;

		public RegisterModel RegisterModel {
			get {
				return _registerModel;
			}

			set {
				_registerModel = value;
				RaisePropertyChanged(nameof(RegisterModel));
			}
		}

		private LoginModel _loginModel = new LoginModel();

		public LoginModel LoginModel {
			get {
				return _loginModel;
			}

			set {
				_loginModel = value;
				RaisePropertyChanged(nameof(LoginModel));
			}
		}

		private User _currentUser;
		public User CurrentUser {
			get {
				return _currentUser ?? new User("", "");
			}

			set {
				_currentUser = value;
				RaisePropertyChanged(nameof(CurrentUser));
			}
		}

		public async Task SaveUser(string name) {
			try {
				CurrentUser = await _userService.Update(CurrentUser);

				RaisePropertyChanged(nameof(IsEntrepreneur));
				RaisePropertyChanged(nameof(CurrentUser));

				if ( name?.Equals("") != false )
					name = "User";
				await new MessageDialog(name + " saved!").ShowAsync();
			} catch ( Exception e ) {
				await new MessageDialog("Error saving user: " + e.Message).ShowAsync();
			}
		}

		public bool LoggedIn {
			get {
				return CurrentUser.Id != null || CurrentUser == null;
			}
		}

		public bool IsEntrepreneur {
			get {
				return CurrentUser.Company != null;
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
					try {
						if ( await _userService.CheckUsername(RegisterModel.UserName) ) {

							CurrentUser = await _userService.Register(RegisterModel);
							if ( _registerCompany )
								_navigationService.NavigateTo(nameof(RegisterCompanyPage));
							else
								_navigationService.NavigateTo(nameof(HomePage));
							RaisePropertyChanged(nameof(LoggedIn));
							await new MessageDialog("Registered!").ShowAsync();
						}
						else {
							await new MessageDialog("User already taken!").ShowAsync();
						}
					} catch ( Exception e ) {
						await new MessageDialog(e.Message).ShowAsync();
					}
				});
			}
		}

		private RelayCommand _loginCommand;

		public RelayCommand LoginCommand {
			get {
				return _loginCommand = new RelayCommand(async () => {
					try {
						if ((LoginModel.UserName == null || LoginModel.UserName.Trim().Equals("")) || LoginModel.Password == null)
						{
							await new MessageDialog("The fields can't be empty.").ShowAsync();
						}
						else
						{
							CurrentUser = await _userService.Login(LoginModel);
							_navigationService.NavigateTo(nameof(HomePage));
							RaisePropertyChanged(nameof(LoggedIn));
							await new MessageDialog("Logged in!").ShowAsync();
						}
					} catch ( Exception e ) {
						if (e.Message.Contains("Unauthorized"))
						{
							await new MessageDialog("The login was incorrect.").ShowAsync();
						}
						else if (e.Message.Contains("Found"))
						{
							await new MessageDialog("The user was not found.").ShowAsync();
						}
						else
						{
							await new MessageDialog("Something went wrong..").ShowAsync();
						}
					}
				});
			}
		}

		private RelayCommand _logoutCommand;

		public RelayCommand LogoutCommand {
			get {
				return _logoutCommand = new RelayCommand(async () => {
					CurrentUser = null;
					RaisePropertyChanged(nameof(LoggedIn));
					_navigationService.NavigateTo(nameof(HomePage));
					await new MessageDialog("Logged out").ShowAsync();
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
				return new RelayCommand(() =>
				{
					_registerCompany = false;
					_navigationService.NavigateTo(nameof(RegisterClientPage));
				});
			}
		}

		public RelayCommand ToCompanyRegistration {
			get {
				return new RelayCommand(() => {
					_registerCompany = true;
					_navigationService.NavigateTo(nameof(RegisterClientPage));
				});
			}
		}
	}
}
