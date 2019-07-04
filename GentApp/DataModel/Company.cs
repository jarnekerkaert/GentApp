using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel {
	public class Company {
		private int _id;
		private string _name;
		private string _address;
		private string _openingHours;

		public string Name { get => _name; set => _name = value; }
		public string Address { get => _address; set => _address = value; }

		public int Id { get => _id; set => _id = value; }
		public string OpeningHours { get => _openingHours; set => _openingHours = value; }

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
