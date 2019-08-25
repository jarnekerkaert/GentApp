using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using GentApp.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace GentApp.ViewModels {
	public class CompanyViewModel : ViewModelBase {
		private INavigationService _navigationService;
		private readonly CompanyService _companyService;
		private readonly BranchService _branchService;
		private readonly UserService _userService;
		private bool isNavigated;

		public CompanyViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			_companyService = new CompanyService();
			_branchService = new BranchService();
			_userService = new UserService();
			_myCompany = new Company();
		}

		public UserViewModel UserViewModel {
			get {
				return SimpleIoc.Default.GetInstance<UserViewModel>();
			}
		}

		private Company _myCompany;

		public Company MyCompany {
			get { return _myCompany; }
			set {
				_myCompany = value;
				RaisePropertyChanged(nameof(MyCompany));
			}
		}

		public async Task SaveCompany() {
			try {
				await _companyService.Update(MyCompany);
				MyCompany = await _companyService.GetCompany(UserViewModel.CurrentUser.Company.Id);
			} catch ( Exception e ) {
				await new MessageDialog("Error saving company: " + e.Message).ShowAsync();
			}
		}
		public async Task CreateCompany() {
			try {
				UserViewModel.CurrentUser.Company = MyCompany;
				await UserViewModel.SaveUser("Company");
				_navigationService.NavigateTo(nameof(MyCompanyPage));
			} catch ( Exception e ) {
				await new MessageDialog("Error saving company: " + e.Message).ShowAsync();
			}
		}

		private Branch selectedBranch;

		public Branch SelectedBranch {
			get { return selectedBranch; }
			set {
				selectedBranch = value;
				RaisePropertyChanged(nameof(SelectedBranch));
			}
		}

		private RelayCommand _branchSelectedCommand;

		public RelayCommand BranchSelectedCommand {
			get {
				return _branchSelectedCommand = new RelayCommand(() => {
					if ( isNavigated && SelectedBranch == null ) {
						isNavigated = false;
					}
					else {
						isNavigated = true;
						_navigationService.NavigateTo(nameof(EditBranchPage));
					}
				});
			}
		}

		public async Task EditCompany(string name, string address) {
			MyCompany.Name = name;
			MyCompany.Address = address;

			await _companyService.Update(MyCompany);
			RaisePropertyChanged(nameof(MyCompany));
			_navigationService.NavigateTo(nameof(MyCompanyPage));
		}

		public async Task AddBranch(Branch branch) {
			MyCompany.Branches.Add(branch);
			await _companyService.Update(MyCompany);
			_navigationService.NavigateTo(nameof(MyCompanyPage));
			RaisePropertyChanged(nameof(MyCompany.Branches));
		}

		public async Task EditBranch(string name, string address, string openingHours, BranchType type) {
			SelectedBranch.Name = name;
			SelectedBranch.Address = address;
			SelectedBranch.OpeningHours = openingHours;
			SelectedBranch.Type = type;

			MyCompany.Branches[MyCompany.Branches.FindIndex(i => i.Id.Equals(SelectedBranch.Id))] = SelectedBranch;
			await _branchService.Update(SelectedBranch);
			_navigationService.NavigateTo(nameof(MyCompanyPage));
			RaisePropertyChanged(nameof(MyCompany.Branches));
		}

		public async Task UpdateBranch(List<Event> events, List<Promotion> promotions) {
			string branchId = SelectedBranch.Id;
			SelectedBranch.Events = events;
			SelectedBranch.Promotions = promotions;

			MyCompany.Branches[MyCompany.Branches.FindIndex(i => i.Id.Equals(SelectedBranch.Id))] = SelectedBranch;
			await SaveCompany();
			SelectedBranch = MyCompany.Branches.Find(b => b.Id.Equals(branchId));
		}

		public async Task DeleteBranch() {
			MyCompany.Branches.RemoveAt(MyCompany.Branches.FindIndex(i => i.Id.Equals(SelectedBranch.Id)));

			await _branchService.Delete(SelectedBranch);
			_navigationService.NavigateTo(nameof(MyCompanyPage));
			RaisePropertyChanged(nameof(MyCompany.Branches));
		}

		private RelayCommand _loadCompanyCommand;

		public RelayCommand LoadCompanyCommand {
			get {
				return _loadCompanyCommand = new RelayCommand(async () => {
					MyCompany = await _companyService.GetCompany(UserViewModel.CurrentUser.Company.Id);
					isNavigated = true;
					RaisePropertyChanged(nameof(MyCompany));
				});
			}
		}

		private RelayCommand _navigateToCompany;

		public RelayCommand NavigateToCompany {
			get {
				return _navigateToCompany = new RelayCommand(() => _navigationService.NavigateTo(nameof(MyCompanyPage)));
			}
		}

		public async void NotifySubscribers(bool isEvent)
		{
			await _branchService.NotifySubscribersEvents(SelectedBranch.Id, isEvent);
		}
	}
}
