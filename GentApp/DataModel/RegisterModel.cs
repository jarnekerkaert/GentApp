
namespace GentApp.DataModel {
	public class RegisterModel {
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public RegisterModel() {

		}
		public RegisterModel(string userName, string firstName, string lastName, string password) {
			UserName = userName;
			FirstName = firstName;
			LastName = lastName;
			Password = password;
		}
	}
}
