using SQLite;

namespace AppMobile.Models
{
    class Athlete
    {
        [PrimaryKey]
        public string username { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string birthdate { get; set; }
        public string nationality { get; set;}
        public int age { get; set; }
        public string pass { get; set; }
        public string category { get; set; }
        public string photo { get; set; }

    }
}