using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using MetroLog;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace GentApp.ViewModels {
	public class AccountViewModel : ViewModelBase {
		private ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<AccountViewModel>();

		public AccountService accountService;
		public User User { get; set; }
		public RegisterModel RegisterModel { get; set; }
		public string ErrorMessage { get; set; }

		public AccountViewModel() {
			accountService = new AccountService();
		}

		private RelayCommand _registerCommand;

		public RelayCommand RegisterCommand {
			get {
				return _registerCommand = new RelayCommand(async () => {
					try {
						await accountService.Register(RegisterModel);
						SendToast("Register", "Success");
						log.Info("Register success");
					}
					catch (Exception e) {
						SendToast("Register", e.Message);
						log.Error("ERROR " + e.ToString());
					}
				});
			}
		}

		private void SendToast(string title, string content) {
			ToastContent toastContent = new ToastContent() {
				Visual = new ToastVisual() {
					BindingGeneric = new ToastBindingGeneric() {
						Children =
						{
							new AdaptiveText() {
								Text = title
							},
							new AdaptiveText()
							{
							  Text = content
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
