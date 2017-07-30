using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Scannn;
using Scannn.Droid;
using Android.App;
using Android.Hardware;
using Android.Views;
using Android.Graphics;
using Android.Widget;

[assembly:ExportRenderer (typeof(HomePage), typeof(HomePageRenderer))]
namespace Scannn.Droid
{
    public class HomePageRenderer : PageRenderer
    {
        //global::Android.Hardware.Camera camera;
        global::Android.Widget.Button ScanButton;
        global::Android.Views.View view;

        Activity activity;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                activity = this.Context as Activity;
                view = activity.LayoutInflater.Inflate(Resource.Layout.HomeAndroid, this, false);
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
            
        }

        void SetupEventHandlers()
        {
            ScanButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.ScanButton);
            ScanButton.Click += (sender, e) =>
            {
                DependencyService.Get<ActiveScan>().Scan();
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