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
        //private NSString cellIdentifier;
        UILabel title, time;//, content;
        nfloat imgwidth, imgheight;
        UIImageView img, timeimg;
        UITextView content;
        UIView mainview;

        public NewsItemCell(NSString cellIdentifier) : base(UITableViewCellStyle.Default, cellIdentifier)
        {
            //width = ContentView.Bounds.Width - 5;
            img = new UIImageView()
            {
                ContentMode =  UIViewContentMode.ScaleAspectFit
                BackgroundColor = UIColor.White
            };
            timeimg = new UIImageView()
            {
                Image = UIImage.FromFile("ic_schedule").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),
                TintColor = Xamarin.Forms.Color.FromHex(Constants.ColorPrimary).ToUIColor(),
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.Clear
            };
            //this.cellIdentifier = cellIdentifier;
            title = new UILabel()
            {
                Font = UIFont.BoldSystemFontOfSize(15f),
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
            time = new UILabel()
            {
                Font = UIFont.SystemFontOfSize(11f),
                TextAlignment = UITextAlignment.Justified,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
            content = new UITextView()
            {
                Editable = false, Font = UIFont.SystemFontOfSize(11f),
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
            ContentView.AddSubviews( new UIView() { title, time, timeimg, img, content });
            /*mainview = new UIView()
            {
                ContentMode = UIViewContentMode.ScaleToFill
            };
            mainview.AddSubviews(title, time, timeimg, img, content);
            ContentView.Add(mainview); 
            mainview.BackgroundColor = UIColor.Cyan;
            //mainview.Bounds.Size = 20f;
            //mainview.TranslatesAutoresizingMaskIntoConstraints = false;
            //mainview.Frame = new CGRect(0, 0, ContentView.Bounds.Width, 280);
            //ContentView.AddConstraint(NSLayoutConstraint.Create(mainview, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Width, 1, 0));
            //ContentView.AddConstraint(NSLayoutConstraint.Create(mainview, NSLayoutAttribute.Height, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Height, 1, 0));
            //mainview.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;
            ContentView.AddConstraint(NSLayoutConstraint.Create(mainview, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.TopMargin, 1, 0));
            ContentView.AddConstraint(NSLayoutConstraint.Create(mainview, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.BottomMargin, 1, 0));
            ContentView.AddConstraint(NSLayoutConstraint.Create(mainview, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.LeadingMargin, 1, 0));
            ContentView.AddConstraint(NSLayoutConstraint.Create(mainview, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.TrailingMargin, 1, 0));
            */
        }

        internal void UpdateCell(NewsItem item)
        {
            title.Text = item.title;
            string contentdisplay = "";
            if (item.content.Length > 170) contentdisplay = item.content.Substring(0, 170);
            else contentdisplay = item.content;
            content.DataDetectorTypes = UIDataDetectorType.Link;
            var urlstring = new NSMutableAttributedString(contentdisplay + "... " + "Xem thêm");
            var lengthcontent = urlstring.Length;
            var linkAttributes = new UIStringAttributes
            {
                Link = new NSUrl(item.detail)
            };
            urlstring.SetAttributes(linkAttributes, new NSRange(lengthcontent - 9, 9));
            content.AttributedText = urlstring;
            //System.Diagnostics.Debug.WriteLine(size.Height +" "+ font.LineHeight+ " "+ size.Width/font.LineHeight+ " " +fsize);
            
            //System.Diagnostics.Debug.WriteLine(actualsize/font.LineHeight);
            time.Text = item.time;

            byte[] imageByte = Convert.FromBase64String(item.img);
            NSData data = NSData.FromArray(imageByte);
            var dataimage = UIImage.LoadFromData(data);
            imgwidth = dataimage.Size.Width;
            imgheight = dataimage.Size.Height;
            UIImage cropped;
            var resolution = (ContentView.Bounds.Width - 10)  / 175;
            var Cropwidthsize = imgheight * resolution;
            //System.Diagnostics.Debug.WriteLine("Cropwidthsize: " + Cropwidthsize);
            if (Cropwidthsize>imgwidth)
            {
                //System.Diagnostics.Debug.WriteLine("height"+ContentView.Bounds.Width + "|" + imgwidth + "|" + imgheight);
                var Cropheightsize = imgwidth / resolution;
                var ycrop = (imgheight - Cropheightsize)/2;
                cropped = CropImage(dataimage, 0, (int)ycrop, (float)imgwidth, (float)Cropheightsize);
            }
            else
            {
                var xcrop = (imgwidth - Cropwidthsize) / 2;
                //System.Diagnostics.Debug.WriteLine("width"+ContentView.Bounds.Width + "|" + imgwidth + "|" + imgheight);
                cropped = CropImage(dataimage, (int)xcrop, 0, (float)Cropwidthsize, (float)imgheight);
            }
            System.Diagnostics.Debug.WriteLine("data cropped"+cropped.Size.Height+ "|" + cropped.Size.Width);
            img.Image = cropped;
        }

        private UIImage CropImage(UIImage sourceImage, int crop_x, int crop_y, float width, float height)
        {
            var imgSize = sourceImage.Size;
            UIGraphics.BeginImageContext(new SizeF(width, height));
            var context = UIGraphics.GetCurrentContext();
            var clippedRect = new RectangleF(0, 0, width, height);
            context.ClipToRect(clippedRect);
            var drawRect = new RectangleF(-crop_x, -crop_y, (float)imgSize.Width, (float)imgSize.Height);
            sourceImage.Draw(drawRect);
            var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return modifiedImage;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            
            var imgviewheight = 175;
            var imgviewwidth = imgviewheight * imgwidth / imgheight;

            //base.AddConstraint(NSLayoutConstraint.Create(mainview, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView,  ));
            //title.Frame = new CGRect(5,10,ContentView.Bounds.Width - 10, 30);
            title.SizeToFit();
            //title.Bounds.Size = new SizeF(320f, (float)title.Bounds.Size.Height);
            //title.TranslatesAutoresizingMaskIntoConstraints = false;
            AddConstraint(NSLayoutConstraint.Create(title, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(title, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, img, NSLayoutAttribute.Bottom, 1, 0));

            img.Frame = new CGRect(5, 40, ContentView.Bounds.Width - 10, imgviewheight);
            //img.TranslatesAutoresizingMaskIntoConstraints = false;
            AddConstraint(NSLayoutConstraint.Create(img, NSLayoutAttribute.Top, NSLayoutRelation.Equal, title, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(img, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, content, NSLayoutAttribute.Bottom, 1, 0));
            
            content.Frame = new CGRect(5, 10+ imgviewheight + 20, ContentView.Bounds.Width - 10, 60);
            //content.TranslatesAutoresizingMaskIntoConstraints = false;
            AddConstraint(NSLayoutConstraint.Create(content, NSLayoutAttribute.Top, NSLayoutRelation.Equal, img, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(content, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, time, NSLayoutAttribute.Bottom, 1, 0));
            
            time.Frame = new CGRect(5 + (ContentView.Bounds.Width - 10) / 7, 10+ imgviewheight + 60+20, ContentView.Bounds.Width- 10 - (ContentView.Bounds.Width -10) / 7, 20);
            //time.TranslatesAutoresizingMaskIntoConstraints = false;
            AddConstraint(NSLayoutConstraint.Create(time, NSLayoutAttribute.Top, NSLayoutRelation.Equal, content, NSLayoutAttribute.Top, 1, 0));
            AddConstraint(NSLayoutConstraint.Create(time, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Bottom, 1, 0));

            timeimg.Frame = new CGRect(5, 10 + imgviewheight + 60 + 20, (ContentView.Bounds.Width - 10) / 7, 20);

            //mainview.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            }
    }
}