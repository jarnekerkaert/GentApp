using GalaSoft.MvvmLight.Ioc;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BranchPromotionsPage : Page
	{
		//public ObservableCollection<Promotion> Promotions { get; set; }
		public IEnumerable<Promotion> Promotions { get; set; }
		public List<Promotion> CurrentPromotions { get; set; }
		public List<Promotion> NonCurrentPromotions { get; set; }

		public BranchPromotionsPage()
		{
			this.InitializeComponent();
			PrepareLists();
		}

		private async void PrepareLists()
		{
			var currentDate = DateTime.Today.Date;
			Promotions = await SimpleIoc.Default.GetInstance<CompaniesViewModel>().RetrievePromotions();
			// check wanneer start- en einddatum vandaag zijn => current
			CurrentPromotions = Promotions.Where(p => p.StartDate <= currentDate && p.EndDate >= currentDate).ToList();
			NonCurrentPromotions = Promotions.Except(CurrentPromotions).ToList();
			promotionsListView.ItemsSource = NonCurrentPromotions;
			currentPromotionsListView.ItemsSource = CurrentPromotions;
		}

		private void PromotionsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPromotion = e.ClickedItem as Promotion;
			SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion = selectedPromotion;
			Frame.Navigate(typeof(EditPromotionPage));
		}

		private void AddIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddPromotionPage));
		}

	}
}
