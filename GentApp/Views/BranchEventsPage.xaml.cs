using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
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
	public sealed partial class BranchEventsPage : Page
	{
		public BranchEventsPage()
		{
			this.InitializeComponent();
			horStackPanel.DataContext = SimpleIoc.Default.GetInstance<CompaniesViewModel>().SelectedBranch;
		}

		private void EventsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedEvent = e.ClickedItem as Event;
			//SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion = selectedPromotion;
			//Frame.Navigate(typeof(EditEventPage));
		}

		private void AddIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			//Frame.Navigate(typeof(AddEventPage));
		}
	}
}
