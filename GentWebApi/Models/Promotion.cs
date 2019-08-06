﻿using GentApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GentWebApi.Models {
	public class Promotion {
		
		public string Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Branch Branch { get; set; }
		public Coupon Coupon { get; set; }
		public bool AllBranches { get; set; }

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
