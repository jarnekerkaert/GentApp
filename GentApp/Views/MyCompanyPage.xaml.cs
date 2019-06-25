using GentApp.ViewModels;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MyCompanyPage : Page
	{
		public MyCompanyPage()
		{
			this.InitializeComponent();
			//ViewModel = new CompaniesViewModel();
			this.DataContext = MainPage.ViewModel.MyCompany;
			//this.DataContext = ViewModel.MySelectedCompany;
			//this.DataContext = DummyDataSource.Companies[0];
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(EditCompanyPage));
		}

		//public CompaniesViewModel ViewModel { get; set; }
	}
}
