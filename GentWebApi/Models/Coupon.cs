namespace GentWebApi.Models {
	public class Coupon {

		public string Id { get; set; }
		public string ImageUrl { get; set; }

		public Coupon() {

		}

		public Coupon(string id, string imageUrl) {
			Id = id;
			ImageUrl = imageUrl;
		}
	}
}
