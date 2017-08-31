
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using System.Threading.Tasks;

namespace cdit.ezcheck
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {

        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            System.Diagnostics.Debug.WriteLine("Khởi tạo Splash");
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        public override void OnBackPressed() { }
        
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(3000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));

        }
    }
}