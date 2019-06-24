using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
    public class Company
    {
        private int _id;
        private string _name;
        private string _address;
        private CompanyType _type;
        private string _openingsuren;

        public string Name { get => _name; set => _name = value; }
        public string Address { get => _address; set => _address = value; }
        public string Openingsuren { get => _openingsuren; set => _openingsuren = value; }
        public CompanyType Type { get => _type; set => _type = value; }
        public int Id { get => _id; set => _id = value; }

        public Company(string name, string address, CompanyType type, string openingsuren)
        {
            _name = name;
            _address = address;
            _type = type;
            _openingsuren = openingsuren;
        }

        public Company()
        {
        }
    }
}
