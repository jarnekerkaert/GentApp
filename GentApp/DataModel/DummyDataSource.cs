using System;
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
            new Company(){Id=1, Name="Microsoft", Address="Amerika", Type=CompanyType.BIG, OpeningHours="ALTIJD"},
            new Company(){Id=1, Name="Apple", Address="Amerika", Type=CompanyType.BIG, OpeningHours="SOMS"},
            new Company(){Id=1, Name="Mind-it", Address="Voskeslaan", Type=CompanyType.SMALL, OpeningHours="NOOIT"}
        };
    }
}
