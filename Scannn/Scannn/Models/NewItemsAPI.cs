using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannn.Models
{
    public class NewItemsAPI
    {
        public int code { get; set; }
        public string desc { get; set; }
        public List<NewsItem> data { get; set; }
    }
}
