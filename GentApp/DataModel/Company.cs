﻿using System;
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
        private string _openingHours;

        public string Name { get => _name; set => _name = value; }
        public string Address { get => _address; set => _address = value; }

		public CompanyType Type { get => _type; set => _type = value; }
        public int Id { get => _id; set => _id = value; }
		public string OpeningHours { get => _openingHours; set => _openingHours = value; }

		public Company(string name, string address, CompanyType type, string openingHours)
        {
            _name = name;
            _address = address;
            _type = type;
            _openingHours = openingHours;
        }

        public Company()
        {
        }
    }
}
