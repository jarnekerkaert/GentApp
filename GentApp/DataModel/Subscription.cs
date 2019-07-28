using System.ComponentModel.DataAnnotations;

namespace GentApp.DataModel
{
	public class Subscription
	{
		public string Id { get; set; }
		[Required]
		public string BranchId { get; set; }
		[Required]
		public string UserId { get; set; }

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
