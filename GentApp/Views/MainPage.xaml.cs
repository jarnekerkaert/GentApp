using Windows.UI.Xaml.Controls;

namespace GentApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
		}

		public Frame AppFrame { get { return ContentFrame; } }
	}
}
