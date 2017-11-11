using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using cdit.ezcheck;
using Scannn;
using Scannn.Droid;
using Scannn.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Support.V4.View.ViewPager;

[assembly: ExportRenderer(typeof(HomePage), typeof(HomePageRenderer))]
namespace cdit.ezcheck
{
    public class HomePageRenderer : PageRenderer
    {
        global::Android.Views.View view;
        global::Android.Support.V4.View.ViewPager pager;
        global::Android.Support.Design.Widget.TabLayout tabLayout;
        global::Android.Support.V7.Widget.SearchView search;
        MainPagerAdapter adapter;

        AppCompatActivity activity;

        private int[] tabIcons = {
            Resource.Drawable.home_color_selector,
            Resource.Drawable.history_color_selector,
            Resource.Drawable.dashboard_color_selector
        };
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                SetupUserInterface();
                SetupEventHandlers();
                AddView(view);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"          ERROR: ", ex.Message);
            }
        }

        void SetupUserInterface()
        {
            activity = this.Context as AppCompatActivity;
            view = activity.LayoutInflater.Inflate(Resource.Layout.Main, this, false);
        }
        void SetupEventHandlers()
        {
            pager = view.FindViewById<global::Android.Support.V4.View.ViewPager>(Resource.Id.pager);
            tabLayout = view.FindViewById<global::Android.Support.Design.Widget.TabLayout>(Resource.Id.sliding_tabs);
            adapter = new MainPagerAdapter(view.Context, activity.SupportFragmentManager);
            
            pager.Adapter = adapter;
            /*pager.PageSelected += (object sender, PageSelectedEventArgs e) =>
                {
                    if( e.Position==1)
                    {
                        Handler h = new Handler();
                        Action myAction = () =>
                        {
                            // your code that you want to delay here
                            pager.Adapter.NotifyDataSetChanged();
                            for (int i = 0; i < tabLayout.TabCount; i++)
                            {
                                Android.Support.Design.Widget.TabLayout.Tab tab = tabLayout.GetTabAt(i);
                                tab.SetIcon(tabIcons[i]);
                            }
                        };
                        h.PostDelayed(myAction, 500);
                    }
                };*/
            tabLayout.SetupWithViewPager(pager);

            for (int i = 0; i < tabLayout.TabCount; i++)
            {
                Android.Support.Design.Widget.TabLayout.Tab tab = tabLayout.GetTabAt(i);
                tab.SetIcon(tabIcons[i]);
            }

            Android.Widget.Button ScanButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.ScanButton);
            ScanButton.Click += (sender, e) =>
            {
                //DependencyService.Get<IActiveScan>().Scan();
                DependencyService.Get<IActiveScan>().Scan();
            };

            search = view.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchview);
            search.QueryHint = "Truy vấn mã sản phẩm";
            search.InputType = 2;
            search.SetIconifiedByDefault(false);
            search.QueryTextSubmit += (sender, e) =>
            {
                //Android.Widget.Toast.MakeText(view.Context, "You searched: " + e.Query, Android.Widget.ToastLength.Short).Show();
                search.SetQuery("", false);
                search.ClearFocus();
                var newResultPage = new ResultPage(e.Query);
                App.Current.MainPage.Navigation.PushAsync(newResultPage);
            };
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            view.Measure(msw, msh);
            view.Layout(0, 0, r - l, b - t);
        }

    }
}