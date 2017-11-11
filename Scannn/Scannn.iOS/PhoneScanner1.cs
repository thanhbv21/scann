using Scannn.iOS;
using Scannn.Views;
using System;
using System.Diagnostics;

using Foundation;
using UIKit;
using Xamarin.Forms;
using ZXing.Mobile;
using AVFoundation;
using CoreFoundation;

[assembly: Dependency(typeof(PhoneScanner))]
namespace Scannn.iOS
{
    class PhoneScanner: UIViewController ,IActiveScan
    {
        CustomOverlayView customscanlayout;
        MobileBarcodeScanner scanner = new MobileBarcodeScanner();
        Sample1 sp1 = new Sample1();
        AVCaptureSession session;
        AVCaptureMetadataOutput metadataOutput;
        UIAlertView alert = new UIAlertView()
        {
            Title = "Nội dung quét được"
        };
        public void Scan()
        {
            UIStoryboard board = UIStoryboard.FromName("QRScan", null);
            UIViewController ctrl = (UIViewController)board.InstantiateViewController("QRScan");
            ctrl.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
            this.PresentViewController(ctrl, true, null);

            //Console.WriteLine("Bắt đầu scan");
            //UIView scanpage = new UIView();
            //UILabel label1 = new UILabel()
            //{
            //    Text = "1232"
            //};
            //label1.Frame = new CoreGraphics.CGRect(0,0,100,200);
            //this.Add(scanpage);
            //var cvc = new Sample1();

            //var navController = new UINavigationController(cvc);
            //Window.

            //UIStoryboard board = UIStoryboard.FromName("ScanPage", null);
            //UIViewController ctrl = (UIViewController)board.InstantiateViewController("Number2VC");
            //ctrl.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;

            //PresentViewController(navController, true, null);
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