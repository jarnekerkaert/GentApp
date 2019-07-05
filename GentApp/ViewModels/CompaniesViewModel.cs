using GentApp.DataModel;
using GentApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.ViewModels
{
    public class CompaniesViewModel : INotifyPropertyChanged
	{
        public ObservableCollection<Company> Companies { get; set; }
		public ObservableCollection<Branch> Branches { get; set; }

		private Company mySelectedCompany;
		public Company MySelectedCompany
		{
			get { return mySelectedCompany; }
			set
			{
				if (value != mySelectedCompany)
				{
					mySelectedCompany = value; NotifyPropertyChanged("MySelectedCompany");
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
					myCompany = value; NotifyPropertyChanged("MyCompany");
				}
			}
		}

		public RelayCommand SaveCompanyCommand { get; set; }

		public CompaniesViewModel()
        {
            Companies = new ObservableCollection<Company>(DummyDataSource.Companies);
			MyCompany = DummyDataSource.Companies[2];
            SaveCompanyCommand = new RelayCommand((p) => SaveCompany(p));
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

		private void NotifyPropertyChanged(String propertyName) {
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
