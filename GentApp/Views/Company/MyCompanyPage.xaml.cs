using System.Collections.ObjectModel;
using GentApp.DataModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace GentApp.Views
{
	public sealed partial class MyCompanyPage : Page
	{
		public ObservableCollection<Branch> Branches { get; set; }

		public MyCompanyPage()
		{
			InitializeComponent();
		}

		private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(EditCompanyPage));
		}

		private void AddIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddBranchPage));
		}
	}
}
