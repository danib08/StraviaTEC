using SQLite;

namespace AppMobile.Models
{
    class AthleteInCompetition
    {
        [PrimaryKey]
        public string athleteID { get; set; }
        public string competitionID { get; set; }
        public string status { get; set; }
    }
}
