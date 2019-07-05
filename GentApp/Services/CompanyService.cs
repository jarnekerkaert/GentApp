﻿using GentApp.DataModel;
using MetroLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.Services {
	class CompanyService {
		private ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompanyService>();
		private readonly string apiUrl = "http://localhost:50957/api/companies";
		private HttpClient HttpClient;

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
	}
}
