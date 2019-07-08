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

		public async Task<IEnumerable<Branch>> GetBranches(string id)
		{
			// TODO: aanpassen
			HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<IEnumerable<Branch>>(await response.Content.ReadAsStringAsync());
			}
			return Enumerable.Empty<Branch>();
		}

		public async Task Save(Branch branch)
		{
			try
			{
				await HttpClient.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(branch)));
			}
			catch (Exception ex)
			{
				await new MessageDialog(ex.Message).ShowAsync();
			}
		}
	}
}
