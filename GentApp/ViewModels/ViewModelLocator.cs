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
			SetupNavigation();
		}

		private static void SetupNavigation() {
			var navigationService = new NavigationService();
			navigationService.Configure(nameof(HomePage), typeof(HomePage));
			navigationService.Configure(nameof(MainPage), typeof(MainPage));
			navigationService.Configure(nameof(CompaniesPage), typeof(CompaniesPage));
			navigationService.Configure(nameof(CompanyDetailsPage), typeof(CompanyDetailsPage));
			navigationService.Configure(nameof(MyCompanyPage), typeof(MyCompanyPage));
			navigationService.Configure(nameof(BranchDetailsPage), typeof(BranchDetailsPage));
			navigationService.Configure(nameof(AddBranchPage), typeof(AddBranchPage));
			navigationService.Configure(nameof(AddPromotionPage), typeof(AddPromotionPage));
			SimpleIoc.Default.Register<INavigationService>(() => navigationService);
		}

		public MainViewModel MainViewModelInstance => ServiceLocator.Current.GetInstance<MainViewModel>();
		public AccountViewModel AccountViewModelInstance => ServiceLocator.Current.GetInstance<AccountViewModel>();
		public CompaniesViewModel CompaniesViewModelInstance => ServiceLocator.Current.GetInstance<CompaniesViewModel>();
		public BranchesViewModel BranchesViewModelInstance => ServiceLocator.Current.GetInstance<BranchesViewModel>();

		// <summary>
		// The cleanup.
		// </summary>
		public static void Cleanup() {
			// TODO Clear the ViewModels
		}
	}
}
