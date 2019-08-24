using GentApp.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.Services {
	public class UserService {
		private readonly string apiUrl = "http://localhost:50957/api/users";
		private HttpClient HttpClient;

		public UserService() {
			HttpClient = new HttpClient();
		}

		public async Task<User> Register(RegisterModel content) {
			using ( var request = new HttpRequestMessage(HttpMethod.Post, apiUrl + "/register") ) {
				var json = JsonConvert.SerializeObject(content);
				using ( var stringContent = new StringContent(json, Encoding.UTF8, "application/json") ) {
					request.Content = stringContent;
					using ( var response = await HttpClient
						.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
						.ConfigureAwait(false) ) {
						response.EnsureSuccessStatusCode();
						return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
					}
				}
			}
		}

		public async Task<User> Login(LoginModel loginModel) {
			byte[] usernamePasswordBytes = System.Text.Encoding.UTF8.GetBytes(loginModel.UserName + ":" + loginModel.Password);
			var authToken = Convert.ToBase64String(usernamePasswordBytes);

			HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/login/" + loginModel.UserName);
			response.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
		}

		public async Task<User> GetUser(string userId) {
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/" + userId);
			response.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
		}

		public async Task<User> Update(User content) {
			using ( var request = new HttpRequestMessage(HttpMethod.Put, apiUrl + "/" + content.Id) ) {
				var json = JsonConvert.SerializeObject(content);
				using ( var stringContent = new StringContent(json, Encoding.UTF8, "application/json") ) {
					request.Content = stringContent;
					using ( var response = await HttpClient
						.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
						.ConfigureAwait(false) ) {
						response.EnsureSuccessStatusCode();
						return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
					}
				}
			}
		}

		public async Task<IEnumerable<Branch>> GetSubscribedBranches(string id)
		{
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl + "/" + id + "/subscribedbranches");
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<IEnumerable<Branch>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Branch>();
		}
	}
}
