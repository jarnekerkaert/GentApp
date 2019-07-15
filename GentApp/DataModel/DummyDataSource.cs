using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel {
	class DummyDataSource {
		public static List<Company> Companies { get; set; } = new List<Company>()
		{
			new Company(){Id="a", Name="Microsoft", Address="Amerika", OpeningHours="ALTIJD"},
			new Company(){Id="b", Name="Apple", Address="Amerika", OpeningHours="SOMS"},
			new Company(){Id="c", Name="Mind-it", Address="Voskeslaan", OpeningHours="NOOIT"}
		};

		public static List<Branch> Branches { get; set; } = new List<Branch>()
		{
			new Branch(){Id="d", Name="Branch A", Address="straat 1", Type=BranchType.CAFE, OpeningHours="ALTIJD"},
			new Branch(){Id="e", Name="Branch B", Address="straat 2", Type=BranchType.SHOESSTORE, OpeningHours="SOMS"},
			new Branch(){Id="f", Name="Branch C", Address="straat3", Type=BranchType.OTHER, OpeningHours="NOOIT"}
		};

		public static List<Promotion> Promotions { get; set; } = new List<Promotion>()
		{
			new Promotion(){Id="g", StartDate=DateTime.Now, EndDate = DateTime.Now, Title = "Big promotion for July!", Description = "Buy one item and get a second for free - only during July" },
			new Promotion(){Id="h", StartDate=DateTime.Now, EndDate = DateTime.Now, Title = "Big promotion for August!", Description = "Buy two items and get a third for free - only during August" },
			new Promotion(){Id="i", StartDate=DateTime.Now, EndDate = DateTime.Now, Title = "Big promotion for September!", Description = "Buy three items and get a fourth for free - only during September" }
		};

		public static User DefaultUser = new User() {
			Firstname = "henk",
			Lastname = "pim",
			Id = "abcd-1234"
		};
	}
}