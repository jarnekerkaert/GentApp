using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace GentApp.Helpers {
	public static class PdfHelper {
		public static async void Save(Stream stream, string filename) {
			stream.Position = 0;

			StorageFile stFile;
			if ( !Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons") ) {
				FileSavePicker savePicker = new FileSavePicker {
					DefaultFileExtension = ".pdf",
					SuggestedFileName = filename
				};
				savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
				stFile = await savePicker.PickSaveFileAsync();
			}
			else {
				StorageFolder local = ApplicationData.Current.LocalFolder;
				stFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
			}
			if ( stFile != null ) {
				Windows.Storage.Streams.IRandomAccessStream fileStream = await stFile.OpenAsync(FileAccessMode.ReadWrite);
				Stream st = fileStream.AsStreamForWrite();
				st.SetLength(0);
				st.Write(( stream as MemoryStream )?.ToArray(), 0, (int) stream.Length);
				st.Flush();
				st.Dispose();
				fileStream.Dispose();
				MessageDialog msgDialog = new MessageDialog("Do you want to view the Document?", "File created.");
				UICommand yesCmd = new UICommand("Yes");
				msgDialog.Commands.Add(yesCmd);
				UICommand noCmd = new UICommand("No");
				msgDialog.Commands.Add(noCmd);
				IUICommand cmd = await msgDialog.ShowAsync();
				if ( cmd == yesCmd ) {
					// Launch the retrieved file 
					bool success = await Windows.System.Launcher.LaunchFileAsync(stFile);
				}
			}
		}
	}
}
