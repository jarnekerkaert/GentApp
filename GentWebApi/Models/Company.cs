using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GentApp.Models
{
    public class Company
    {
		public string Name { get; set; }
		public string Address { get; set; }
		public string OpeningHours { get; set; }
		
		public string Id { get; set; }
		public IEnumerable<Branch> Branches { get; set; }

		public Company(string name, string address, string openingHours) {
			Name = name;
			Address = address;
			OpeningHours = openingHours;
		}

		public Company() {
		}
    }
}
