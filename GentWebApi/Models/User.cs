using GentWebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.Models
{
    public class User : IdentityUser
    {
        public User(){

        }

        public User(string username)
        {
			UserName = username;
        }
		
		public string Username { get; set; }
        public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string CompanyId { get; set; }
		internal UserRole Role { get; set; }
	}
}
