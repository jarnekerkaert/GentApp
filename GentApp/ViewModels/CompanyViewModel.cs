using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using GentApp.Views;
using MetroLog;
using System;
using System.Collections.Generic;

namespace GentApp.ViewModels {
	public class CompanyViewModel : ViewModelBase {
		private INavigationService _navigationService;
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompanyViewModel>();
		private readonly CompanyService _companyService = new CompanyService();
		private bool isNavigated;

		public CompanyViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			_companyService = new CompanyService();
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

		private RelayCommand<string> _saveCompanyCommand;

		public RelayCommand<string> SaveCompanyCommand {
			get {
				return _saveCompanyCommand = new RelayCommand<string>(name => {
					UserViewModel.CurrentUser.Company = MyCompany;
					UserViewModel.SaveUser(name);
					RaisePropertyChanged(nameof(MyCompany));
				});
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

		public void EditCompany(string name, string address, string openingHours) {
			MyCompany.Name = name;
			MyCompany.Address = address;
			MyCompany.OpeningHours = openingHours;

			SaveCompanyCommand.Execute("Company");
			_navigationService.NavigateTo(nameof(MyCompanyPage));
		}

		public void AddBranch(Branch branch) {
			MyCompany.Branches.Add(branch);

			SaveCompanyCommand.Execute("Branch");
			_navigationService.NavigateTo(nameof(MyCompanyPage));
		}

		public void EditBranch(string name, string address, string openingHours, BranchType type) {
			SelectedBranch.Name = name;
			SelectedBranch.Address = address;
			SelectedBranch.OpeningHours = openingHours;
			SelectedBranch.Type = type;

			MyCompany.Branches[MyCompany.Branches.FindIndex(i => i.Equals(SelectedBranch))] = SelectedBranch;
			SaveCompanyCommand.Execute("Branch");
			_navigationService.NavigateTo(nameof(MyCompanyPage));
		}

		public void EditBranch(List<Event> events, List<Promotion> promotions, string name) {
			SelectedBranch.Events = events;
			SelectedBranch.Promotions = promotions;

			MyCompany.Branches[MyCompany.Branches.FindIndex(i => i.Equals(SelectedBranch))] = SelectedBranch;
			SaveCompanyCommand.Execute(name);
			_navigationService.NavigateTo(nameof(EditBranchPage));
		}

		public void DeleteBranch() {
			MyCompany.Branches.RemoveAt(MyCompany.Branches.FindIndex(i => i.Equals(SelectedBranch)));

			SaveCompanyCommand.Execute("Company");
			_navigationService.NavigateTo(nameof(MyCompanyPage));
		}

		private RelayCommand _loadCompanyCommand;

		public RelayCommand LoadCompanyCommand {
			get {
				return _loadCompanyCommand = new RelayCommand(async () => {
					MyCompany = await _companyService.GetMyCompany(UserViewModel.CurrentUser.Company.Id);
					isNavigated = true;
					RaisePropertyChanged(nameof(MyCompany));
				});
			}
		}
	}
}
