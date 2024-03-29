﻿using System;
using Windows.UI.Xaml.Data;

namespace GentApp.DataModel
{
	public class StringFormatConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var format = parameter as string;
			if (!string.IsNullOrEmpty(format))
				return string.Format(format, value);

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			throw new NotImplementedException();
		}
	}
}
