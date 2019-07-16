using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
	public class Subscription
	{
		public string Id { get; set; }
		[Required]
		public string BranchId { get; set; }
		//public Branch Branch { get; set; }
		[Required]
		public string UserId { get; set; }
		//public User User { get; set; }

		public Subscription()
		{
		}

		public Subscription(string branchId, string userId)
		{
			BranchId = branchId;
			UserId = userId;
		}
	}
}
