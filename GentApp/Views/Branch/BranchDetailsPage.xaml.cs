using Windows.UI.Xaml.Controls;
using System;
using GentApp.DataModel;
using Syncfusion.Pdf;
using System.IO;
using GentApp.Helpers;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using System.Reflection;

namespace GentApp.Views {
	public sealed partial class BranchDetailsPage : Page {
		public BranchDetailsPage() {
			InitializeComponent();
		}

		public async void Promotion_Clicked(object sender, ItemClickEventArgs e) {
			var promotion = e.ClickedItem as Promotion;

			if (promotion.UsesCoupon) {
				ContentDialog noWifiDialog = new ContentDialog() {
					Title = "Save coupon?",
					Content = "Save coupon for " + promotion.Title + " as PDF",
					IsPrimaryButtonEnabled = true,
					PrimaryButtonText = "Save",
					CloseButtonText = "Cancel",
				};

				var result = await noWifiDialog.ShowAsync();

				if ( result == ContentDialogResult.Primary ) {
					GeneratePdf(promotion);
				}
			}

			currentPromotionsListView.SelectedItem = null;
		}

		public async void GeneratePdf(Promotion promotion) {
			if ( promotion == null ) {
				throw new ArgumentNullException(nameof(promotion));
			}

			//Create a new PDF document.
			PdfDocument document = new PdfDocument();
			//Document settings
			document.PageSettings.Orientation = PdfPageOrientation.Landscape;
			document.PageSettings.Margins.All = 100;
			//Add a page to the document.
			PdfPage page = document.Pages.Add();
			//Create PDF graphics for the page
			PdfGraphics graphics = page.Graphics;
			//Set the title font
			PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 25);
			//Set the standard font
			PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

			//Draw title
			graphics.DrawString(promotion.Title, titleFont, PdfBrushes.Black, new PointF(0, 0));
			//Draw description
			graphics.DrawString(promotion.Description, font, PdfBrushes.Black, new PointF(0, 25));

			//Load the image as stream.
			Stream imageStream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("GentApp.Assets.barcode.png");
			PdfBitmap image = new PdfBitmap(imageStream);
			//Draw the image
			graphics.DrawImage(image, 200, 100);

			//Save the PDF document to stream.
			MemoryStream stream = new MemoryStream();
			await document.SaveAsync(stream);
			//Close the document.
			document.Close(true);
			//Save the stream as PDF document file in local machine. Refer to PDF/UWP section for respected code samples.
			PdfHelper.Save(stream, promotion.Title + ".pdf");
		}
	}
}
