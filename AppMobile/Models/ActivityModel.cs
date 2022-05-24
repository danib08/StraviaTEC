using SQLite;

namespace AppMobile.Models
{
    class ActivityModel
    {
        [PrimaryKey]
        public string id { get; set; }
        public string name { get; set; }
        public string route { get; set; }
        public string date { get; set; }
        public string duration { get; set; }
        public int kilometers { get; set; }
        public string type { get; set; }
        public string athleteUsername { get; set; }
    }
}