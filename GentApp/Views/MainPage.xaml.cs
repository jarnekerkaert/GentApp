﻿using GentApp.Views;
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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GentApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationViewItem _lastItem;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_OnItemInvoked(
      Windows.UI.Xaml.Controls.NavigationView sender,
      NavigationViewItemInvokedEventArgs args)
        {
            //if(args.InvokedItem != null)
            //{
            //    switch (args.InvokedItem)
            //    {
            //        case "Companies":
            //            ContentFrame.Navigate(typeof(CompaniesPage));
            //            break;

            //        case "Register a company":
            //            ContentFrame.Navigate(typeof(RegisterCompanyPage));
            //            break;
            //    }
            //}
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
            ("Companies_Page", typeof(CompaniesPage)),
            ("RegisterCompany_Page", typeof(RegisterCompanyPage)),
            ("RegisterCompany", typeof(RegisterCompanyPage))
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

        private void NavView_SelectionChanged_1(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }
    }
}
