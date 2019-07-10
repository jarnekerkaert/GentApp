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
	class PromotionService
	{
		private ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<PromotionService>();
		private readonly string apiUrl = "http://localhost:50957/api/promotions";
		private HttpClient HttpClient;

		public PromotionService()
		{
			HttpClient = new HttpClient();
		}

		public async Task Delete(Promotion promotion)
		{
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Delete,
					RequestUri = new Uri(apiUrl),
					Content = new StringContent(JsonConvert.SerializeObject(promotion), Encoding.UTF8, "application/json")
				};
				HttpResponseMessage responseMsg = await HttpClient.SendAsync(request);
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task Save(Promotion promotion)
		{
			try
			{
				var response = await HttpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(promotion), System.Text.Encoding.UTF8, "application/json"));
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}
	}
}
