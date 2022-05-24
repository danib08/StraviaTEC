using SQLite;

namespace AppMobile.Models
{
    class AthleteInChallenge
    {
        [PrimaryKey]
        public string athleteID { get; set; }
        public string challengeID { get; set; }
        public string status { get; set; }
    }
}
