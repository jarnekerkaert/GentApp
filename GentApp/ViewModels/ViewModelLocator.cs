using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GentApp.Helpers;
using GentApp.Views;

namespace GentApp.ViewModels
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// </summary> 
	public class ViewModelLocator {
		/// <summary>
		/// Initializes a new instance of the ViewModelLocator class.
		/// </summary>

		static ViewModelLocator() {
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if ( ViewModelBase.IsInDesignModeStatic ) {
				// Create design time view services and models
			}
			else {
				// Create run time view services and models
			}

			//Register your services used here
			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<UserViewModel>();
			SimpleIoc.Default.Register<CompaniesViewModel>();
			SimpleIoc.Default.Register<CompanyViewModel>();
			SimpleIoc.Default.Register<BranchesViewModel>();
			SimpleIoc.Default.Register<BranchViewModel>();
			SetupNavigation();
		}

		private static void SetupNavigation() {
			var navigationService = new NavigationService();
			navigationService.Configure(nameof(HomePage), typeof(HomePage));
			navigationService.Configure(nameof(MainPage), typeof(MainPage));
			navigationService.Configure(nameof(LoginPage), typeof(LoginPage));
			navigationService.Configure(nameof(RegisterClientPage), typeof(RegisterClientPage));
			navigationService.Configure(nameof(RegisterCompanyPage), typeof(RegisterCompanyPage));
			navigationService.Configure(nameof(CompaniesPage), typeof(CompaniesPage));
			navigationService.Configure(nameof(CompanyDetailsPage), typeof(CompanyDetailsPage));
			navigationService.Configure(nameof(MyCompanyPage), typeof(MyCompanyPage));
			navigationService.Configure(nameof(BranchDetailsPage), typeof(BranchDetailsPage));
			navigationService.Configure(nameof(AddBranchPage), typeof(AddBranchPage));
			navigationService.Configure(nameof(AddPromotionPage), typeof(AddPromotionPage));
			SimpleIoc.Default.Unregister<INavigationService>();
			SimpleIoc.Default.Register<INavigationService>(() => navigationService);
		}

		public MainViewModel MainViewModelInstance => ServiceLocator.Current.GetInstance<MainViewModel>();
		public UserViewModel UserViewModelInstance => ServiceLocator.Current.GetInstance<UserViewModel>();
		public CompaniesViewModel CompaniesViewModelInstance => ServiceLocator.Current.GetInstance<CompaniesViewModel>();
		public CompanyViewModel CompanyViewModelInstance => ServiceLocator.Current.GetInstance<CompanyViewModel>();
		public BranchesViewModel BranchesViewModelInstance => ServiceLocator.Current.GetInstance<BranchesViewModel>();
		public BranchViewModel BranchViewModelInstance => ServiceLocator.Current.GetInstance<BranchViewModel>();

		// <summary>
		// The cleanup.
		// </summary>
		public static void Cleanup() {
			// TODO Clear the ViewModels
		}
	}
}
