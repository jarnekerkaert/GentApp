using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.ViewModels;
using Windows.UI.Xaml.Controls;


namespace GentApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BranchEventsPage : Page
	{
		public BranchEventsPage()
		{
			InitializeComponent();
		}

		private void EventsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			SimpleIoc.Default.GetInstance<BranchViewModel>().SelectedEvent = e.ClickedItem as Event;
			Frame.Navigate(typeof(EditEventPage));
		}
	}
}
