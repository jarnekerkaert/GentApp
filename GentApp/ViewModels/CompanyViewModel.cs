﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using GentApp.Views;
using MetroLog;

namespace GentApp.ViewModels {
	public class CompanyViewModel : ViewModelBase {
		private INavigationService _navigationService;
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompanyViewModel>();
		private readonly CompanyService companyService = new CompanyService();
		private readonly BranchService branchService = new BranchService();

		public CompanyViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			companyService = new CompanyService();
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

		private RelayCommand _saveCompanyCommand;

		public RelayCommand SaveCompanyCommand {
			get {
				return _saveCompanyCommand = new RelayCommand(() => {
					UserViewModel.CurrentUser.Company = MyCompany;
					UserViewModel.SaveUser();
					RaisePropertyChanged(nameof(MyCompany));
					_navigationService.NavigateTo(nameof(HomePage));
				});
			}
		}

		private Branch selectedBranch;

		public Branch SelectedBranch {
			get { return selectedBranch; }
			set {
				if ( value != selectedBranch ) {
					selectedBranch = value;
					RaisePropertyChanged(nameof(SelectedBranch));
				}
			}
		}

		private RelayCommand _branchSelectedCommand;

		public RelayCommand BranchSelectedCommand
		{
			get
			{
				return _branchSelectedCommand = new RelayCommand(() => _navigationService.NavigateTo("EditBranchPage"));
			}
		}

		public async void EditCompany(string name, string address, string openingHours) {
			MyCompany.Name = name;
			MyCompany.Address = address;
			MyCompany.OpeningHours = openingHours;

			await companyService.Update(MyCompany);
			RaisePropertyChanged(nameof(MyCompany));
		}

		//public async void SaveBranch() {
		//	//MyCompany.Branches.Add(SelectedBranch);
		//	await companyService.Save(MyCompany);
		//}

		public async void EditBranch(string name, string address, string openingHours, BranchType type) {
			SelectedBranch.Name = name;
			SelectedBranch.Address = address;
			SelectedBranch.OpeningHours = openingHours;
			SelectedBranch.Type = type;
			//var oldBranch = MyCompany.Branches.Where(b => b.Id.Equals(SelectedBranch.Id)).First();
			//oldBranch = SelectedBranch;
			await companyService.Update(MyCompany);
			RaisePropertyChanged(nameof(MyCompany));
		}

		public async void AddBranch(Branch branch) {
			//MyCompany.Branches.Add(branch);
			await branchService.Save(branch);
		}

		//public async void AddBranch(Branch branch)
		//{
		//	await branchService.Save(branch);
		//	//MyCompany.Branches.Add(branch);
		//	//await companyService.Update(MyCompany);
		//}

		private RelayCommand _loadCompanyCommand;

		public RelayCommand LoadCompanyCommand {
			get {
				return _loadCompanyCommand = new RelayCommand(async () => {
					//MyCompany = UserViewModel.CurrentUser.Company;
					MyCompany = await companyService.GetMyCompany(UserViewModel.CurrentUser.Company.Id);
					RaisePropertyChanged(nameof(MyCompany));
				});
			}
		}

		public async void DeleteBranch()
		{
			await branchService.Delete(SelectedBranch);
			RaisePropertyChanged(nameof(MyCompany));
		}

	}
}