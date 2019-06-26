using GentApp.DataModel;
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
    public sealed partial class AddBranchPage : Page
    {
        public AddBranchPage()
        {
            this.InitializeComponent();
			this.DataContext = MainPage.ViewModel;
			var _enumval = Enum.GetValues(typeof(BranchType)).Cast<BranchType>().ToList();
			_enumval.Remove(BranchType.NONE);
			Type.ItemsSource = _enumval;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// TODO: validation for the other properties
			var comboBoxItem = Type.SelectedValue;
			if (comboBoxItem != null)
			{
				BranchType selectedType = (BranchType) comboBoxItem;
				Branch newBranch = new Branch() { Name = Name.Text, Address = Address.Text, OpeningHours = OpeningHours.Text, Type = selectedType };
				MainPage.ViewModel.SaveBranch(newBranch);
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
