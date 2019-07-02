using GentApp.ViewModels;
using GentApp.Views;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace GentApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
			CompaniesViewModel = new CompaniesViewModel();
			BranchesViewModel = new BranchesViewModel();
		}

		public static CompaniesViewModel CompaniesViewModel { get; set; }
		public static BranchesViewModel BranchesViewModel { get; set; }


		private void NavView_OnItemInvoked(
      Windows.UI.Xaml.Controls.NavigationView sender,
      NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                NavView_Navigate("settings");
            }
            else if (args.InvokedItem != null)
            {
                var navItemTag = args.InvokedItem.ToString();
                NavView_Navigate(navItemTag);
            }
        }

        private void NavView_Navigate(string navItemTag)
        {
            Type _page = null;
            if (navItemTag == "settings")
            {
                //_page = typeof(SettingsPage);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null, null);
            }
        }

		private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
		{
			("Companies", typeof(CompaniesPage)),
			("Your Company", typeof(MyCompanyPage)),
			("Add a branch", typeof(AddBranchPage)),
			("Manage promotions", typeof(MyPromotionsPage)),
			("Manage events", typeof(MyEventsPage)),
            ("Logout",typeof(LoginPage))
        };

        private void NavView_OnBackRequested(
            Windows.UI.Xaml.Controls.NavigationView sender,
            NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }

        private void NavView_SelectionChanged(NavigationView sender,
                                      NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected == true)
            {
                NavView_Navigate("settings");
            }
            else 
            {
                var navItemTag = args.SelectedItem.ToString();
                NavView_Navigate(navItemTag);
            }
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            ContentFrame.Navigate(typeof(CompaniesPage));
        }
    }
}
