using Android.Views;
using Android.Widget;
using cdit.ezcheck;
using Scannn;
using Scannn.Views;
using System;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(PhoneScanner))]
namespace cdit.ezcheck
{
    public class PhoneScanner : IActiveScan
    {
        Android.Views.View zxingOverlay;
        Android.Widget.Button flashButton;
        MobileBarcodeScanner scanner = new MobileBarcodeScanner();
        public async void Scan()
        {
            scanner.UseCustomOverlay = true;
            var options = new MobileBarcodeScanningOptions() {
                 AutoRotate = false,
                 TryInverted = true
            };
            zxingOverlay = LayoutInflater.FromContext(Android.App.Application.Context).Inflate(Resource.Layout.ScanCustom, null);
            //zxingOverlay.scree
 
            flashButton = zxingOverlay.FindViewById<Android.Widget.Button>(Resource.Id.buttonFlash);
            flashButton.Click += (sender, e) =>
            {
                if (flashButton.Text == "Tắt Flash") flashButton.Text = "Bật Flash";
                else flashButton.Text = "Tắt Flash";
                scanner.ToggleTorch();
            };
            scanner.CustomOverlay = zxingOverlay;

            //scanner.TopText = "Giữ camera cách mã khoảng 6inch";
            //scanner.BottomText = "Đợi một chút...";

            var result = await scanner.Scan(options);
            System.Diagnostics.Debug.WriteLine("Đợi két quả");
            HandleScanResult(result);
        }

        public void HandleScanResult(ZXing.Result result)
        {
            string msg = "";
            System.Diagnostics.Debug.WriteLine("Đợi Ktra kết quả");
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                string message = result.Text;
                System.Diagnostics.Debug.WriteLine("bắt đầu ktra");
                bool checkresult = Uri.TryCreate(message, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (checkresult)
                {
                    System.Diagnostics.Debug.WriteLine("đây là URL");
                    if (message.Contains("ezcheck") == true)
                    {
                        string[] splitlink;
                        splitlink = message.Split(new string[] { "/" }, StringSplitOptions.None);
                        System.Diagnostics.Debug.WriteLine("đây là url ezcheck");
                        string itemcode = splitlink[splitlink.Length - 1];
                        bool isNumberic = long.TryParse(itemcode, out long itemcodenum);
                        if (isNumberic)
                        {
                            msg = "Tìm thấy mã sản phẩm: " + itemcode;
                            System.Diagnostics.Debug.WriteLine("bắt đầu đẩy");
                            var newResultPage = new ResultPage(itemcode);
                            Application.Current.MainPage.Navigation.PushAsync(newResultPage);
                        }
                        else
                        {
                            msg = "Tìm thấy URL: " + message;
                            if (itemcode[0].ToString().Equals("B")) Application.Current.MainPage.Navigation.PushAsync(new ResultPage(itemcode));
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("đây URL ezcheck thuần");
                                Device.OpenUri(new Uri(message));
                            }
                        }
                    }
                    else
                    {
                        msg = "Tìm thấy URL: " + message;
                    }

                }
                else msg = "Tìm thấy nội dung: " + message;
            }

            else
                msg = "Scanning Canceled!";
            Toast.MakeText(Android.App.Application.Context, msg, ToastLength.Long).Show();
        }
    }
}