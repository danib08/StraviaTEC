using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Atlhete in Competition Model

namespace StraviaAPI.Models
{
    public class Athlete_In_Competition
    {
        public string athleteID { get; set; }
        public string competitionID { get; set; }
        public string status { get; set; }
    }
}
