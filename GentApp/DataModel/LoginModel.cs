using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel {
	public class LoginModel {
		public string UserName { get; set; }
		public string Password { get; set; }
		public LoginModel() {

		}
		public LoginModel(string userName, string password) {
			UserName = userName;
			Password = password;
		}
	}
}
