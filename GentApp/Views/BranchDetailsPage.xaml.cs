using GalaSoft.MvvmLight.Ioc;
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

namespace GentApp.Views
{
	public sealed partial class BranchDetailsPage : Page
	{
		public BranchDetailsPage()
		{
			InitializeComponent();
		}


		private void PromotionsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			//var selectedPromotion = e.ClickedItem as Promotion;
			//SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion = selectedPromotion;
			//Frame.Navigate(typeof(PromotionPage));
		}

	}
}
