using Scannn.Data;
using Scannn.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Scannn.iOS.Extensions;
using System.Threading.Tasks;
using UIKit;
using CoreGraphics;
using Foundation;

[assembly: Dependency(typeof(DrawText))]
namespace Scannn.iOS
{
    class DrawText : IDraw
    {

        public async Task<ImageSource> Drawtext(ImageSource image, string message, string author)
        {
            var handler = image.GetHandler();

            if (handler == null) return null;

            var uiImage = await handler.LoadImageAsync(image);
            NSString s = NSString.FromData(message, NSStringEncoding.UTF8);
            NSString s1 = NSString.FromData(author, NSStringEncoding.UTF8);
            var resultimage = DrawTextInImage1(uiImage, s, s1, new CGPoint(0,0));
            return ImageSource.FromStream(() => resultimage.AsPNG().AsStream());
            throw new NotImplementedException();
        }

        public static UIImage DrawTextInImage1(UIImage uiImage, NSString text, NSString text1, CGPoint point)
        {
            nfloat fWidth = uiImage.Size.Width;
            nfloat fHeight = uiImage.Size.Height;

            nfloat afHeight = uiImage.Size.Height / 5;
            UIFont font = UIFont.BoldSystemFontOfSize(50);
            UIFont authorfont = UIFont.SystemFontOfSize(45);
            var imagesize = new CGSize(fWidth, fHeight+ afHeight);
            UIGraphics.BeginImageContext(imagesize);

            UIImage background = new UIImage();
            background.Draw(new CGRect(0, 0, fWidth, fHeight + afHeight));
            uiImage.Draw(new CGRect(0, 0, fWidth, fHeight));

            var style = new NSMutableParagraphStyle();
            style.Alignment = UITextAlignment.Center;
            var att = new NSAttributedString(text,
                font: font,
                paragraphStyle : style,
                foregroundColor: UIColor.White
                );

            var author = new NSAttributedString(text1,
                font: authorfont,
                paragraphStyle: style,
                foregroundColor: UIColor.Black
                );
            CGRect stringrect = att.GetBoundingRect(new CGSize(fWidth-100,fHeight + afHeight), NSStringDrawingOptions.UsesLineFragmentOrigin, null);
            System.Diagnostics.Debug.WriteLine("width:"+stringrect.Size.Width + " height:"+stringrect.Size.Height);
            nfloat yOffset = (fHeight - stringrect.Size.Height) / 2;
            nfloat heightsize = stringrect.Size.Height + yOffset;
            System.Diagnostics.Debug.WriteLine("yOffset + height:" + heightsize);
            CGRect rect = new CGRect(point.X + 50, yOffset, fWidth - 100, fHeight + afHeight);
            att.DrawString(rect.Integral());
            
            CGRect rect1 = new CGRect(point.X, fHeight +  (afHeight - authorfont.LineHeight)/2, fWidth, 50);
            author.DrawString(rect1.Integral());

            UIImage resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
        /*
        public static UIImage DrawTextInImage(UIImage uiImage, string sText, UIColor textColor, int iFontSize)
        {
            nfloat fWidth = uiImage.Size.Width;
            nfloat fHeight = uiImage.Size.Height;

            CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();

            using (CGBitmapContext ctx = new CGBitmapContext(IntPtr.Zero, (nint)fWidth, (nint)fHeight, 8, 4 * (nint)fWidth, CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
            {
                ctx.DrawImage(new CGRect(0, 0, (double)fWidth, (double)fHeight), uiImage.CGImage);

                ctx.SelectFont("Helvetica", iFontSize, CGTextEncoding.MacRoman);

                //Measure the text's width - This involves drawing an invisible string to calculate the X position difference
                float start, end, textWidth;

                //Get the texts current position
                start = (float)ctx.TextPosition.X;
                //Set the drawing mode to invisible
                ctx.SetTextDrawingMode(CGTextDrawingMode.Invisible);
                //Draw the text at the current position
                ctx.ShowText(sText);
                //Get the end position
                end = (float)ctx.TextPosition.X;
                //Subtract start from end to get the text's width
                textWidth = end - start;

                nfloat fRed;
                nfloat fGreen;
                nfloat fBlue;
                nfloat fAlpha;
                //Set the fill color to black. This is the text color.
                textColor.GetRGBA(out fRed, out fGreen, out fBlue, out fAlpha);
                ctx.SetFillColor(fRed, fGreen, fBlue, fAlpha);

                //Set the drawing mode back to something that will actually draw Fill for example
                ctx.SetTextDrawingMode(CGTextDrawingMode.Fill);

                //Draw the text at given coords.
                ctx.ShowTextAtPoint(8, 0, sText);

                return UIImage.FromImage(ctx.ToImage());
            }
        }
        */

    }
}
