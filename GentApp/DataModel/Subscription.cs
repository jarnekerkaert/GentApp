using System.ComponentModel.DataAnnotations;

namespace GentApp.DataModel
{
	public class Subscription
	{
		public string Id { get; set; }
		[Required]
		public Branch Branch { get; set; }
		[Required]
		public User User { get; set; }

		public Subscription()
		{
		}

		public Subscription(Branch branch, User user)
		{
			Branch = branch;
			User = user;
		}
	}
}
