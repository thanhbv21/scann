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
        CustomOverlayView customscanlayout;
        MobileBarcodeScanner scanner = new MobileBarcodeScanner();
        UIAlertView alert = new UIAlertView()
        {
            Title = "Nội dung quét được"
        };
        public async void Scan()
        {
            scanner.UseCustomOverlay = true;
            var options = new MobileBarcodeScanningOptions()
            {
                AutoRotate = false,
                TryInverted = true
            };
            customscanlayout = new CustomOverlayView();
            scanner.CustomOverlay = customscanlayout;
            //var options = new MobileBarcodeScanningOptions() { };
            customscanlayout.ButtonTorch.TouchUpInside += delegate {
                if (customscanlayout.ButtonTorch.TitleLabel.Text == "Tắt Flash") customscanlayout.ButtonTorch.SetTitle("Bật Flash", UIControlState.Normal);
                else customscanlayout.ButtonTorch.SetTitle("Tắt Flash", UIControlState.Normal);

                scanner.ToggleTorch();
            };
            customscanlayout.ButtonCancel.TouchUpInside += delegate
            {
                scanner.Cancel();
            };
            //scanner.TopText = "Giữ camera cách mã khoảng 6inch";
            //scanner.BottomText = "Đợi một chút...";

            var result = await scanner.Scan();
            System.Diagnostics.Debug.WriteLine("Đợi két quả");
            HandleScanResult(result);
        }

        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";
            
            alert.AddButton("OK");
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
                            alert.Message = msg;
                            alert.Show();
                            var newResultPage = new ResultPage(itemcode);
                            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newResultPage);
                        }
                        else
                        {
                            msg = "Tìm thấy URL: " + message;
                            if (itemcode[0].ToString().Equals("B")) Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ResultPage(itemcode));
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("đây URL ezcheck thuần");
                                Device.OpenUri(new Uri(message));
                            }
                            alert.Message = msg;
                            alert.Show();
                        }
                    }
                    else
                    {
                        msg = "Tìm thấy URL: " + message;
                        alert.Message = msg;
                        alert.Show();
                    }

                }
                else
                {

                    msg = "Tìm thấy nội dung: " + message;
                    alert.Message = msg;
                    alert.Show();
                }
            }

            else
                msg = "Scanning Canceled!";
            
            
        }
    }
}