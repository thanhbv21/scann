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
        public static ServicesManager SvLocationManager { get; private set; }
        public static HistoryDatabase historydatabase;
        public static NewsDatabase newsdatabase;
        public static bool Bought;
        public static string soldtime;
        public static List<HistoryScanItem> AppHSI = new List<HistoryScanItem>();
        public static List<NewsItem> AppNews = new List<NewsItem>();

        public App()
        {
            InitializeComponent();
            //App.HDatabase.DeleteAllAsync();
            //App.NDatabase.DeleteAllAsync();
            SvManager = new ServicesManager(new GetProduct());
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
    }
}
