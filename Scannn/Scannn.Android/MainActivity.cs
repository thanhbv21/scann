using Android.App;
using Android.Content.PM;
using Android.OS;
using Scannn;
using ZXing.Mobile;
using System;
using Android.Widget;
using Xamarin.Facebook;


[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/app_id")]
[assembly: MetaData("com.facebook.sdk.ApplicationName", Value = "@string/app_name")]
namespace cdit.ezcheck
{
    [Activity(Label = "Ezcheck", Icon = "@drawable/ezcheck_Logo_v6", MainLauncher = false, Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            //global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
            
            FacebookSdk.SdkInitialize(this);
            MobileBarcodeScanner.Initialize(Application);
            LoadApplication(new App());
        }
    }
}

