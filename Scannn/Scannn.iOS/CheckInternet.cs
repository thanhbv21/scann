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
            return true;
        }
    }
}
