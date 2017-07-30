using System;
using Android.Widget;
using Scannn.Droid;
using Xamarin.Forms;
using ZXing.Mobile;
using Scannn.Models;
using Scannn.Views;

[assembly: Dependency(typeof(PhoneScanner))]
namespace Scannn.Droid
{
    public class PhoneScanner : ActiveScan
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

        public void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                string message = result.Text;
                //if (message.Contains("ezcheck") == true)
                //{
                    string[] splitlink;
                    splitlink = message.Split(new string[] { "/" }, StringSplitOptions.None);
                    string itemcode = splitlink[splitlink.Length - 1];
                    msg = "Found a link: " + itemcode;
                    var newResultPage = new ResultPage();
                    newResultPage.setlayout(itemcode);
                    Application.Current.MainPage.Navigation.PushAsync(newResultPage);
                /*Product pro = await App.SvManager.GetProductAsync(itemcode);
                Company com = await App.SvManager.GetCompanyAsync(itemcode);
                ImageAPI image = await App.SvManager.GetImageAsync(itemcode);

                LayoutDataResult dataresult = new LayoutDataResult();
                dataresult.product = pro;
                dataresult.company = com;
                dataresult.image = image.image;

                var newResultPage = new ResultPage();
                newResultPage.setlayout(dataresult);
                await Application.Current.MainPage.Navigation.PushAsync(newResultPage);*/
                //}

                //else msg = "Tìm thấy nội dung: " + result.Text;
            }
               
            else
                msg = "Scanning Canceled!";
            //Toast.MakeText(Android.App.Application.Context, msg, ToastLength.Long).Show();
            //return msg;
        }
    }
}