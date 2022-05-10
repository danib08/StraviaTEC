using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaAPI.Models
{
    public class Athlete_In_Competition
    {
        public string AthleteID { get; set; }
        public string CompetitionID { get; set; }
        public int Position { get; set; }
        public DateTime Time { get; set; }
    }
}
