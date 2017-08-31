using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Scannn.Models;
using System;
using System.Collections.Generic;

namespace cdit.ezcheck
{
    public class HistoryScanItemAdapter : BaseAdapter<HistoryScanItem>
    {
        List<HistoryScanItem> items;
        Activity context;

        public HistoryScanItemAdapter(Activity context, List<HistoryScanItem> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override HistoryScanItem this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.HistoryItemRow, null);
            view.FindViewById<TextView>(Resource.Id.TextName).Text = item.itemname;
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.itemcode;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.companyname;
            view.FindViewById<TextView>(Resource.Id.Text3).Text = item.datetime;
            if (item.itemimage=="")
            {
                view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.icon_default);
            }
            else
            {
                if (item.itemimage.Contains("data:image/png;base64,"))
                {
                    string head = "data:image/png;base64,";
                    item.itemimage = item.itemimage.Remove(0, head.Length);
                }
                try
                {
                    byte[] proimageBytes = Convert.FromBase64String(item.itemimage);
                    view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(BitmapFactory.DecodeByteArray(proimageBytes, 0, proimageBytes.Length));
                }
                catch
                {
                    view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.icon_default); ;
                }
                
            }
            return view;
        }
    }
}