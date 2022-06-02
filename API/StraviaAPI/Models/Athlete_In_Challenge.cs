using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Athlete in Challenge Model

namespace StraviaAPI.Models
{
    public class Athlete_In_Challenge
    {
        public string athleteID { get; set; }
        public string challengeID { get; set; }
        public string status { get; set; }
    }
}
