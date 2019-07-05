using GentWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.Models {
	public class Branch {
		public int Id { get; set; }
		public int CompanyId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public BranchType Type { get; set; }
		public string OpeningHours { get; set; }
		public IEnumerable<Promotion> Promotions { get; set; }

		public Branch() {
		}

		public Branch(string name, BranchType type, int Id, int companyId, string address, string openingHours) {
			Name = name;
			Type = type;
			CompanyId = companyId;
			Address = address;
			OpeningHours = openingHours;
		}
	}
}
