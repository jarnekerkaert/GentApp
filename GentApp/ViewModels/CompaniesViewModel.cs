using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using MetroLog;

namespace GentApp.ViewModels {
	public class CompaniesViewModel : ViewModelBase {
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompaniesViewModel>();
		private readonly CompanyService companyService = new CompanyService();
		private readonly INavigationService _navigationService;

		public CompaniesViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			MyCompany = DummyDataSource.Companies[2];
		}

		private ObservableCollection<Company> _companies;
		public ObservableCollection<Company> Companies {
			get {
				return _companies;
			}

			set {
				_companies = value;
				RaisePropertyChanged(nameof(Companies));
			}
		}

		public Company SelectedCompany { get; set; }

		private Company myCompany;

		public Company MyCompany {
			get { return myCompany; }
			set {
				if (value != myCompany) {
					myCompany = value;
					RaisePropertyChanged(nameof(MyCompany));
				}
			}
		}

		private Branch selectedBranch;

		public Branch SelectedBranch {
			get { return selectedBranch; }
			set {
				if (value != selectedBranch) {
					selectedBranch = value;
					RaisePropertyChanged(nameof(SelectedBranch));
				}
			}
		}

		private RelayCommand _saveCompanyCommand;

		public RelayCommand SaveCompanyCommand {
			get {
				return _saveCompanyCommand = new RelayCommand(() => Companies.Add(new Company() {
					Name = SelectedCompany.Name,
					Address = "Dummy adres",
					OpeningHours = "24/7"
				}));
			}
		}

		private RelayCommand _companySelectedCommand;

		public RelayCommand CompanySelectedCommand {
			get {
				return _companySelectedCommand = new RelayCommand(() => _navigationService.NavigateTo("CompanyDetailsPage"));
			}
		}
		private RelayCommand _branchSelectedCommand;

		public RelayCommand BranchSelectedCommand {
			get {
				return _branchSelectedCommand = new RelayCommand(() => _navigationService.NavigateTo("BranchDetailsPage"));
			}
		}

		public void EditCompany(string name, string address, string openingHours) {
			// var oldCompany = Companies.Where(x => x.Id == companyId).First();
			var oldCompany = MyCompany;
			if (oldCompany != null) {
				oldCompany.Name = name;
				oldCompany.Address = address;
				oldCompany.OpeningHours = openingHours;
			}
		}

		public async void SaveBranch() {
			SelectedCompany.Branches.Add(SelectedBranch);
			await companyService.Save(SelectedCompany);
		}

		private RelayCommand _loadCommand;

		public RelayCommand LoadCommand {
			get {
				return _loadCommand ?? (_loadCommand = new RelayCommand(async () => {
					Companies = new ObservableCollection<Company>(await companyService.GetAll());
					MyCompany = Companies[0];
				}
				));
			}
		}
	}
}
