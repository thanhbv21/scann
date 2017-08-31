using Android.OS;
using Android.Views;
using Android.Widget;
using Scannn;
using Scannn.Models;
using Scannn.Views;
using System.Collections.Generic;

namespace cdit.ezcheck.Views
{
    class SecondFragment : Android.Support.V4.App.Fragment
    {

        private List<HistoryScanItem> uphsiItems = new List<HistoryScanItem>();
        //private ListView mListView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public static SecondFragment NewInstance()
        {
            var fragment = new SecondFragment { Arguments = new Bundle() };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.SecondFragment, container, false);
            List<HistoryScanItem> hsiItems = new List<HistoryScanItem>();
            uphsiItems = new List<HistoryScanItem>();
            uphsiItems = App.HDatabase.GetItemsAsync().Result;
            view.SetBackgroundColor(Android.Graphics.Color.White);

            for (int i = uphsiItems.Count - 1; i >= 0; i--) hsiItems.Add(uphsiItems[i]);
            App.AppHSI = hsiItems;
            string tag = "scannn - hsiitems";
            Android.Util.Log.Debug(tag, hsiItems.Count.ToString());

            ListView listView;
            listView = view.FindViewById<ListView>(Resource.Id.myListView);
            listView.Adapter = new HistoryScanItemAdapter(Activity, App.AppHSI);

            listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Android.Util.Log.Debug(tag, "click");
                var index = hsiItems[e.Position].itemcode;
                var newResultPage = new ResultPage(index);
                global::Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newResultPage);
            };

            return view;
        }
    }
}