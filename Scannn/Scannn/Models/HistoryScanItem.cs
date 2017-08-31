using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Scannn.Models
{
    public class HistoryScanItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public DateTime
        public string itemcode { get; set; }
        public string itemname { get; set; }
        public string itemimage { get; set; }
        public string datetime { get; set; }
        public string companyname { get; set; }
        //
        //public s
    }
}
