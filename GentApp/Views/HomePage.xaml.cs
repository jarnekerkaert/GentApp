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
				if (isFilteredBranchesNull() == false)
				{
					autoSuggestBoxBranch.ItemsSource = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches;
					filterListOfBranches();
					List<string> name_results = new List<string>();
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches.ForEach(b => name_results.Add(b.Name));
					autoSuggestBoxBranch.ItemsSource = name_results;
				}
			}
		}

		private bool isFilteredBranchesNull()
		{
			return SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches == null;
		}

		private void AutoSuggestBoxBranch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
		{
			SearchTerm = args.QueryText;
			filterListOfBranches();
		}

		private void CompanyTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedItemComboBox = companyTypeComboBox.SelectedItem.ToString();
			filterListOfBranches();
		}

		public string SearchTerm { get; set; }
		public string SelectedItemComboBox { get; set; }

		private void filterListOfBranches()
		{
			if (isFilteredBranchesNull() == false)
			{
				switch (checkBoxOngoingPromotions.IsChecked)
				{
					case true:
						filterBranchesWithOngoingPromotions();
						break;
					case false:
						filterAllBranches();
						break;
					default:
						filterAllBranches();
						break;
				}
		}
	}

		private void filterAllBranches()
		{
			if (SelectedItemComboBox == null || SelectedItemComboBox.Equals("UNFILTERED"))
			{
				if (SearchTerm == null || SearchTerm.Equals(""))
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.ToLower().Contains(SearchTerm.ToLower())).ToList();
				}
			}
			else
			{
				if (SearchTerm == null || SearchTerm.Equals(""))
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Type.ToString().Equals(SelectedItemComboBox)).ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.ToLower().Contains(SearchTerm.ToLower()) && b.Type.ToString().Equals(SelectedItemComboBox)).ToList();
				}
			}
		}

		private void filterBranchesWithOngoingPromotions()
		{
			if (SelectedItemComboBox == null || SelectedItemComboBox.Equals("UNFILTERED"))
			{
				if (SearchTerm == null || SearchTerm.Equals(""))
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.hasOngoingPromotions() == true).ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.ToLower().Contains(SearchTerm.ToLower()) && b.hasOngoingPromotions() == true).ToList();
				}
			}
			else
			{
				if (SearchTerm == null || SearchTerm.Equals(""))
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Type.ToString().Equals(SelectedItemComboBox) && b.hasOngoingPromotions() == true).ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.ToLower().Contains(SearchTerm.ToLower()) && b.Type.ToString().Equals(SelectedItemComboBox) && b.hasOngoingPromotions() == true).ToList();
				}
			}
		}

		private void CheckBoxOngoingPromotions_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			if (checkBoxOngoingPromotions.IsChecked == true)
			{
				filterBranchesWithOngoingPromotions();
			}
			else
			{
				filterAllBranches();
			}
		}
	}
}
