using GentApp.Models;
using System;

namespace GentWebApi.Models {
	public class Promotion {
		public string Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Branch Branch { get; set; }
		public bool UsesCoupon { get; set; }
		public bool AllBranches { get; set; }

		public Promotion() {
			UsesCoupon = false;
		}

		public Promotion(DateTime startDate, DateTime endDate, string title, string description) {
			UsesCoupon = false;
			StartDate = startDate;
			EndDate = endDate;
			Title = title;
			Description = description;
		}
	}
}
