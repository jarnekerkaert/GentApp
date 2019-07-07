using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GentWebApi.Models;

using Microsoft.AspNetCore.Identity;

namespace GentApp.Models
{
    public class User
    {
        public User(){

        }

        public User(string firstName) {
			Firstname = firstName;
		}
		
		public string Id { get; set; }
        public string Firstname { get; set; }
		public string Lastname { get; set; }
		public Company Company { get; set; }
		internal RoleType Role { get; set; }
	}
}
