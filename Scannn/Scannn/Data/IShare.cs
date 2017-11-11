using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scannn.Data
{
    public interface IShare
    {
        Task ShareAsync(ImageSource image, string message);
    }
}
