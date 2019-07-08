using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GentApp;
using GentApp.ViewModels;

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
			SimpleIoc.Default.Register<INavigationService, NavigationService>();
			SimpleIoc.Default.Register<AccountViewModel>();
			SimpleIoc.Default.Register<CompaniesViewModel>();
			SimpleIoc.Default.Register<BranchesViewModel>();
		}

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
