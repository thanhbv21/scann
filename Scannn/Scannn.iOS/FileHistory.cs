using Scannn.iOS;
using System;
using Scannn.Data;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHistory))]
namespace Scannn.iOS
{
    public class FileHistory : IFileHistory
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            System.Diagnostics.Debug.WriteLine("Đường dẫn database: "+ libFolder + "|"+ filename);
            return Path.Combine(libFolder, filename);
        }
    }
}