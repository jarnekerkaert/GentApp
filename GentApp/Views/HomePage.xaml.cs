using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class HomePage : Page {
		public HomePage() {
			InitializeComponent();
			companyTypeComboBox.ItemsSource = Enum.GetValues(typeof(BranchType));
			//Branches = new List<Branch>(SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches);
			//autoSuggestBoxBranch.ItemsSource = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches;
			//autoSuggestBoxBranch.ItemsSource = Branches;
		}

		//public List<Branch> Branches { get; set; }
		//public List<string> BranchNames { get; set; }

		private void AutoSuggestBoxBranch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
		{
			if (args.CheckCurrent())
			{
				var search_term = autoSuggestBoxBranch.Text;
				if (SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches != null)
				{
					autoSuggestBoxBranch.ItemsSource = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches;
					var results = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(search_term)).ToList();
					//autoSuggestBoxBranch.ItemsSource = results;
					List<string> name_results = new List<string>();
					results.ForEach(b => name_results.Add(b.Name));
					autoSuggestBoxBranch.ItemsSource = name_results;
					HomeFeedGrid.ItemsSource = results;
				}
			}

		}

		private void AutoSuggestBoxBranch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
		{
			var search_term = args.QueryText;
			if (SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches != null)
			{
				var results = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(search_term)).ToList();
				//autoSuggestBoxBranch.ItemsSource = results;
				HomeFeedGrid.ItemsSource = results;
			}
		}

	}
}
