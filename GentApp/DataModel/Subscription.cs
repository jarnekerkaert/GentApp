using System.ComponentModel.DataAnnotations;

namespace GentApp.DataModel
{
	public class Subscription
	{
		public string Id { get; set; }
		[Required]
		public Branch Branch { get; set; }
		public string BranchId { get; set; }
		[Required]
		public string UserId { get; set; }
		public int AmountPromotions { get; set; }
		public int AmountEvents { get; set; }

		public Subscription()
		{
		}

		public Subscription(Branch branch, string userId)
		{
			Branch = branch;
			UserId = userId;
		}
	}
}
