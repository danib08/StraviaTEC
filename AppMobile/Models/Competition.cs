using SQLite;

namespace AppMobile.Models
{
    class Competition
    {
        [PrimaryKey]
        public string id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string route { get; set; }
        public string privaty { get; set; }
        public string bankAccount { get; set; }
        public string price { get; set; }
        public string activityID { get; set; }

    }
}