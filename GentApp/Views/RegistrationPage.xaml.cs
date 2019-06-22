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
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
using Windows.ApplicationModel.Activation;

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

        void RegisterClient_Click(object sender, RoutedEventArgs e)
        {
            sendToast("Register", "Client");
            Frame.Navigate(typeof(RegisterClientPage));
        }

        void RegisterCompany_Click(object sender, RoutedEventArgs e)
        {
            sendToast("Register", "Company");
            Frame.Navigate(typeof(MainPage));
        }

        private void sendToast(String title, String content)
        {
            ToastContent toastContent = new ToastContent() {
                Visual = new ToastVisual() {
                    BindingGeneric = new ToastBindingGeneric() {
                        Children =
                        {
                            new AdaptiveText() {
                            Text = "Register"
                            },
                            new AdaptiveText()
                            {
                              Text = "Company"
                            }
                        }
                    }
                },
                Actions = new ToastActionsCustom(),

                Launch = new QueryString()
                {
                    {"action", "register" },
                    {"id", "id" }
                }.ToString()
            };

            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastContent.GetXml()));
        }
    }
}
