using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GentWebApi.Models {
	public class RegisterModel {
		internal string Firstname;

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }

		public RegisterModel(string firstName, string lastName, string password) {
			FirstName = firstName;
			LastName = lastName;
			Password = password;
		}
	}
}
