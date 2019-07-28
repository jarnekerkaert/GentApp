using GentApp.Helpers;
using System.Collections.Generic;

namespace GentApp.DataModel {
    public class Company
    {
		public string Name { get; set; }
		public string Address { get; set; }
		public string OpeningHours { get; set; }
		public string Id { get; set; }
		public ICollection<Branch> Branches { get; set; }
		public string ImageUri { get; set; }

		public Company(string name, string address, string openingHours) 
			: this() {
			Name = name;
			Address = address;
			OpeningHours = openingHours;
		}

		public Company() {
			ImageUri = RandomAsset.getRandomAsset();
		}
    }
}
