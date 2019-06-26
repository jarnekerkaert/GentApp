namespace GentApp.Models
{
    public class Company
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Openingsuren { get; set; }
        public CompanyType Type { get; set; }
        public int Id { get; set; }

		public Company() {

		}

        public Company(string name, string address, CompanyType type)
        {
            Name = name;
            Address = address;
            Type = type;
        }

        public Company(string openingsuren, string name)
        {
            Openingsuren = openingsuren;
            Name = name;
        }
    }
}
