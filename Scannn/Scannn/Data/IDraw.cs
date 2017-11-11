using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scannn.Data
{
    public interface IDraw
    {
        Task<ImageSource> Drawtext(ImageSource image, string message, string author);
    }
}
