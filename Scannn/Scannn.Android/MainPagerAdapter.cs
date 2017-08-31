using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using cdit.ezcheck.Views;
using Android.OS;

namespace cdit.ezcheck
{
    public class MainPagerAdapter : FragmentPagerAdapter
    {
        const int PAGE_COUNT = 3;
        private string[] tabTitles = { "Trang chủ", "Lịch sử", "Thông tin thêm" };
        readonly Context context;

        public MainPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MainPagerAdapter(Context context, FragmentManager fm) : base(fm)
        {
            this.context = context;
        }

        public override int Count
        {
            get { return PAGE_COUNT; }
        }

        public override int GetItemPosition(Java.Lang.Object objectValue)
        {
            //System.Diagnostics.Debug.WriteLine("123. " + objectValue);
            return PositionNone;
        }

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case 1:
                    return SecondFragment.NewInstance();
                case 2:
                    return ThirdFragment.NewInstance();
            }
            return FirstFragment.NewInstance();
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            // Generate title based on item position
            //return CharSequence.ArrayFromStringArray(tabTitles)[position];
            return null;
        }

        public View GetTabView(int position)
        {
            // Given you have a custom layout in `res/layout/custom_tab.xml` with a TextView
            var tv = (TextView)LayoutInflater.From(context).Inflate(Resource.Layout.custom_tab, null);
            tv.Text = tabTitles[position];
            return tv;
        }

    }
}