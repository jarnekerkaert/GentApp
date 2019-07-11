using GalaSoft.MvvmLight.Ioc;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MyPromotionsPage : Page
	{
		public Company MyCompany { get; set; }
		public IEnumerable<Promotion> Promotions { get; set; }

		public MyPromotionsPage()
		{
			InitializeComponent();
			MyCompany = SimpleIoc.Default.GetInstance<CompaniesViewModel>().MyCompany;
			horStackPanel.DataContext = MyCompany;
			Promotions = SimpleIoc.Default.GetInstance<BranchViewModel>().Promotions;
			AmountPromotionsTextBlock.Text = Promotions.ToList().Count.ToString();
		}

		private void PromotionsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion = e.ClickedItem as Promotion;
			Frame.Navigate(typeof(EditPromotionPage));
		}

		private void AddIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddPromotionPage));
		}
	}
}
