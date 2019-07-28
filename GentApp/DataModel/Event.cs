using GentApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
	public class Event
	{
		public string Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		//public Branch Branch { get; set; }
		public string BranchId { get; set; }
		public string ImageUri { get; set; }

		public Event()
		{
			ImageUri = RandomAsset.getRandomAsset();
		}

		public Event(DateTime startDate, DateTime endDate, string title, string description)
			: this()
		{
			StartDate = startDate;
			EndDate = endDate;
			Title = title;
			Description = description;
		}
	}
}
