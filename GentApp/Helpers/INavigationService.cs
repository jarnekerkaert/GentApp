﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Used https://maximelabelle.wordpress.com/2017/01/19/implementing-viewmodel-based-navigation-in-your-universal-app/
namespace GentApp.Helpers
{
	/// <summary>
	/// Provides a mechanism to navigate between pages.
	/// </summary>
	public interface INavigationService {
		/// <summary>
		/// Gets the name of the currently displayed page.
		/// </summary>
		string CurrentPage { get; }

		/// <summary>
		/// Navigates to the specified page.
		/// </summary>
		/// <param name="page"></param>
		void NavigateTo(string page);

		/// <summary>
		/// Navigates to the specified page and
		/// supply additional page-specific parameters.
		/// </summary>
		/// <param name="page"></param>
		/// <param name="parameter"></param>
		void NavigateTo(string page, object parameter);

		/// <summary>
		/// Navigates to the previous page in the navigation history.
		/// </summary>
		void GoBack();
	}
}
