using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Microsoft.QueryStringDotNET; // QueryString.NET
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library

using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GentApp.Views
{
    /// <summary>
    /// Page that handles registration of either client or company
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {

        public RegistrationPage()
        {
            InitializeComponent();
        }

        void Register_Client(object sender, RoutedEventArgs args)
        {
            //sendToast("Register", "Client");
            Frame.Navigate(typeof(RegisterClientPage));
        }

        void Register_Company(object sender, RoutedEventArgs args)
        {
            //sendToast("Register", "Company");
            Frame.Navigate(typeof(RegisterCompanyPage));
        }
	}
}
