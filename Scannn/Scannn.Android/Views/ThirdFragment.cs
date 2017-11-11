using Android.OS;
using Android.Views;
using Android.Widget;
using Rg.Plugins.Popup.Extensions;
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
            var LoginoutButton = view.FindViewById<Button>(Resource.Id.LoginButton);
            var Username = view.FindViewById<TextView>(Resource.Id.user_profile_name);
            var shortbio = view.FindViewById<TextView>(Resource.Id.user_profile_short_bio);

            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(0.2), () =>
            {
                // Do something
                //Debug.WriteLine("Chỉ số đã mua: " + App.Bought);
                if (Scannn.App.sessionId != null)
                {
                    Username.Text = Scannn.App.fullname;
                    shortbio.Text = Scannn.App.email;
                    LoginoutButton.Text = "Đăng xuất";
                }
                else
                {
                    
                    LoginoutButton.Text = "Đăng nhập";
                }
               
                return true; // True = Repeat again, False = Stop the timer
                
            });
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

            LoginoutButton.Click += async delegate 
            {
                if(LoginoutButton.Text == "Đăng nhập")
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new LoginDialog());
                else
                {
                    try
                    {
                        await Scannn.App.SvLoginManager.DoLogoutAsync(Scannn.App.sessionId);
                        Scannn.App.sessionId = null;
                        Scannn.App.UDatabase.DeleteAllAsync();
                        // Scannn.App.UDatabase.cre
                        Username.Text = "ezCheck";
                        shortbio.Text = "Kiểm tra nguồn gốc";
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                }
            };
            return view;
        }
    }
}