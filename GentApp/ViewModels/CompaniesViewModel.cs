using GentApp.DataModel;
using GentApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.ViewModels
{
    public class CompaniesViewModel
    {
        public ObservableCollection<Company> Companies { get; set; }
		public Company MySelectedCompany { get; set; }
        public RelayCommand SaveCompanyCommand { get; set; }
        public CompaniesViewModel()
        {
            Companies = new ObservableCollection<Company>(DummyDataSource.Companies);
            SaveCompanyCommand = new RelayCommand((p) => SaveCompany(p));
        }

        private void SaveCompany(object p)
        {
            this.Companies.Add(new Company()
            {
                Name = p.ToString(),
                Address = "Dummy adres",
                Openingsuren = "24/7",
                Type = CompanyType.MEDIUM
            });
        }
    }
}
