using Foundation;
using Scannn.Data;
using Scannn.iOS;
using Scannn.iOS.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Facebook.ShareKit;

[assembly: Dependency(typeof(Scannn.iOS.Sharing))]
namespace Scannn.iOS
{
    class Sharing : IShare
    {
        
        public async Task ShareAsync(ImageSource image, string message)
        {
            await ShareImageAsync(image, message);
        }

        private static async Task ShareImageAsync(ImageSource image, string message)
        {
            ShareDialog dialog = new ShareDialog();
            SharePhotoContent content = new SharePhotoContent();

            var handler = image.GetHandler();

            if (handler == null) return;

            var uiImage = await handler.LoadImageAsync(image);

            var items = new List<NSObject> { new NSString(message ?? string.Empty) };
            items.Add(uiImage);

            var controller = new UIActivityViewController(items.ToArray(), null);
            
            SharePhoto photo = SharePhoto.From(uiImage, false);
            SharePhoto[] lshare = new SharePhoto[1];
            lshare[0] = photo;
            content.Photos = lshare;

            dialog.FromViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            dialog.Mode = ShareDialogMode.Automatic;
            System.Diagnostics.Debug.WriteLine(dialog.Mode.ToString());
            dialog.SetShareContent(content);
            dialog.Show();
        }
    }
}