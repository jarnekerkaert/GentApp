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
	public sealed partial class SubscriptionsPage : Page
	{
		public SubscriptionsPage()
		{
			this.InitializeComponent();
			SearchTerm = null;
		}

		public string SearchTerm { get; set; }

		private void ListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			Subscription selectedSubscription = e.ClickedItem as Subscription;
			SimpleIoc.Default.GetInstance<BranchesViewModel>().ClearSubscriptionAmounts(selectedSubscription);
			SimpleIoc.Default.GetInstance<BranchesViewModel>().SelectedBranch = selectedSubscription.Branch;
			Frame.Navigate(typeof(BranchDetailsPage));

		}

		private void AutoSuggestBoxSubscription_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
		{
			if (args.CheckCurrent())
			{
				SearchTerm = autoSuggestBoxSubscription.Text;
				if (isFilteredSubscriptionsNull() == false)
				{
					autoSuggestBoxSubscription.ItemsSource = SimpleIoc.Default.GetInstance<BranchesViewModel>().Subscriptions;
					filterListOfSubscriptions();
					List<string> name_results = new List<string>();
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredSubscriptions.ForEach(s => name_results.Add(s.Branch.Name));
					autoSuggestBoxSubscription.ItemsSource = name_results;
				}
			}
		}

		private void AutoSuggestBoxSubscription_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
		{
			SearchTerm = args.QueryText;
			if (isFilteredSubscriptionsNull() == false)
			{
				filterListOfSubscriptions();
			}
		}

		private bool isFilteredSubscriptionsNull()
		{
			return SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredSubscriptions == null;
		}

		private void filterListOfSubscriptions()
		{
			if (SearchTerm == null || SearchTerm.Equals(""))
			{
				SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredSubscriptions = SimpleIoc.Default.GetInstance<BranchesViewModel>().Subscriptions.ToList();
			}
			else
			{
				SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredSubscriptions = SimpleIoc.Default.GetInstance<BranchesViewModel>().Subscriptions.Where(b => b.Branch.Name.ToLower().Contains(SearchTerm.ToLower())).ToList();
			}
		}
	}
}
