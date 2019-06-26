using GentApp.DataModel;
using GentApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.ViewModels
{
    class CompaniesViewModel
    {
		private const string apiUrl = "http://localhost:29062//api/Companies";
		private HttpClient client;

		public ObservableCollection<Company> Companies { get; set; }
        public RelayCommand SaveCompanyCommand { get; set; }
        public CompaniesViewModel()
        {
			client = new HttpClient();
			Companies = new ObservableCollection<Company>(DummyDataSource.Companies);
            SaveCompanyCommand = new RelayCommand(SaveCompany);
        }

		private async void LoadCompanies() {
			var response = await client.GetStringAsync(new Uri(apiUrl));
			Companies = JsonConvert.DeserializeObject<ObservableCollection<Company>>(response);
		}

        private void SaveCompany(object p)
        {
            Companies.Add(new Company()
            {
                Name = p.ToString(),
                Address = "Dummy adres",
                Openingsuren = "24/7",
                Type = CompanyType.MEDIUM
            });
        }
    }
}
