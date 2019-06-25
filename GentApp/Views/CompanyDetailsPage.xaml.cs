using GentApp.DataModel;
using GentApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GentApp.Views
{
    public sealed partial class CompanyDetailsPage : Page
    {
		public ObservableCollection<Branch> Branches { get; set; }

		public CompanyDetailsPage()
        {
            this.InitializeComponent();
			//ViewModel = new CompaniesViewModel();
			this.DataContext = MainPage.ViewModel.MySelectedCompany;
			Branches = MainPage.ViewModel.Branches;
			//this.DataContext = DummyDataSource.Companies[0];
		}

		//public CompaniesViewModel ViewModel { get; set; }
	}
}
