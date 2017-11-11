using Scannn.Data;
using Scannn.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(CheckInternet))]
namespace Scannn.iOS
{
    class CheckInternet : ICheckInternet
    {
        public bool IsInternet()
        {
            //bool isOnline = false;
            //if()
            NetworkStatus internetStatus = Reachability.InternetConnectionStatus();
            if (!Reachability.IsHostReachable("http://google.com")) return false;
            //if (internetStatus.Equals(NetworkStatus.NotReachable)) return false;
            
            return true;
        }
    }
}
