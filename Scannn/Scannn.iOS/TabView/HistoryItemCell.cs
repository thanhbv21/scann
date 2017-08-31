using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Scannn.iOS.TabView
{
    class HistoryItemCell : UITableViewCell
    {
        UILabel headingLabel, subhead, subheadingLabel, comLabel, comhead, timehead, timeLabel;
        UIImageView imageView;
        public HistoryItemCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Gray;
            ContentView.BackgroundColor = UIColor.White;
            imageView = new UIImageView();
            headingLabel = new UILabel()
            {
                Font = UIFont.BoldSystemFontOfSize(18f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
            subhead = new UILabel()
            {
                TextColor = UIColor.Black,
                Font = UIFont.ItalicSystemFontOfSize(12f),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Text = "Mã sản phẩm"
            };
            timehead = new UILabel()
            {
                TextColor = UIColor.Black,
                Font = UIFont.ItalicSystemFontOfSize(12f),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Text = "Thời gian "
            };
            comhead = new UILabel()
            {
                TextColor = UIColor.Black,
                Font = UIFont.ItalicSystemFontOfSize(12f),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Text = "Nhà SX"
            };
            subheadingLabel = new UILabel()
            {
                TextColor = UIColor.Black,
                Font = UIFont.ItalicSystemFontOfSize(12f),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear
            };
            comLabel = new UILabel()
            {
                TextColor = UIColor.Black,
                Font = UIFont.ItalicSystemFontOfSize(12f),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear
            };
            timeLabel = new UILabel()
            {
                TextColor = UIColor.Black,
                Font = UIFont.ItalicSystemFontOfSize(12f),
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear
            };
            ContentView.AddSubviews(new UIView[] { headingLabel, subhead, comhead, timehead, imageView, subheadingLabel, comLabel, timeLabel });
        }
        public void UpdateCell(string caption, string subtitle, UIImage image, string time, string com)
        {
            comLabel.Text = com;
            imageView.Image = image;
            headingLabel.Text = caption;
            subheadingLabel.Text = subtitle;
            timeLabel.Text = time;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var xMainDataContent = 90;
            var yMainDataContent = ContentView.Bounds.Height / 10;
            var yDataContent = ContentView.Bounds.Height / 10 + 25;
            var widthDataContent = ContentView.Bounds.Width / 2;
            var widthHeadContent = ContentView.Bounds.Width / 4;
            var xRightDataContent = xMainDataContent + widthHeadContent;
            //System.Diagnostics.Debug.WriteLine(ContentView.Bounds.Width + "|"+ ContentView.Bounds.Height);
            imageView.Frame = new CGRect(10, 10, 70, 70);
            headingLabel.Frame = new CGRect(xMainDataContent, yMainDataContent, ContentView.Bounds.Width - 70, 20);

            //left.Frame = new CGRect(xMainDataContent, yDataContent, widthDataContent, ContentView.Bounds.Height / 10 + 60);
            subhead.Frame = new CGRect(xMainDataContent, yDataContent, widthHeadContent, 15);
            comhead.Frame = new CGRect(xMainDataContent, yDataContent + 15, widthHeadContent, 15);
            timehead.Frame = new CGRect(xMainDataContent, yDataContent + 30, widthHeadContent, 15);

            //right.Frame = new CGRect(xRightDataContent, yDataContent, widthDataContent, ContentView.Bounds.Height / 10 + 60);
            subheadingLabel.Frame = new CGRect(xRightDataContent, yDataContent, widthDataContent, 15);
            comLabel.Frame = new CGRect(xRightDataContent, yDataContent + 15, widthDataContent, 15);
            timeLabel.Frame = new CGRect(xRightDataContent, yDataContent + 30, widthDataContent, 15);
        }


    }
}
