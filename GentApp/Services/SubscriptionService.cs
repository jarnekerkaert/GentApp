﻿using GentApp.DataModel;
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
	class SubscriptionService
	{
		private readonly string apiUrl = "http://localhost:50957/api/subscriptions";
		private HttpClient HttpClient;

		public SubscriptionService()
		{
			HttpClient = new HttpClient();
		}

		public async Task Subscribe(Subscription subscription)
		{
			try
			{
				var response = await HttpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(subscription), Encoding.UTF8, "application/json"));
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task<IEnumerable<Subscription>> GetSubscriptions(string userId)
		{
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/user/" + userId);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<IEnumerable<Subscription>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Subscription>();
		}

		public async Task Unsubscribe(string subscriptionId)
		{
			try
			{
				HttpResponseMessage responseMsg = await HttpClient.DeleteAsync(apiUrl + "/" + subscriptionId);
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}

		public async Task Update(Subscription subscription)
		{
			try
			{
				var response = await HttpClient.PutAsync(apiUrl + "/" + subscription.Id, new StringContent(JsonConvert.SerializeObject(subscription), Encoding.UTF8, "application/json"));
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}
	}
}
