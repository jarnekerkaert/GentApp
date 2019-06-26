using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
    public class Branch
    {
		private int _id;
		private string _name;
        private BranchType type;
        private string _companyId;
        private string _address;
		private string _openingHours;

		public Branch()
        {
        }

        public Branch(string name, BranchType type, string companyId, string address, string openingHours)
        {
            _name = name;
            this.type = type;
            _companyId = companyId;
            _address = address;
			_openingHours = openingHours;
		}

		public int Id { get => _id; set => _id = value; }
		public string Name { get => _name; set => _name = value; }
        public string CompanyId { get => _companyId; set => _companyId = value; }
        public string Address { get => _address; set => _address = value; }
        internal BranchType Type { get => type; set => type = value; }
		public string OpeningHours { get => _openingHours; set => _openingHours = value; }

	}
}
