using GentApp.Helpers;
using System;

namespace GentApp.DataModel
{
	public class Event
	{
		public string Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Branch Branch { get; set; }
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
