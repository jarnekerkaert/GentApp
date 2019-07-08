using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
	public sealed partial class MyCompanyPage : Page
	{
		public ObservableCollection<Branch> Branches { get; set; }

		public MyCompanyPage()
		{
			InitializeComponent();
			DataContext = MainPage.CompaniesViewModel.MyCompany;
			Branches = MainPage.BranchesViewModel.Branches;
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(EditCompanyPage));
		}

		private void AddIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			// navigationservice, navigate to add a branch
			Frame.Navigate(typeof(AddBranchPage));
		}

		private void ListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedBranch = e.ClickedItem as Branch;
			MainPage.BranchesViewModel.MySelectedBranch = selectedBranch;
			Frame.Navigate(typeof(EditBranchPage));

		}
	}
}
