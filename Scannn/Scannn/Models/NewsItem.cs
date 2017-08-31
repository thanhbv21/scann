using SQLite;

namespace Scannn.Models
{
    public class NewsItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public DateTime
        public string img { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public string content { get; set; }
        public string time { get; set; }
    }
}
