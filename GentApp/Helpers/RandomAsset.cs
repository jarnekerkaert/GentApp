using System;

namespace GentApp.Helpers {
	public static class RandomAsset {

		private static string[] assets;

		static RandomAsset() {
			assets = new string[] {
				"/Assets/company1.jpg",
			"/Assets/company2.jpg",
			"/Assets/company3.jpg",
			"/Assets/company4.jpg"
			};
		}

		public static string getRandomAsset() {
			Random random = new Random();
			return assets[random.Next(assets.Length)];
		}
	}
}
