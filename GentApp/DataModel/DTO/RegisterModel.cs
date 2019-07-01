using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel.DTO
{
	public class RegisterModel {
		public string UserName { get; set; }
		public string Password { get; set; }

		public RegisterModel(string userName, string password) {
			UserName = userName;
			Password = password;
		}

		public RegisterModel() {

		}
	}
}
