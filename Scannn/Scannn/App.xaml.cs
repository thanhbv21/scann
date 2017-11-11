using Scannn.Data;
using Scannn.Models;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;

using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Scannn
{
    public partial class App : Application
    {
        public static ServicesManager SvManager { get; private set; }
        public static ServicesManager SvNewsManager { get; private set; }
        public static ServicesManager SvLoginManager { get; private set; }
        public static ServicesManager SvLocationManager { get; private set; }
        public static HistoryDatabase historydatabase;
        public static UserProfileDatabase userdatabase;
        public static NewsDatabase newsdatabase;
        public static bool Bought;
        public static string soldtime;
        public static List<HistoryScanItem> AppHSI = new List<HistoryScanItem>();
        public static List<NewsItem> AppNews = new List<NewsItem>();
        public static UserProfile user;
        public static string sessionId;
        public static string email;
        public static string fullname;

        public App()
        {
            InitializeComponent();
            //App.HDatabase.DeleteAllAsync();
            //App.NDatabase.DeleteAllAsync();
            user = new UserProfile();
            List<UserProfile> userlist = App.UDatabase.GetItemsAsync().Result;
            Debug.WriteLine("số user"+userlist.Count);
            if (userlist.Count != 0)
            {
                App.sessionId = userlist[0].sessionid;
                App.fullname = userlist[0].fullname;
                App.email = userlist[0].email;
            }
            else App.sessionId = null;
            SvManager = new ServicesManager(new GetProduct());
            SvLoginManager = new ServicesManager(new GetUserProfile());
            SvNewsManager = new ServicesManager(new GetNews());
            SvLocationManager = new ServicesManager(new GetLocation());
            //MainPage = new NavigationPage(new Scannn.MainPage());
            MainPage = new NavigationPage(new Scannn.HomePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static HistoryDatabase HDatabase
        {
            get
            {
                if (historydatabase == null)
                {
                    Debug.WriteLine("Gán database tại vị trí");
                    historydatabase = new HistoryDatabase(DependencyService.Get<IFileHistory>().GetLocalFilePath("ScanSQLite.db3"));
                }
                return historydatabase;
            }
        }

        public static NewsDatabase NDatabase
        {
            get
            {
                if (newsdatabase == null)
                {
                    Debug.WriteLine("Gán database tại vị trí");
                    newsdatabase = new NewsDatabase(DependencyService.Get<IFileHistory>().GetLocalFilePath("ScanSQLite.db3"));
                }
                return newsdatabase;
            }
        }

        public static UserProfileDatabase UDatabase
        {
            get
            {
                if (userdatabase == null)
                {
                    Debug.WriteLine("Gán database tại vị trí");
                    userdatabase = new UserProfileDatabase(DependencyService.Get<IFileHistory>().GetLocalFilePath("ScanSQLite.db3"));
                }
                return userdatabase;
            }
        }
    }
}
