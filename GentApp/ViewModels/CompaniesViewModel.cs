using GentApp.DataModel;
using GentApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.ViewModels
{
    public class CompaniesViewModel : INotifyPropertyChanged
	{
        public ObservableCollection<Company> Companies { get; set; }
		//public Company MySelectedCompany { get; set; }
		private Company mySelectedCompany;
		public Company MySelectedCompany
		{
			get { return mySelectedCompany; }
			set
			{
				if (value != mySelectedCompany)
				{
					mySelectedCompany = value; NotifyPropertyChanged("MySelectedCompany");
				}
			}
		}

        public RelayCommand SaveCompanyCommand { get; set; }
        public CompaniesViewModel()
        {
            Companies = new ObservableCollection<Company>(DummyDataSource.Companies);
            SaveCompanyCommand = new RelayCommand((p) => SaveCompany(p));
        }

		public event PropertyChangedEventHandler PropertyChanged;

		private void SaveCompany(object p)
        {
            this.Companies.Add(new Company()
            {
                Name = p.ToString(),
                Address = "Dummy adres",
                OpeningHours = "24/7",
                Type = CompanyType.MEDIUM
            });
        }

		private void NotifyPropertyChanged(String propertyName) {
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
