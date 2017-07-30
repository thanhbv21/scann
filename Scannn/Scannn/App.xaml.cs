using Scannn.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Scannn
{
    public partial class App : Application
    {
        public static ServicesManager SvManager { get; private set; }
        //public static ResultManager NavigationResult { get; private set; }
        public App()
        {
            InitializeComponent();

            SvManager = new ServicesManager(new GetProduct());
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
    }
}
