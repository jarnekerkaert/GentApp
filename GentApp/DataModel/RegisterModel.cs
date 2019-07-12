using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GentApp.DataModel {
	public class RegisterModel {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public RegisterModel() {

		}
		public RegisterModel(string firstName, string lastName) {
			FirstName = firstName;
			LastName = lastName;
		}
	}
}
