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
    public sealed partial class CompanyDetailsPage : Page
    {
		public ObservableCollection<Branch> Branches { get; set; }

		public CompanyDetailsPage()
        {
            this.InitializeComponent();
			RetrieveBranches();
			this.DataContext = MainPage.CompaniesViewModel.MySelectedCompany;
			Branches = MainPage.BranchesViewModel.Branches;
		}

		private void ListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedBranch = e.ClickedItem as Branch;
			MainPage.BranchesViewModel.MySelectedBranch = selectedBranch;
			Frame.Navigate(typeof(BranchDetailsPage));

		}

		private async void RetrieveBranches()
		{
			HttpClient client = new HttpClient();
			progressBranches.IsActive = true;
			var json = await client.GetStringAsync(new Uri("http://localhost:50957/api/companies/" + MainPage.CompaniesViewModel.MySelectedCompany.Id +"/branches"));
			var list = JsonConvert.DeserializeObject<ObservableCollection<Branch>>(json);
			branchesListView.ItemsSource = list;
			progressBranches.IsActive = false;
			MainPage.BranchesViewModel.Branches = list;
		}
	}
}
