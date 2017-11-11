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

        public NewsItemCell(NSString cellIdentifier) : base(UITableViewCellStyle.Default, cellIdentifier)
        {
            //width = ContentView.Bounds.Width - 5;
            img = new UIImageView()
            {
                BackgroundColor = UIColor.White
            };
            timeimg = new UIImageView()
            {
                Image = UIImage.FromFile("ic_schedule").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),
                TintColor = Xamarin.Forms.Color.FromHex(Constants.ColorPrimary).ToUIColor(),
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
                Font = UIFont.SystemFontOfSize(11f),
                TextAlignment = UITextAlignment.Justified,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            content = new UITextView()
            {
                Editable = false,
                Font = UIFont.SystemFontOfSize(11f),
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };
            ContentView.AddSubviews(new UIView[] { title, time, timeimg, img, content });

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
            UIImage dataimage = UIImage.FromFile("no_image_available");
            try
            {
                if (item.img == "")
                {
                    System.Diagnostics.Debug.WriteLine("empty");
                    dataimage = UIImage.FromFile("no_image_available");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("no empty");
                    byte[] imageByte = Convert.FromBase64String(item.img);
                    NSData data = NSData.FromArray(imageByte);
                    dataimage = UIImage.LoadFromData(data);
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("error");

            }
            imgwidth = dataimage.Size.Width;
            imgheight = dataimage.Size.Height;
            UIImage cropped;
            var resolution = (ContentView.Bounds.Width - 10) / 175;
            var Cropwidthsize = imgheight * resolution;
            //System.Diagnostics.Debug.WriteLine("Cropwidthsize: " + Cropwidthsize);
            if (Cropwidthsize > imgwidth)
            {
                //System.Diagnostics.Debug.WriteLine("height"+ContentView.Bounds.Width + "|" + imgwidth + "|" + imgheight);
                var Cropheightsize = imgwidth / resolution;
                var ycrop = (imgheight - Cropheightsize) / 2;
                cropped = CropImage(dataimage, 0, (int)ycrop, (float)imgwidth, (float)Cropheightsize);
            }
            else
            {
                var xcrop = (imgwidth - Cropwidthsize) / 2;
                //System.Diagnostics.Debug.WriteLine("width"+ContentView.Bounds.Width + "|" + imgwidth + "|" + imgheight);
                cropped = CropImage(dataimage, (int)xcrop, 0, (float)Cropwidthsize, (float)imgheight);
            }
            System.Diagnostics.Debug.WriteLine("data cropped" + cropped.Size.Height + "|" + cropped.Size.Width);
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
            System.Diagnostics.Debug.WriteLine(3 * ContentView.Bounds.Width / 5);
            var imgviewwidth = imgviewheight * imgwidth / imgheight;
            title.Frame = new CGRect(5, 10, ContentView.Bounds.Width - 10, 30);
            img.Frame = new CGRect(5, 40, ContentView.Bounds.Width - 10, imgviewheight);
            content.Frame = new CGRect(5, 10 + imgviewheight + 20, ContentView.Bounds.Width - 10, 60);
            time.Frame = new CGRect(5 + (ContentView.Bounds.Width - 10) / 7, 10 + imgviewheight + 60 + 20, ContentView.Bounds.Width - 10 - (ContentView.Bounds.Width - 10) / 7, 20);
            timeimg.Frame = new CGRect(5, 10 + imgviewheight + 60 + 20, (ContentView.Bounds.Width - 10) / 7, 20);
        }
    }
}