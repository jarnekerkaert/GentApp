using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GentApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BranchPromotionsPage : Page
	{

		public BranchPromotionsPage()
		{
			this.InitializeComponent();
			horStackPanel.DataContext = SimpleIoc.Default.GetInstance<CompaniesViewModel>().SelectedBranch;
		}

		private void PromotionsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPromotion = e.ClickedItem as Promotion;
			SimpleIoc.Default.GetInstance<BranchViewModel>().MySelectedPromotion = selectedPromotion;
			Frame.Navigate(typeof(EditPromotionPage));
		}

		private void AddIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddPromotionPage));
		}

	}
}
