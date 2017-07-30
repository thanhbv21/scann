using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Scannn.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneScanner))]
namespace Scannn.iOS
{
    class PhoneScanner: ActiveScan
    {
        public void Scan()
        {
            //return "";
        }
    }
}