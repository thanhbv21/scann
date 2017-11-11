using CoreGraphics;
using Foundation;
using Scannn.Models;
using System;
using System.Drawing;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Scannn.iOS.TabView
{
    class NewsItemCell : UITableViewCell
    {
        UILabel title, time;//, content;
        nfloat imgwidth, imgheight;
        UIImageView img, timeimg;
        UITextView content;
        UIView mainview;

        public NewsItemCell(NSString cellIdentifier) : base(UITableViewCellStyle.Default, cellIdentifier)
        {
            img = new UIImageView()
            {
                ContentMode =  UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.White
            };
            timeimg = new UIImageView()
            {
                Image = UIImage.FromFile("ic_schedule").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),
                TintColor = Xamarin.Forms.Color.FromHex(Constants.ColorPrimary).ToUIColor(),
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.White
            };
            title = new UILabel()
            {
                Font = UIFont.BoldSystemFontOfSize(15f),
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            time = new UILabel()
            {
                Font = UIFont.SystemFontOfSize(11f),
                TextAlignment = UITextAlignment.Justified,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            content = new UITextView()
            {
                Editable = false, Font = UIFont.SystemFontOfSize(11f),
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            ContentView.AddSubviews( new UIView() { title, img, content, time, timeimg});
            AddConstraint(NSLayoutConstraint.Create(title, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(title, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Bottom, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(img, NSLayoutAttribute.Top, NSLayoutRelation.Equal, title, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(img, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, content, NSLayoutAttribute.Bottom, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(content, NSLayoutAttribute.Top, NSLayoutRelation.Equal, img, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(content, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, time, NSLayoutAttribute.Bottom, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(time, NSLayoutAttribute.Top, NSLayoutRelation.Equal, content, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(time, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Bottom, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(timeimg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, content, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(timeimg, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Bottom, 1, 0));

        }

        internal void UpdateCell(NewsItem item)
        {
            title.Text = item.title;
            content.Text = item.content;
            time.Text = item.time;

            byte[] imageByte = Convert.FromBase64String(item.img);
            NSData data = NSData.FromArray(imageByte);
            var dataimage = UIImage.LoadFromData(data);
            
            img.Image = dataimage;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            title.Frame = new CGRect(5, 10, ContentView.Bounds.Width - 10, 30);
            img.Frame = new CGRect(5, 40, ContentView.Bounds.Width - 10, 175);
            content.Frame = new CGRect(5, 10+ 175 + 20, ContentView.Bounds.Width - 10, 60);
            time.Frame = new CGRect(5 + (ContentView.Bounds.Width - 10) / 7, 10+ 175 + 60+20, ContentView.Bounds.Width- 10 - (ContentView.Bounds.Width -10) / 7, 20);
            timeimg.Frame = new CGRect(5, 10 + 175 + 60 + 20, (ContentView.Bounds.Width - 10) / 7, 20);
            }
    }
}