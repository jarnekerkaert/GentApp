using GentApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GentWebApi.Models
{
	public class Event
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		//public Branch Branch { get; set; }
		public string BranchId { get; set; }

		public Event()
		{
		}

		public Event(DateTime startDate, DateTime endDate, string title, string description)
		{
			StartDate = startDate;
			EndDate = endDate;
			Title = title;
			Description = description;
		}


	}
}
