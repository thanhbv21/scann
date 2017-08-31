using Scannn.iOS;
using Scannn.Views;
using System;
using System.Diagnostics;

using Foundation;
using UIKit;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(PhoneScanner))]
namespace Scannn.iOS
{
    class PhoneScanner: IActiveScan
    {
        MobileBarcodeScanner scanner = new MobileBarcodeScanner();
        public async void Scan()
        {
            scanner.UseCustomOverlay = false;
            //var options = new MobileBarcodeScanningOptions() { };

            scanner.TopText = "Giữ camera cách mã khoảng 6inch";
            scanner.BottomText = "Đợi một chút...";

            var result = await scanner.Scan();

            HandleScanResult(result);
        }

        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                string message = result.Text;
                string[] splitlink;
                splitlink = message.Split(new string[] { "/" }, StringSplitOptions.None);
                string itemcode = splitlink[splitlink.Length - 1];
                msg = "Found a link: " + itemcode;
                var newResultPage = new ResultPage(itemcode);
                //newResultPage.setlayout(itemcode);
                Debug.WriteLine("nội dung tìm thấy" + msg);
                App.Current.MainPage.Navigation.PushAsync(newResultPage);
            }
               // msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            
            /*this.InvokeOnMainThread(() => {
                var av = new UIAlertView("Barcode Result", msg, null, "OK", null);
                av.Show();
            });*/
        }
    }
}