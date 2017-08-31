using Android.App;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Android.Widget;
using Scannn.Models;
using System;
using System.Collections.Generic;

namespace cdit.ezcheck
{
    class NewfeedAdapter : BaseAdapter<NewsItem>
    {
        List<NewsItem> items;
        Activity context;

        public NewfeedAdapter(Activity context, List<NewsItem> items) : base()
        {
            this.context = context;
            this.items = items;
        }
    
        public override NewsItem this[int position]
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
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.NewitemRow, null);
            view.FindViewById<TextView>(Resource.Id.titleNew).Text = item.title;
            if (item.img == "")
            {
                view.FindViewById<ImageView>(Resource.Id.news_cover_image).SetImageResource(Resource.Drawable.empty_img);
            }
            else
            {
                if (item.img.Contains("data:image/png;base64,"))
                {
                    string head = "data:image/png;base64,";
                    item.img = item.img.Remove(0, head.Length);
                }
                try
                {
                    byte[] proimageBytes = Convert.FromBase64String(item.img);
                    view.FindViewById<ImageView>(Resource.Id.news_cover_image).SetImageBitmap(BitmapFactory.DecodeByteArray(proimageBytes, 0, proimageBytes.Length));

                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("lỗi ảnh");
                    view.FindViewById<ImageView>(Resource.Id.news_cover_image).SetImageResource(Resource.Drawable.empty_img);
                }
            }
            var content = view.FindViewById<TextView>(Resource.Id.news_content);
            content.TextFormatted = Html.FromHtml(item.content + " <a href = '"+ item.detail + "'> Xem thêm </a>");
            content.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
            view.FindViewById<TextView>(Resource.Id.news_date).Text = item.time;
            return view;
        }
    }
}