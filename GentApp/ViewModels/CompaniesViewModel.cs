using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using MetroLog;
using Newtonsoft.Json;

namespace GentApp.ViewModels {
	public class CompaniesViewModel : ViewModelBase {
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompaniesViewModel>();
		private readonly CompanyService companyService = new CompanyService();

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

		public RelayCommand SaveCompanyCommand { get; set; }

		private RelayCommand _companySelectedCommand;

		public RelayCommand CompanySelectedCommand {
			get {
				_companySelectedCommand = new RelayCommand((_) => log.Info(SelectedCompany.Name + " selected"));
				return _companySelectedCommand;
			}
		}

		public RelayCommand BranchSelectedCommand { get; set; }


		public CompaniesViewModel() {
			RetrieveCompanies();
			MyCompany = DummyDataSource.Companies[2];
			SaveCompanyCommand = new RelayCommand(SaveCompany);
		}

		public async void RetrieveCompanies() {
			Companies = new ObservableCollection<Company>(await companyService.GetAll());
		}

		private void SaveCompany(object p) {
			Companies.Add(new Company() {
				Name = p.ToString(),
				Address = "Dummy adres",
				OpeningHours = "24/7"
			});
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
				return _loadCommand ?? (_loadCommand = new RelayCommand(async (_) => Companies = new ObservableCollection<Company>(await companyService.GetAll())));
			}
		}
	}
}
