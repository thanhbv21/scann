using Scannn.Data;
using cdit.ezcheck;
using cdit.ezcheck.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin.Facebook.Share;
using Xamarin.Facebook.Share.Model;
using Android.Graphics;
using Android.Runtime;
using Xamarin.Facebook.Share.Widget;
using System;

[assembly: Dependency(typeof(cdit.ezcheck.Sharing))]
namespace cdit.ezcheck
{
    class Sharing : IShare
    {
        public async Task ShareAsync(ImageSource image, string message)
        {
            await ShareImageAsync(image, message);
            
        }

        private static async Task ShareImageAsync(ImageSource image, string message)
        {
            var handler = image.GetHandler();

            if (handler == null) return;
            var activity = ((Android.App.Activity)Xamarin.Forms.Forms.Context);
            var bitmap = await handler.LoadImageAsync(image, Android.App.Application.Context);

            System.Diagnostics.Debug.WriteLine("share image");
            ShareDialog shareDialog;
            Bitmap dataimage = bitmap;
            try
            {
                SharePhoto photo = new SharePhoto.Builder().SetBitmap(dataimage).Build().JavaCast<SharePhoto>();
                SharePhotoContent content = new SharePhotoContent.Builder().AddPhoto(photo).Build();

                shareDialog = new ShareDialog(activity);
                shareDialog.Show(content, ShareDialog.Mode.Automatic);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: e: "+ e);
            };
        }
    }
}