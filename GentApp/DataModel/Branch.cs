using GentApp.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GentApp.DataModel {
	public class Branch
	{
		public string Id { get; set; }
		public string CompanyId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		public BranchType Type { get; set; }
		public string OpeningHours { get; set; }
		public IEnumerable<Promotion> Promotions { get; set; }
		public IEnumerable<Event> Events { get; set; }
		public string ImageUri { get; set; }

		public Branch() {
			ImageUri = RandomAsset.getRandomAsset();
		}

		public Branch(string name, BranchType type, string id, string companyId, string address, string openingHours)
			: this() {
			Name = name;
			Type = type;
			CompanyId = companyId;
			Address = address;
			OpeningHours = openingHours;
		}
	}
}
