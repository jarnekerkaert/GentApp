using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel {
	public class Company {
		public string Name { get; set; }
		public string Address { get; set; }
		public string OpeningHours { get; set; }
		public int Id { get; set; }
		public IEnumerable<Branch> branches { get; set; }

		public Company(string name, string address, string openingHours) {
			Name = name;
			Address = address;
			OpeningHours = openingHours;
		}

		public Company() {

		}
	}
}
