using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Scannn.Droid;
using Xamarin.Forms;
using Scannn.Data;
using Android.Net;

[assembly: Dependency(typeof(CheckInternet))]
namespace Scannn.Droid
{
    class CheckInternet : ICheckInternet
    {
        public bool IsInternet()
        {
            var mContext = Android.App.Application.Context;
            ConnectivityManager connectivityManager = (ConnectivityManager)mContext.GetSystemService(Android.App.Activity.ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
           
            System.Diagnostics.Debug.WriteLine("ktra internet");
            bool isOnline = false;
            try
            {
                isOnline = networkInfo.IsConnected;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Éo hiểu sao lại lỗi" + ex);
            }
            return isOnline;
        }
    }
}