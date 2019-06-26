using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
    public class Company
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Openingsuren { get; set; }
        public CompanyType Type { get; set; }
        public int Id { get; set; }

		public Company(string name, string address, string openingHours)
        {
            Name = name;
            Address = address;
            Type = type;
            Openingsuren = openingsuren;
        }

        public Company()
        {
        }
    }
}
