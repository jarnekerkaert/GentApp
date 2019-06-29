using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GentWebApi.Models {
	public class UserRole : IdentityRole {
		public UserRole() : base() { }
		public UserRole(string name) : base(name) { }
	}
}
