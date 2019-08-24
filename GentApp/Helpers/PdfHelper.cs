using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace GentApp.Helpers {
	public class PdfHelper {

		public void GenerateDefault() {
			//Initialize writer
			PdfWriter writer = new PdfWriter("\\Coupon.pdf");

			//Initialize document
			PdfDocument pdfDocument = new PdfDocument(writer);
			Document document = new Document(pdfDocument);
			Image image = new Image(ImageDataFactory.Create("/Assets/barcode.png"));

			image.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT);

			//Add paragraph to the document
			document.Add(new Paragraph("Hello World!"));
			document.Add(image);

			//Close document
			document.Close();

		}
	}
}
