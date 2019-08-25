using System;
using Windows.UI.Xaml.Data;

namespace GentApp.DataModel {
	public class CouponToVisibilityConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, string language) {
			bool usesCoupon = (bool) value;
			return usesCoupon ? "Visible" : "Collapsed";
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			return new NotImplementedException();
		}
	}
}
