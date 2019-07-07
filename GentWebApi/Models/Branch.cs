using GentWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.Models {
	public class Branch
	{
		[Required]
		public string Id { get; set; }
		[Required]
		public string CompanyId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		public BranchType Type { get; set; }
		public string OpeningHours { get; set; }
		public IEnumerable<Promotion> Promotions { get; set; }

		public Branch() {
		}

		public Branch(string name, BranchType type, string id, string companyId, string address, string openingHours) {
			Name = name;
			Type = type;
			CompanyId = companyId;
			Address = address;
			OpeningHours = openingHours;
		}
	}
}
