using GentApp.DataModel;
using GentApp.DataModel.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.Services
{
    public class AccountService
    {
		private readonly string apiUrl= "http://localhost:50957/api";
		private HttpClient HttpClient;

		public AccountService() {
			HttpClient = new HttpClient();
		}

		public async Task Register(RegisterModel content) {
			using ( var request = new HttpRequestMessage(HttpMethod.Post, apiUrl + "/account") ) {
				var json = JsonConvert.SerializeObject(content);
				using ( var stringContent = new StringContent(json, Encoding.UTF8, "application/json") ) {
					request.Content = stringContent;

					using ( var response = await HttpClient
						.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
						.ConfigureAwait(false) ) {
						response.EnsureSuccessStatusCode();
					}
				}
			}
		}

		public async Task<string> GetUser() {
			HttpClient.DefaultRequestHeaders.UserAgent.Clear();
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/account");
			return response.Content.ToString();
		}
	}
}
