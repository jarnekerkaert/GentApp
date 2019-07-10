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
		public List<Promotion> Promotions { get; set; }

		public BranchPromotionsPage()
		{
			this.InitializeComponent();
			RetrievePromotions();
			horStackPanel.DataContext = SimpleIoc.Default.GetInstance<CompaniesViewModel>().SelectedBranch;
			//AmountPromotionsTextBlock.Text = Promotions.Count.ToString();
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

		private async void RetrievePromotions()
		{
			HttpClient client = new HttpClient();
			progressPromotions.IsActive = true;
			var json = await client.GetStringAsync(new Uri("http://localhost:50957/api/branches/" + SimpleIoc.Default.GetInstance<CompaniesViewModel>().SelectedBranch.Id + "/promotions"));
			var list = JsonConvert.DeserializeObject<ObservableCollection<Promotion>>(json);
			var currentBranchId = SimpleIoc.Default.GetInstance<CompaniesViewModel>().SelectedBranch.Id;
			SimpleIoc.Default.GetInstance<CompaniesViewModel>().MyCompany.Branches.Where(b => b.Id.Equals(currentBranchId)).FirstOrDefault().Promotions = list;
			//SimpleIoc.Default.GetInstance<BranchViewModel>().Promotions = list;
			progressPromotions.IsActive = false;
			promotionsListView.ItemsSource = SimpleIoc.Default.GetInstance<CompaniesViewModel>().SelectedBranch.Promotions;
			// TODO: datums checken
			currentPromotionsListView.ItemsSource = SimpleIoc.Default.GetInstance<CompaniesViewModel>().SelectedBranch.Promotions;
		}
	}
}
