using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using MetroLog;
using Newtonsoft.Json;

namespace GentApp.ViewModels
{
    public class CompaniesViewModel : INotifyPropertyChanged
	{
		private ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompaniesViewModel>();
		public ObservableCollection<Company> Companies { get; set; }
		public ObservableCollection<Branch> Branches { get; set; }

		private readonly CompanyService companyService = new CompanyService();

		private Company mySelectedCompany;
		public Company MySelectedCompany
		{
			get { return mySelectedCompany; }
			set
			{
				if (value != mySelectedCompany)
				{
					mySelectedCompany = value;
					NotifyPropertyChanged("MySelectedCompany");
				}
			}
		}

		private Company myCompany;
		public Company MyCompany
		{
			get { return myCompany; }
			set
			{
				if (value != myCompany)
				{
					myCompany = value;
					NotifyPropertyChanged("MyCompany");
				}
			}
		}

		private Branch mySelectedBranch;
		public Branch MySelectedBranch
		{
			get { return mySelectedBranch; }
			set
			{
				if (value != mySelectedBranch)
				{
					mySelectedBranch = value;
					NotifyPropertyChanged("MySelectedBranch");
				}
			}
		}

		public RelayCommand SaveCompanyCommand { get; set; }
		public RelayCommand SaveBranchCommand { get; set; }

		public CompaniesViewModel()
        {
            RetrieveCompanies();
			Branches = new ObservableCollection<Branch>(DummyDataSource.Branches);
			MyCompany = DummyDataSource.Companies[2];
            SaveCompanyCommand = new RelayCommand((p) => SaveCompany(p));
			SaveBranchCommand = new RelayCommand((p) => SaveBranch(p));
        }

		public async void RetrieveCompanies() {
			Companies = new ObservableCollection<Company>(await companyService.GetAll());
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void SaveCompany(object p)
        {
            Companies.Add(new Company()
            {
                Name = p.ToString(),
                Address = "Dummy adres",
                OpeningHours = "24/7"
			});
        }

		private void SaveBranch(object p)
		{
			//this.Branches.Add(new Branch()
			//{
			//	Name = p.ToString(),
			//	Address = "Dummy adres",
			//	OpeningHours = "24/7"
			//});
			Branches.Add(p as Branch);
		}

		public void SaveBranch(Branch newBranch)
		{
			Branches.Add(newBranch);
		}

		public void EditCompany(int companyId, Company updatedCompany)
		{
			var oldCompany = Companies.Where(x => x.Id == companyId).First();
			oldCompany.Name = updatedCompany.Name;
			oldCompany.Address = updatedCompany.Address;
			oldCompany.OpeningHours = updatedCompany.OpeningHours;
		}

		private void NotifyPropertyChanged(String propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
