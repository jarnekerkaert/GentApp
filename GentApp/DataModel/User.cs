using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
    class User
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private string _password;
        private string _companyId;
        private RoleType _role;

        public User(){

        }

        public User(string firstname, string lastname, string password, string companyId, RoleType role)
        {
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
            CompanyId = companyId;
            Role = role;
        }

        public int Id { get => _id; set => _id = value; }
        public string Firstname { get => _firstname; set => _firstname = value; }
        public string Lastname { get => _lastname; set => _lastname = value; }
        public string Password { get => _password; set => _password = value; }
        public string CompanyId { get => _companyId; set => _companyId = value; }
        internal RoleType Role { get => _role; set => _role = value; }
    }
}
