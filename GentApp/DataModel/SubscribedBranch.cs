using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
	public class SubscribedBranch
	{
		public string Id { get; set; }
		public Company Company { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public BranchType Type { get; set; }
		public string OpeningHours { get; set; }
		public List<Promotion> Promotions { get; set; }
		public List<Event> Events { get; set; }
		public int AmountPromotions { get; set; }
		public int AmountEvents { get; set; }

		public SubscribedBranch(){
		}

		public SubscribedBranch(string name, BranchType type, Company company, string address, string openingHours)
			: this()
		{
			Name = name;
			Type = type;
			Company = company;
			Address = address;
			OpeningHours = openingHours;
		}

		public SubscribedBranch(Branch branch)
		{
			Name = branch.Name;
			Type = branch.Type;
			Company = branch.Company;
			Address = branch.Address;
			OpeningHours = branch.OpeningHours;
		}
	}
}
