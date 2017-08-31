using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Scannn.Models;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Scannn.iOS.TabView
{
    class NewsTableSource : UITableViewSource
    {
        private List<NewsItem> TableItems;
        NSString CellIdentifier = new NSString("TableCell");

        public NewsTableSource(List<NewsItem> items)
        {
            TableItems = items;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) as NewsItemCell;
            NewsItem item = TableItems[indexPath.Row];

            if (cell == null) cell = new NewsItemCell(CellIdentifier);

            byte[] imageByte = Convert.FromBase64String(item.img);
            NSData data = NSData.FromArray(imageByte);
            UIImage itemimage = UIImage.LoadFromData(data);

            cell.BackgroundColor = Xamarin.Forms.Color.FromHex("#cfd0d5").ToUIColor();
            //cell.Layer.BorderWidth = 5;
            //cell.Layer.BorderColor = UIColor.Gray.CGColor;// = new UIEdgeInsets(10,0,0,0);
            //cell.LayoutMargins = new UIEdgeInsets(10, 0, 0, 0);
            //cell.Layer.BackgroundColor = UIColor.Gray.CGColor;

            cell.UpdateCell(item);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Count;
        }
    }
}