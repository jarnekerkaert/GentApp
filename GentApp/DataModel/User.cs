﻿
namespace GentApp.DataModel {
	public class User {
		public User() {

		}

		public User(string firstName, string lastName, string id)
			: this(firstName, lastName) {
			Id = id;
		}

		public User(string firstName, string lastName) {
			Firstname = firstName;
			Lastname = lastName;
		}

		public string Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public Company Company { get; set; }
	}
}
