using Android.OS;
using Android.Views;
using Android.Widget;
using Scannn.Views;
using System;

namespace cdit.ezcheck.Views
{
    class ThirdFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static ThirdFragment NewInstance()
        {
            var fragment = new ThirdFragment { Arguments = new Bundle() };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ThirdFragment, container, false);

            var HomePageButton = view.FindViewById<LinearLayout>(Resource.Id.homepage);
            var InfoPageButton = view.FindViewById<LinearLayout>(Resource.Id.infopage);
            var TermsPageButton = view.FindViewById<LinearLayout>(Resource.Id.termspage);
            var ManualPageButton = view.FindViewById<LinearLayout>(Resource.Id.manualpage);

            HomePageButton.Click += delegate
            {
                Xamarin.Forms.Device.OpenUri(new Uri("http://ezcheck.vn"));
            };
            InfoPageButton.Click += delegate
            {
                var newInfoPage = new InfoPage();
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newInfoPage);
            };
            TermsPageButton.Click += delegate
            {
                var newTermsPage = new TermsPage();
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newTermsPage);
            };
            ManualPageButton.Click += delegate
            {
                var newManualPage = new ManualPage();
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(newManualPage);
            };
            return view;
        }
    }
}