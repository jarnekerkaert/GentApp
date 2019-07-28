using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GentWebApi.Models
{
	public class Subscription
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		[Required]
		public string BranchId { get; set; }
		[Required]
		public string UserId { get; set; }
	}
}
