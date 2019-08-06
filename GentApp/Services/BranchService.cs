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

		public async Task<IEnumerable<Branch>> GetAll()
		{
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<IEnumerable<Branch>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Branch>();
		}

		public async Task<Branch> Save(Branch branch)
		{
			try
			{
				var request = new HttpRequestMessage {
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl),
					Content = new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json")
				};

				var response = await HttpClient.SendAsync(request);

				return JsonConvert.DeserializeObject<Branch>(response.Content.ToString());
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}

			return null;
		}

		public async Task Update(Branch branch) {
			try {
				var request = new HttpRequestMessage {
					Method = HttpMethod.Put,
					RequestUri = new Uri(apiUrl + "/" + branch.Id),
					Content = new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json")
				};
				await HttpClient.SendAsync(request);

			} catch ( Exception ex ) {
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
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task NotifySubscribersEvents(string id, bool isEvent)
		{
			try
			{
				var response = await HttpClient.PutAsync(apiUrl + "/" + id + "/notifysubscribers", new StringContent(JsonConvert.SerializeObject(isEvent), System.Text.Encoding.UTF8, "application/json"));
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

		public async Task<IEnumerable<Event>> GetEvents(string id)
		{
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/" + id + "/events");
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<IEnumerable<Event>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Event>();
		}
	}
}
