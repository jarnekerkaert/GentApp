using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
		private readonly BranchService branchService = new BranchService();
		private readonly INavigationService _navigationService;

		public CompaniesViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			companyService = new CompanyService();
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
		
		private RelayCommand _loadCommand;

		public RelayCommand LoadCommand {
			get {
				return _loadCommand ?? ( _loadCommand = new RelayCommand(async () => {
					Companies = new ObservableCollection<Company>(await companyService.GetAll());
					//MyCompany = Companies[0];
				}
				) );
			}
		}
		
		public async void RefreshCompanies() {
			Companies = new ObservableCollection<Company>(await companyService.GetAll());
			RaisePropertyChanged(nameof(Companies));
		}

		public async void DeleteBranch() {
			await branchService.Delete(SelectedBranch);
		}
	}
}
