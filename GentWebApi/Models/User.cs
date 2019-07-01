using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GentWebApi.Models;

using Microsoft.AspNetCore.Identity;

namespace GentApp.Models
{
    public class User : IdentityUser
    {
        public User(){

        }

        public User(string userName)
			: base(userName) {
		}
		
        public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string CompanyId { get; set; }
		internal UserRole Role { get; set; }
	}
}
