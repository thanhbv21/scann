using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using Scannn.Models;
using Scannn.iOS.TabView;

namespace Scannn.iOS
{
    public class HistoryTableSource : UITableViewSource
    {
        List<HistoryScanItem> TableItems;
        NSString CellIdentifier = new NSString("TableCell");

        public HistoryTableSource(List<HistoryScanItem> items)
        {
            TableItems = items;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) as HistoryItemCell;
            HistoryScanItem item = TableItems[indexPath.Row];

            if (cell == null) cell = new HistoryItemCell(CellIdentifier);
            //{ cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }
            UIImage itemimage;
            try
            {
                if(item.itemimage == "")
                {
                    itemimage = UIImage.FromFile("icon_default");
                } else
                {
                    byte[] imageByte = Convert.FromBase64String(item.itemimage);
                    NSData data = NSData.FromArray(imageByte);
                    itemimage = UIImage.LoadFromData(data);
                }
            }
            catch
            {
                itemimage = UIImage.FromFile("icon_default");
            }
            cell.UpdateCell(item.itemname, item.itemcode, itemimage, item.datetime, item.companyname);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var newResultPage = new Views.ResultPage(TableItems[indexPath.Row].itemcode);
            global::Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newResultPage);
        }

        }
}
