using GentWebApi.Models;
using System;

namespace GentApp.DataModel {
	public class Promotion {
		public string Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Branch Branch { get; set; }
		public bool AllBranches { get; set; }
		public Coupon Coupon { get; set; }

		public Promotion() {
		}

		public Promotion(DateTime startDate, DateTime endDate, string title, string description) {
			StartDate = startDate;
			EndDate = endDate;
			Title = title;
			Description = description;
		}
	}
}
