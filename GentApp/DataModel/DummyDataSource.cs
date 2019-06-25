﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
    class DummyDataSource
    {
        public static List<Company> Companies { get; set; } = new List<Company>()
        {
            new Company(){Id=1, Name="Microsoft", Address="Amerika", OpeningHours="ALTIJD"},
            new Company(){Id=2, Name="Apple", Address="Amerika", OpeningHours="SOMS"},
            new Company(){Id=3, Name="Mind-it", Address="Voskeslaan", OpeningHours="NOOIT"}
        };

		public static List<Branch> Branches { get; set; } = new List<Branch>()
		{
			new Branch(){Id=4, Name="Branch A", Address="straat 1", Type=BranchType.CAFE, OpeningHours="ALTIJD"},
			new Branch(){Id=5, Name="Branch B", Address="straat 2", Type=BranchType.SHOESSTORE, OpeningHours="SOMS"},
			new Branch(){Id=6, Name="Branch C", Address="straat3", Type=BranchType.OTHER, OpeningHours="NOOIT"}
		};
	}
}