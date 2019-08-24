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
			SearchTerm = null;
			SelectedItemComboBox = null;
			companyTypeComboBox.ItemsSource = Enum.GetValues(typeof(BranchType));
		}

		private void AutoSuggestBoxBranch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
		{
			if (args.CheckCurrent())
			{
				SearchTerm = autoSuggestBoxBranch.Text;
				if (SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches != null)
				{
					autoSuggestBoxBranch.ItemsSource = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches;
					filterListOfBranches(SearchTerm, SelectedItemComboBox);
					List<string> name_results = new List<string>();
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches.ForEach(b => name_results.Add(b.Name));
					autoSuggestBoxBranch.ItemsSource = name_results;
				}
			}
		}

		private void AutoSuggestBoxBranch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
		{
			SearchTerm = args.QueryText;
			if (SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches != null)
			{
				filterListOfBranches(SearchTerm, SelectedItemComboBox);
			}
		}

		private void CompanyTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedItemComboBox = companyTypeComboBox.SelectedItem.ToString();
			if (SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches != null)
			{
				filterListOfBranches(SearchTerm, SelectedItemComboBox);
			}
		}

		public string SearchTerm { get; set; }
		public string SelectedItemComboBox { get; set; }

		private void filterListOfBranches(string search_term, string selectedItem)
		{
			if (selectedItem == null || selectedItem.Equals("UNFILTERED"))
			{
				if (search_term.Equals(""))
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(search_term)).ToList();
				}
			}
			else
			{
				if (search_term == null || search_term.Equals(""))
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Type.ToString().Equals(selectedItem)).ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(search_term) && b.Type.ToString().Equals(selectedItem)).ToList();
				}
			}
		}
	}
}
