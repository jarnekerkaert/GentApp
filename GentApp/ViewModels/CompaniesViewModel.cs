﻿using GentApp.DataModel;
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
		public ObservableCollection<Branch> Branches { get; set; }

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

		private Company myCompany;
		public Company MyCompany
		{
			get { return myCompany; }
			set
			{
				if (value != myCompany)
				{
					myCompany = value; NotifyPropertyChanged("MyCompany");
				}
			}
		}

		public RelayCommand SaveCompanyCommand { get; set; }
		public RelayCommand SaveBranchCommand { get; set; }

		public CompaniesViewModel()
        {
            Companies = new ObservableCollection<Company>(DummyDataSource.Companies);
			Branches = new ObservableCollection<Branch>(DummyDataSource.Branches);
			MyCompany = DummyDataSource.Companies[2];
            SaveCompanyCommand = new RelayCommand((p) => SaveCompany(p));
			SaveBranchCommand = new RelayCommand((p) => SaveBranch(p));
        }

		public event PropertyChangedEventHandler PropertyChanged;

		private void SaveCompany(object p)
        {
            this.Companies.Add(new Company()
            {
                Name = p.ToString(),
                Address = "Dummy adres",
                OpeningHours = "24/7"
			});
        }

		private void SaveBranch(object p)
		{
			this.Branches.Add(new Branch()
			{
				Name = p.ToString(),
				Address = "Dummy adres",
				OpeningHours = "24/7"
			});
		}

		private void NotifyPropertyChanged(String propertyName) {
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}