using SQLite;

namespace AppMobile.Models
{
    class ActivityInChallenge
    {
        [PrimaryKey]
        public string activityID { get; set; }
        public string challengeID { get; set; }
    }
}
