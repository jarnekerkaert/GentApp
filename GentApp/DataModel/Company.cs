using GentApp.Helpers;
using System.Collections.Generic;

namespace GentApp.DataModel {
    public class Company
    {
		public string Name { get; set; }
		public string Address { get; set; }
		public string Id { get; set; }
		public List<Branch> Branches { get; set; }
		public string ImageUri { get; set; }

		public Company(string name, string address) 
			: this() {
			Name = name;
			Address = address;
		}

		public Company() {
			ImageUri = RandomAsset.getRandomAsset();
		}
    }
}
