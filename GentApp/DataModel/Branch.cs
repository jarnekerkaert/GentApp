using GentApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GentApp.DataModel {
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
		public List<Promotion> Promotions { get; set; }
		public List<Event> Events { get; set; }
		public string ImageUri { get; set; }

		public Branch() {
			ImageUri = RandomAsset.getRandomAsset();
		}

		public Branch(string name, BranchType type, Company company, string address, string openingHours)
			: this() {
			Name = name;
			Type = type;
			Company = company;
			Address = address;
			OpeningHours = openingHours;
		}

		public bool hasOngoingPromotions()
		{
			var result = false;
			var currentDate = DateTime.Today.Date;
			Promotions.ForEach(p =>
			{
				if (p.StartDate <= currentDate && p.EndDate >= currentDate)
				{
					result = true;
				}
			});
			return result;
		}
	}
}
