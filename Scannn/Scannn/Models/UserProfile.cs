using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannn.Models
{
    public class UserProfile
    {
        [PrimaryKey]
        public string id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string reg_date { get; set; }
        public string mobile { get; set; }
        public string sessionid { get; set; }
    }
}
