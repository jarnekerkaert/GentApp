using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
    public class User
    {
        public User(){

        }

        public User(string userName, string password)
        {
			UserName = userName;
            Password = password;
        }

        public int Id { get; set; }
		public string UserName { get; set; }
        public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Password { get; set; }
		public string CompanyId { get; set; }
		internal RoleType Role { get; set; }
	}
}
