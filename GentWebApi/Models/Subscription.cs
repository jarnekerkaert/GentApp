using GentApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GentWebApi.Models
{
	public class Subscription
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		[Required]
		public Branch Branch { get; set; }
		[Required]
		public string UserId { get; set; }

		public int AmountPromotions { get; set; }
		public int AmountEvents { get; set; }
	}
}
