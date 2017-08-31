using CoreGraphics;
using Scannn;
using Scannn.iOS;
using System;
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
            search.BarTintColor = UIColor.White;
            search.BackgroundColor = Color.FromHex(Constants.ColorPrimary).ToUIColor();
            search.SearchBarStyle = UISearchBarStyle.Minimal;
            search.Placeholder = "Truy vấn mã sản phẩm";
            search.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            search.BarTintColor = Color.FromHex(Constants.ColorSecondary).ToUIColor();

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
                if (App.AppHSI.Count != napphsi) tabControl.reloaddata();
                return true; // True = Repeat again, False = Stop the timer
            });

            tabControl.View.Frame = new CGRect(0, 70, View.Bounds.Width, View.Bounds.Height - 70);
            View.Add(tabControl.View);
            View.Add(search);
            View.AddSubview(ScanButton);
        }
    }
}