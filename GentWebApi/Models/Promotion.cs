using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GentWebApi.Models
{
	public class Promotion
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string CouponId { get; set; }
		public int BranchId { get; set; }
		public bool AllBranches { get; set; }

		public Promotion()
		{
		}

		public Promotion(DateTime startDate, DateTime endDate, string title, string description)
		{
			StartDate = startDate;
			EndDate = endDate;
			Title = title;
			Description = description;
		}
	}
}
