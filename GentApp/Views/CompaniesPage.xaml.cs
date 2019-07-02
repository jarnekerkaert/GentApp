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

namespace GentApp.Views
{
    public sealed partial class CompaniesPage : Page
    {
		public ObservableCollection<Company> Companies { get; set; }
		public CompaniesPage()
        {
            this.InitializeComponent();
			//this.DataContext = new CompaniesViewModel();
			Companies = MainPage.CompaniesViewModel.Companies;
			var _enumval = Enum.GetValues(typeof(BranchType));
			companyTypeComboBox.ItemsSource = _enumval;
			//ViewModel = new CompaniesViewModel();
		}

		//public CompaniesViewModel ViewModel { get; set; }

		private void ListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedCompany = e.ClickedItem as Company;
			MainPage.CompaniesViewModel.MySelectedCompany = selectedCompany;
			Frame.Navigate(typeof(CompanyDetailsPage));
		}
	}
}
