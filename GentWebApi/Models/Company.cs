using System.Collections.Generic;

namespace GentApp.Models
{
    public class Company
    {
		public string Name { get; set; }
		public string Address { get; set; }
		public string OpeningHours { get; set; }

		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
