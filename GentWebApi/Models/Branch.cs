using GentWebApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GentApp.Models {
	public class Branch
	{
		public string Id { get; set; }
		public Company Company { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		public BranchType Type { get; set; }
		public string OpeningHours { get; set; }
		public IEnumerable<Promotion> Promotions { get; set; }
		public IEnumerable<Event> Events { get; set; }

		public Branch() {
		}

		public Branch(string name, BranchType type, Company company, string address, string openingHours) {
			Name = name;
			Type = type;
			Company = company;
			Address = address;
			OpeningHours = openingHours;
		}
	}
}
