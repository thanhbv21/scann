using CoreGraphics;
using Foundation;
using Scannn;
using Scannn.iOS;
using Scannn.Views;
using System;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HomePage), typeof(HomePageRenderer))]
namespace Scannn.iOS
{
    class HomePageRenderer : PageRenderer
    {
        UIButton ScanButton;
       // UIScrollView ScrollContent;
        TabController tabControl;
        UISearchBar search;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                SetupUserInterface();
                SetupEventHandlers();
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Setup màn hình");
                System.Diagnostics.Debug.WriteLine(@"          ERROR: ", ex.Message);
            }
        }

        private void SetupEventHandlers()
        {
            ScanButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                DependencyService.Get<IActiveScan>().Scan();
            };
        }

        private void SetupUserInterface()
        {
            var centerButtonX = View.Bounds.GetMidX() - 35f;
            var topLeftX = View.Bounds.X + 25;
            var topRightX = View.Bounds.Right - 65;
            var bottomButtonY = View.Bounds.Bottom - 150;
            var topButtonY = View.Bounds.Top + 15;
            
            var buttonWidth = 70;
            var buttonHeight = 70;

            var buttonX = View.Bounds.Width - 70;
            var buttonY = View.Bounds.Height/3;
            
            search = new UISearchBar();
            search.Frame = new CGRect(0,0,View.Bounds.Width, 70);
            UIOffset offset = new UIOffset();
            offset.Horizontal = 0;
            offset.Vertical = 10;

            search.SearchFieldBackgroundPositionAdjustment = offset;
            search.KeyboardType = UIKeyboardType.NumberPad;
            search.BackgroundColor = Color.FromHex(Constants.ColorPrimary).ToUIColor();
            search.SearchBarStyle = UISearchBarStyle.Minimal;
            search.Placeholder = "Truy vấn mã sản phẩm";
            search.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            search.BarTintColor = UIColor.White;//Color.FromHex(Constants.ColorSecondary).ToUIColor();
            //search.color

            UIToolbar toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));
            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
                search.ResignFirstResponder();
                if(search.Text.Length != 0)
                {
                    System.Diagnostics.Debug.WriteLine("text: " + search.Text);
                    var newResultPage = new ResultPage(search.Text);
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newResultPage);
                }
            });
            toolbar.Items = new UIBarButtonItem[] {
            new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
            doneButton
            };
            search.InputAccessoryView = toolbar;

            ScanButton = new UIButton();
            ScanButton = UIButton.FromType(UIButtonType.Custom);
            System.Diagnostics.Debug.WriteLine(centerButtonX + "|" + bottomButtonY+"|"+buttonWidth + "|" + buttonHeight);
            ScanButton.SetImage(UIImage.FromFile("ic_scan_button_1.png"), UIControlState.Normal);
            ScanButton.Frame = new CGRect(buttonX, buttonY, buttonWidth, buttonHeight);

            tabControl = new TabController();
            tabControl.View.TintColor = Color.FromHex(Constants.ColorPrimary).ToUIColor();
            var napphsi = App.AppHSI.Count;
            Device.StartTimer(TimeSpan.FromSeconds(0.3), () =>
            {
                if (App.AppHSI.Count != napphsi)
                {
                    tabControl.reloaddata();
                    napphsi = App.AppHSI.Count;
                }
                return true; // True = Repeat again, False = Stop the timer
            });
            /*
            var application = UIApplication.SharedApplication;
            var statusBarView = application.ValueForKey(new NSString("statusBar")) as UIView;
            var foregroundView = statusBarView.ValueForKey(new NSString("foregroundView")) as UIView;
            
            UIView dataNetworkItemView = null;
            foreach (UIView subview in foregroundView.Subviews)
            {
                System.Diagnostics.Debug.WriteLine(subview.Class.Name+"\n");
                if ("UIStatusBarSignalStrengthItemView" == subview.Class.Name)
                {
                    dataNetworkItemView = subview;
                    break;
                }
            }
            if (null == dataNetworkItemView)
                System.Diagnostics.Debug.WriteLine(" return false; //NO SERVICE");
            int bars2 = ((NSNumber)dataNetworkItemView.ValueForUndefinedKey(new NSString("signalStrengthBars"))).Int32Value;
            int bars = ((NSNumber)dataNetworkItemView.ValueForKey(new NSString("signalStrengthBars"))).Int32Value;
            System.Diagnostics.Debug.WriteLine("datanetwork "+ bars + " "+bars2);*/

            tabControl.View.Frame = new CGRect(0, 70, View.Bounds.Width, View.Bounds.Height - 70);
            View.Add(tabControl.View);
            View.Add(search);
            View.AddSubview(ScanButton);
        }
    }
}