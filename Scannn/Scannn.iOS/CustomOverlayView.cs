using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Scannn.iOS
{
    public class CustomOverlayView : UIView
    {
        public UIButton ButtonTorch, ButtonCancel;
        public UIImageView lefttop, leftbottom, righttop, rightbottom;
        public UIView lineseparator;

        public CustomOverlayView() : base()
        {
            lineseparator = new UIView()
            {
                BackgroundColor = UIColor.White
            };
            nfloat pi = (nfloat)Math.PI;
            lefttop = new UIImageView()
            {
                Image = UIImage.FromFile("LeftBottom"),
                Transform = CGAffineTransform.MakeRotation((pi * 90 / 180))
            };
            leftbottom = new UIImageView()
            {
                Image = UIImage.FromFile("LeftBottom"),

            };
            righttop = new UIImageView()
            {
                Image = UIImage.FromFile("LeftBottom"),
                Transform = CGAffineTransform.MakeRotation((pi * 180 / 180))

            };
            rightbottom = new UIImageView()
            {
                Image = UIImage.FromFile("LeftBottom"),
                Transform = CGAffineTransform.MakeRotation((pi * -90 / 180))

            };
            ButtonTorch = UIButton.FromType(UIButtonType.RoundedRect);
            ButtonTorch.SetTitle("Bật Flash", UIControlState.Normal);
            ButtonTorch.SetTitleColor(UIColor.White, UIControlState.Normal);
            ButtonTorch.Layer.BorderWidth = 2;
            ButtonTorch.Layer.BorderColor = UIColor.White.CGColor;
            ButtonTorch.Layer.CornerRadius = 5;

            ButtonCancel = UIButton.FromType(UIButtonType.RoundedRect);
            ButtonCancel.SetTitle("Hủy", UIControlState.Normal);
            ButtonCancel.SetTitleColor(UIColor.White, UIControlState.Normal);
            ButtonCancel.Layer.BorderWidth = 2;
            ButtonCancel.Layer.BorderColor = UIColor.White.CGColor;
            ButtonCancel.Layer.CornerRadius = 5;

            this.AddSubviews(lineseparator, lefttop, leftbottom, rightbottom, righttop ,ButtonCancel, ButtonTorch);
            
        }

        public override void LayoutSubviews()
        {
            var remainview = this.Bounds.Height - this.Bounds.Width - 1 ;
            lineseparator.Frame = new CGRect(this.Bounds.Width/6, this.Bounds.Width, 2* this.Bounds.Width/3 , 1);
            lefttop.Frame = new CGRect(this.Bounds.Width / 6, this.Bounds.Width / 6, this.Bounds.Width / 6, this.Bounds.Width / 6);
            leftbottom.Frame = new CGRect(this.Bounds.Width / 6, 2 * this.Bounds.Width / 3, this.Bounds.Width / 6, this.Bounds.Width / 6);

            righttop.Frame = new CGRect(2* this.Bounds.Width / 3, this.Bounds.Width / 6, this.Bounds.Width / 6, this.Bounds.Width / 6);
            rightbottom.Frame = new CGRect(2* this.Bounds.Width / 3, 2* this.Bounds.Width / 3, this.Bounds.Width / 6, this.Bounds.Width / 6);

            ButtonTorch.Frame = new CGRect(this.Bounds.Width/4, this.Bounds.Width + remainview / 8, this.Bounds.Width / 2, this.Bounds.Height / 10);
            ButtonCancel.Frame = new CGRect(this.Bounds.Width / 4, this.Bounds.Width + remainview / 2 + remainview / 8, this.Bounds.Width / 2, this.Bounds.Height / 10);
        }
    }
}
