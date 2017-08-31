using CoreGraphics;
using Foundation;
using Scannn.Models;
using System;
using UIKit;

namespace Scannn.iOS.TabView
{
    class NewsItemCell : UITableViewCell
    {
        //private NSString cellIdentifier;
        UILabel title, time;
        nfloat imgwidth, imgheight;
        UIImageView img;
        UITextView content;

        public NewsItemCell(NSString cellIdentifier) : base(UITableViewCellStyle.Default, cellIdentifier)
        {
            img = new UIImageView()
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.White
            };
            //this.cellIdentifier = cellIdentifier;
            title = new UILabel()
            {
                Font = UIFont.BoldSystemFontOfSize(15f),
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            time = new UILabel()
            {
                Font = UIFont.ItalicSystemFontOfSize(11f),
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            content = new UITextView()
            {
                Editable = false,
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            ContentView.AddSubviews(new UIView[] { title, time, img, content });
        }

        internal void UpdateCell(NewsItem item)
        {
            title.Text = item.title;
            content.Text = item.content;
            time.Text = item.time;
            byte[] imageByte = Convert.FromBase64String(item.img);
            NSData data = NSData.FromArray(imageByte);
            var dataimage = UIImage.LoadFromData(data);
            imgwidth  = dataimage.Size.Width;
            imgheight = dataimage.Size.Height;
            img.Image = dataimage;
            
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var imgviewheight = 150;
            var imgviewwidth = imgviewheight * imgwidth / imgheight;
            title.Frame = new CGRect(0,10,ContentView.Bounds.Width, 30);
            img.Frame = new CGRect(0, 40, ContentView.Bounds.Width, imgviewheight);
            content.Frame = new CGRect(0, 10+ imgviewheight + 18, ContentView.Bounds.Width, 50);
            time.Frame = new CGRect(0, 10+ imgviewheight + 68, ContentView.Bounds.Width, 15);
        }
        }
}