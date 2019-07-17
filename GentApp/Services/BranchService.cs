using GentApp.DataModel;
using MetroLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace GentApp.Services
{
	class BranchService
	{
		private ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<BranchService>();
		private readonly string apiUrl = "http://localhost:50957/api/branches";
		private HttpClient HttpClient;

		public BranchService()
		{
			HttpClient = new HttpClient();
		}

		public async Task<IEnumerable<Branch>> GetBranches()
		{
			// TODO: aanpassen
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<IEnumerable<Branch>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Branch>();
		}

		public async Task<IEnumerable<Branch>> GetBranchesOfCompany(string id) {
			// TODO: aanpassen
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
			if ( response.IsSuccessStatusCode ) {
				return JsonConvert.DeserializeObject<IEnumerable<Branch>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Branch>();
		}

		public async Task Save(Branch branch)
		{
			try
			{
				var response = await HttpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json"));
				Console.WriteLine("test");
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task Delete(Branch branch)
		{
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Delete,
					RequestUri = new Uri(apiUrl),
					Content = new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json")
				};
				HttpResponseMessage responseMsg = await HttpClient.SendAsync(request);
				//var response = await HttpClient.DeleteAsync(apiUrl + "/" + branch.Id);
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task<IEnumerable<Promotion>> GetPromotions(string id)
		{
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/" + id + "/promotions");
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<IEnumerable<Promotion>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Promotion>();
		}
	}
}
