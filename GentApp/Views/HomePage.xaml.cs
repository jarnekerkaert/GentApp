using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace GentApp.Views {
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
				if ( !IsFilteredBranchesNull() )
				{
					autoSuggestBoxBranch.ItemsSource = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches;
					FilterListOfBranches();
					List<string> name_results = new List<string>();
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches.ForEach(b => name_results.Add(b.Name));
					autoSuggestBoxBranch.ItemsSource = name_results;
				}
			}
		}

		private bool IsFilteredBranchesNull()
		{
			return SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches == null;
		}

		private void AutoSuggestBoxBranch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
		{
			SearchTerm = args.QueryText;
			FilterListOfBranches();
		}

		private void CompanyTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedItemComboBox = companyTypeComboBox.SelectedItem.ToString();
			FilterListOfBranches();
		}

		public string SearchTerm { get; set; }
		public string SelectedItemComboBox { get; set; }

		private void FilterListOfBranches()
		{
			if ( !IsFilteredBranchesNull() )
			{
				switch (checkBoxOngoingPromotions.IsChecked)
				{
					case true:
						FilterBranchesWithOngoingPromotions();
						break;
					case false:
					default:
						FilterAllBranches();
						break;
				}
		}
	}

		private void FilterAllBranches()
		{
			if ( SelectedItemComboBox?.Equals("UNFILTERED") != false )
			{
				if ( SearchTerm?.Equals("") != false )
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
				}
			}
			else if ( SearchTerm?.Equals("") != false ) {
				SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Type.ToString().Equals(SelectedItemComboBox)).ToList();
			}
			else {
				SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) && b.Type.ToString().Equals(SelectedItemComboBox)).ToList();
			}
		}

		private void FilterBranchesWithOngoingPromotions()
		{
			if ( SelectedItemComboBox?.Equals("UNFILTERED") != false )
			{
				if ( SearchTerm?.Equals("") != false )
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.hasOngoingPromotions()).ToList();
				}
				else
				{
					SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) && b.hasOngoingPromotions()).ToList();
				}
			}
			else if ( SearchTerm?.Equals("") != false ) {
				SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Type.ToString().Equals(SelectedItemComboBox) && b.hasOngoingPromotions()).ToList();
			}
			else {
				SimpleIoc.Default.GetInstance<BranchesViewModel>().FilteredBranches = SimpleIoc.Default.GetInstance<BranchesViewModel>().Branches.Where(b => b.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) && b.Type.ToString().Equals(SelectedItemComboBox) && b.hasOngoingPromotions()).ToList();
			}
		}

		private void CheckBoxOngoingPromotions_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			if (checkBoxOngoingPromotions.IsChecked == true)
			{
				FilterBranchesWithOngoingPromotions();
			}
			else
			{
				FilterAllBranches();
			}
		}
	}
}
