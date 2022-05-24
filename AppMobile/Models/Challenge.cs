using SQLite;

namespace AppMobile.Models{
    class Challenge{
        [PrimaryKey]
        public string id { get; set; }
        public string name { get; set; }
        public string endDate { get; set; }
        public string startDate { get; set; }
        public string privaty { get; set; }
        public string kilometers { get; set; }
        public string type { get; set; }

    }
}