using System;
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

		public const string MainPageKey = "MainPage";
		public const string CompaniesPageKey = "CompaniesPage";
		public const string CompanyDetailsPageKey = "CompanyDetailsPage";
		public const string MyCompanyPageKey = "MyCompanyPage";
		public const string BranchDetailsPageKey = "BranchDetailsPage";
		public const string AddBranchPageKey = "AddBranchPage";
		public const string AddPromotionPageKey = "AddPromotionPage";
		public const string EditBranchPageKey = "EditBranchPage";
		public const string LoginPageKey = "LoginPage";

		public ViewModelLocator() {
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if ( ViewModelBase.IsInDesignModeStatic ) {
				// Create design time view services and models
			}
			else {
				// Create run time view services and models
			}

			//Register your services used here
			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<AccountViewModel>();
			SimpleIoc.Default.Register<CompaniesViewModel>();
			SimpleIoc.Default.Register<BranchesViewModel>();
			SimpleIoc.Default.Register<BranchViewModel>();
			SetupNavigation();
		}

		private static void SetupNavigation() {
			var navigationService = new NavigationService();
			navigationService.Configure(MainPageKey, typeof(MainPage));
			navigationService.Configure(CompaniesPageKey, typeof(CompaniesPage));
			navigationService.Configure(CompanyDetailsPageKey, typeof(CompanyDetailsPage));
			navigationService.Configure(MyCompanyPageKey, typeof(MyCompanyPage));
			navigationService.Configure(BranchDetailsPageKey, typeof(BranchDetailsPage));
			navigationService.Configure(AddBranchPageKey, typeof(AddBranchPage));
			navigationService.Configure(AddPromotionPageKey, typeof(AddPromotionPage));
			navigationService.Configure(EditBranchPageKey, typeof(EditBranchPage));
			navigationService.Configure(LoginPageKey, typeof(LoginPage));
			SimpleIoc.Default.Register<INavigationService>(() => navigationService);
		}

		public MainViewModel MainViewModelInstance => ServiceLocator.Current.GetInstance<MainViewModel>();
		public AccountViewModel AccountViewModelInstance => ServiceLocator.Current.GetInstance<AccountViewModel>();
		public CompaniesViewModel CompaniesViewModelInstance => ServiceLocator.Current.GetInstance<CompaniesViewModel>();
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
