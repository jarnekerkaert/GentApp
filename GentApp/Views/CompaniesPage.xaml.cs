using GentApp.DataModel;
using GentApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;

using GentApp.DataModel;
using GentApp.ViewModels;

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
            InitializeComponent();
			var _enumval = Enum.GetValues(typeof(BranchType));
			companyTypeComboBox.ItemsSource = _enumval;
		}

		private void ListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedCompany = e.ClickedItem as Company;
			MainPage.CompaniesViewModel.MySelectedCompany = selectedCompany;
			Frame.Navigate(typeof(CompanyDetailsPage));
		}
	}
}
