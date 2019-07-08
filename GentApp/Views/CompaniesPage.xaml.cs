using GentApp.DataModel;
using System;
using Windows.UI.Xaml.Controls;

namespace GentApp.Views
{
    public sealed partial class CompaniesPage : Page
    {
		public CompaniesPage()
        {
            InitializeComponent();
			companyTypeComboBox.ItemsSource = Enum.GetValues(typeof(BranchType));
		}
	}
}
