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
		public User User { get; set; }
	}
}
