
namespace GentApp.DataModel {
	public class User {
		public User() {

		}

		public User(string userName, string firstName, string lastName, string id)
			: this(userName, firstName, lastName) {
			Id = id;
		}

		public User(string userName, string firstName, string lastName) {
			UserName = userName;
			Firstname = firstName;
			Lastname = lastName;
		}

		public string Id { get; set; }
		public string UserName { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public Company Company { get; set; }
	}
}
