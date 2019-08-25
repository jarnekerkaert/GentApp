using GentApp.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace GentApp.Services {
	internal class CompanyService {
		private readonly string apiUrl = "http://localhost:50957/api/companies";
		private readonly HttpClient HttpClient;

		public CompanyService() {
			HttpClient = new HttpClient();
		}

		public async Task<IEnumerable<Company>> GetAll() {
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
			if ( response.IsSuccessStatusCode ) {
				return JsonConvert.DeserializeObject<IEnumerable<Company>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Company>();
		}

		public async Task<Company> Save(Company company) {
			try {
				var request = new HttpRequestMessage {
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl),
					Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json")
				};

				var response = await HttpClient.SendAsync(request);

				response.EnsureSuccessStatusCode();
				company.Id = response.Content.ToString();

				return company;
			}
			catch (Exception ex) {
				await new MessageDialog(ex.Message).ShowAsync();
				return company;
			}
		}

		public async Task Update(Company company)
		{
			try
			{
				var request = new HttpRequestMessage {
					Method = HttpMethod.Put,
					RequestUri = new Uri(apiUrl + "/" + company.Id),
					Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json")
				};
				await HttpClient.SendAsync(request);
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task<Company> GetCompany(string id)
		{
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/" + id);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<Company>(await response.Content.ReadAsStringAsync());
			}
			return new Company();
		}
	}
}
