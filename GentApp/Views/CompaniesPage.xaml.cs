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
		public CompaniesPage()
        {
            InitializeComponent();
			companyTypeComboBox.ItemsSource = Enum.GetValues(typeof(BranchType));
		}

		private void ListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			MainPage.CompaniesViewModel.MySelectedCompany = e.ClickedItem as Company;
			Frame.Navigate(typeof(CompanyDetailsPage));
		}
	}
}
