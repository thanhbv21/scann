using System;
using System.IO;
using Android.Widget;
using Scannn.Droid;
using Xamarin.Forms;
using ZXing.Mobile;
using Scannn.Models;
using Scannn.Data;

[assembly: Dependency(typeof(FileHistory))]
namespace Scannn.Droid
{
    public class FileHistory : IFileHistory
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}