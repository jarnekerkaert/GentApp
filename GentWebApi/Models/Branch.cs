using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.Models
{
    class Branch
    {
        private string _name;
        private BranchType type;
        private string _companyId;
        private string _address;

        public Branch()
        {
        }

        public Branch(string name, BranchType type, string companyId, string address)
        {
            _name = name;
            this.type = type;
            _companyId = companyId;
            _address = address;
        }

        public string Name { get => _name; set => _name = value; }
        public string CompanyId { get => _companyId; set => _companyId = value; }
        public string Address { get => _address; set => _address = value; }
        internal BranchType Type { get => type; set => type = value; }
    }
}
