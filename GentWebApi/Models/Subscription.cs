using GentApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GentWebApi.Models
{
	public class Subscription
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		[Required]
		public string BranchId { get; set; }
		//public Branch Branch { get; set; }
		[Required]
		public string UserId { get; set; }
		//public User User { get; set; }
	}
}
