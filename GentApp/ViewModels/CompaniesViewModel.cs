﻿using System;
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

		public CompaniesViewModel()
        {
            RetrieveCompanies();
			Branches = new ObservableCollection<Branch>(DummyDataSource.Branches);
			MyCompany = DummyDataSource.Companies[2];
            SaveCompanyCommand = new RelayCommand((p) => SaveCompany(p));
        }

		public async void RetrieveCompanies() {
			Companies = new ObservableCollection<Company>(await companyService.GetAll());
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public async void RetrieveCompanies()
		{
			HttpClient client = new HttpClient();
			var json = await client.GetStringAsync(new Uri("http://localhost:50957/api/companies"));
			var list = JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
			Companies = list;
		}

		private void SaveCompany(object p)
        {
            Companies.Add(new Company()
            {
                Name = p.ToString(),
                Address = "Dummy adres",
                OpeningHours = "24/7"
			});
        }

		public void EditCompany(string name, string address, string openingHours)
		{
			// var oldCompany = Companies.Where(x => x.Id == companyId).First();
			var oldCompany = MyCompany;
			if (oldCompany != null)
			{
				oldCompany.Name = name;
				oldCompany.Address = address;
				oldCompany.OpeningHours = openingHours;
			}
		}

		public void SaveBranch(Branch newBranch) {
			Branches.Add(newBranch);
		}

		private void NotifyPropertyChanged(String propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
