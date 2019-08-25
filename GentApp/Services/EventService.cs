using GentApp.DataModel;
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
	class EventService
	{
		private readonly string apiUrl = "http://localhost:50957/api/events";
		private HttpClient HttpClient;

		public EventService()
		{
			HttpClient = new HttpClient();
		}

		public async Task<IEnumerable<Event>> GetAll() {
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
			if ( response.IsSuccessStatusCode ) {
				return JsonConvert.DeserializeObject<IEnumerable<Event>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Event>();
		}

		public async Task<IEnumerable<Event>> GetByBranchId(string branchId) {
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/" + branchId);
			if ( response.IsSuccessStatusCode ) {
				return JsonConvert.DeserializeObject<IEnumerable<Event>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Event>();
		}

		public async Task<Event> Add(Event newEvent)
		{
			try
			{
				var request = new HttpRequestMessage {
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl),
					Content = new StringContent(JsonConvert.SerializeObject(newEvent), Encoding.UTF8, "application/json")
				};

				var response = await HttpClient.SendAsync(request);

				return JsonConvert.DeserializeObject<Event>(response.Content.ToString());
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}

			return null;
		}

		public async Task Delete(Event eve)
		{
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Delete,
					RequestUri = new Uri(apiUrl),
					Content = new StringContent(JsonConvert.SerializeObject(eve), Encoding.UTF8, "application/json")
				};
				HttpResponseMessage responseMsg = await HttpClient.SendAsync(request);
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task Update(Event updatedEvent)
		{
			try
			{
				var request = new HttpRequestMessage {
					Method = HttpMethod.Put,
					RequestUri = new Uri(apiUrl + "/" + updatedEvent.Id),
					Content = new StringContent(JsonConvert.SerializeObject(updatedEvent), Encoding.UTF8, "application/json")
				};
				await HttpClient.SendAsync(request);
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}
	}
}
