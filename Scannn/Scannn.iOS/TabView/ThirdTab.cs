using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Scannn.Views;

namespace Scannn.iOS.TabView
{
    [Register("ThirdTab")]
    public class ThirdTab : UIView
    {
        //UIButton homebutton;
        UIView Viewinfo, home, info, term, manual;
        UIView Viewbutton;
        UIScrollView scroll;
        nfloat span;
        bool tapped;
        public ThirdTab()
        {
            Initialize();
        }

        public ThirdTab(nfloat width, nfloat height)
        {
            Init(width, height);
        }

        private void Init(nfloat width, nfloat height)
        {
            //width = width - 10;
            scroll = new UIScrollView(new CGRect(0, 0, width, height));
            scroll.ContentSize = new CGSize((int)width, (int)height);
            scroll.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            span = 0;
            Viewinfo = new UIView()
            {
                Frame = new CGRect(0, 0, width, height / 3),
                BackgroundColor = UIColor.White
            };
            Viewinfo.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            Viewinfo.Layer.ShadowRadius = 5;
            Viewinfo.Layer.ShadowOpacity = 1;
            Viewinfo.Layer.ShadowOffset = new SizeF(0f, 3f);
            Viewinfo.Layer.ShadowColor = UIColor.Gray.CGColor;

            BackgroundColor = UIColor.White;
            scroll.AddSubview(Viewinfo);
            span = span + height / 3 + 10;

            home = Makebutton("Trang chủ", "ic_language_white.png", width - 10, span);
            home.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            home.UserInteractionEnabled = true;
            UITapGestureRecognizer taphome = new UITapGestureRecognizer(TapHome);
            home.AddGestureRecognizer(taphome);
            scroll.AddSubview(home);
            
            span = span + 50 + 10;

            info = Makebutton("Thông tin ứng dụng","ic_info_white.png", width - 10, span);
            info.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            info.UserInteractionEnabled = true;
            UITapGestureRecognizer tapinfo = new UITapGestureRecognizer(TapInfo);
            info.AddGestureRecognizer(tapinfo);
            scroll.AddSubview(info);

            span = span + 50 + 10;
            term = Makebutton("Điều khoản sử dụng", "ic_book_white.png", width - 10, span);
            term.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            term.UserInteractionEnabled = true;
            UITapGestureRecognizer tapterm = new UITapGestureRecognizer(TapTerm);
            term.AddGestureRecognizer(tapterm);
            scroll.AddSubview(term);

            span = span + 50 + 10;
            manual = Makebutton("Hướng dẫn sử dụng", "ic_import_contacts_white.png", width - 10, span);
            manual.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            manual.UserInteractionEnabled = true;
            UITapGestureRecognizer tapmanual = new UITapGestureRecognizer(TapManual);
            manual.AddGestureRecognizer(tapmanual);
            scroll.AddSubview(manual);
            AddSubview(scroll);
        }

        void TapHome(UITapGestureRecognizer tap)
        {
            if (!tapped)
            {
                tapped = true;
                System.Diagnostics.Debug.WriteLine("đã chạm home");
                //UIApplication.SharedApplication.OpenUrl(new NSUrl("http://ezcheck.vn"));
                Xamarin.Forms.Device.OpenUri(new Uri("http://ezcheck.vn"));
                tapped = false;
            }
            else
            {
                tapped = false;
                System.Diagnostics.Debug.WriteLine("hủy chạm");
            }
        }
        private void TapManual(UITapGestureRecognizer tap)
        {
            if (!tapped)
            {
                tapped = true;
                System.Diagnostics.Debug.WriteLine("đã chạm manual");
                //UIApplication.SharedApplication.OpenUrl(new NSUrl("http://ezcheck.vn"));
                var newManualPage = new ManualPage();
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newManualPage);
                tapped = false;
            }
            else
            {
                tapped = false;
                System.Diagnostics.Debug.WriteLine("hủy chạm");
            }
        }

        private void TapTerm(UITapGestureRecognizer tap)
        {
            if (!tapped)
            {
                tapped = true;
                System.Diagnostics.Debug.WriteLine("đã chạm điều khoản ");
                //UIApplication.SharedApplication.OpenUrl(new NSUrl("http://ezcheck.vn"));
                var newTermsPage = new TermsPage();
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newTermsPage);
                tapped = false;
            }
            else
            {
                tapped = false;
                System.Diagnostics.Debug.WriteLine("hủy chạm");
            }
        }

        private void TapInfo(UITapGestureRecognizer tap)
        {
            if (!tapped)
            {
                tapped = true;
                System.Diagnostics.Debug.WriteLine("đã chạm info");
                //UIApplication.SharedApplication.OpenUrl(new NSUrl("http://ezcheck.vn"));
                var newInfoPage = new InfoPage();
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newInfoPage);
                tapped = false;
            }
            else
            {
                //tap.View.Transform *= CGAffineTransform.MakeRotation((float)-Math.PI / 2);
                tapped = false;
                System.Diagnostics.Debug.WriteLine("hủy chạm");
            }
        }

        private UIView Makebutton(string title, string imagename, nfloat width, nfloat position)
        {
            Viewbutton = new UIView()
            {
                Frame = new CGRect(5, position, width, 50),
                BackgroundColor = UIColor.White
            };
            Viewbutton.Layer.ShadowRadius = 5;
            Viewbutton.Layer.ShadowOpacity = 1;
            Viewbutton.Layer.ShadowOffset = new SizeF(0f, 3f);
            Viewbutton.Layer.ShadowColor = UIColor.Gray.CGColor;
            UIImageView LeftImage = new UIImageView();
            System.Diagnostics.Debug.WriteLine(imagename);
            UIImageView InsideImage = new UIImageView()
            {
               
                Image = UIImage.FromFile(imagename),
                Frame = new CGRect(17.5, 17.5, 15, 15)
            };
            LeftImage.Image = UIImage.FromFile("Square");
            LeftImage.Frame = new CGRect(10, 10, 30, 30);
            LeftImage.Layer.CornerRadius = 10;
            LeftImage.Layer.MasksToBounds = true;

            UILabel label = new UILabel()
            {
                Text = title,
                Frame = new CGRect(50, 5, width - 50, 40),
                TextColor = UIColor.Black
            };

            Viewbutton.Add(LeftImage);
            Viewbutton.Add(InsideImage);
            Viewbutton.Add(label);

            return Viewbutton;
        }
        public ThirdTab(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
        }

        
    }
}