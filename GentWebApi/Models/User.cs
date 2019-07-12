
namespace GentApp.Models
{
    public class User
    {
        public User(){

        }

        public User(string firstName, string lastName, string password) {
			Firstname = firstName;
			Lastname = lastName;
			Password = password;
		}

		public string Id { get; set; }
        public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Password { get; set; }
		public Company Company { get; set; }
	}
}
