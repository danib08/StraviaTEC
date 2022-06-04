using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Atlhete in Competition Model

namespace StraviaAPI.Models
{
    public class Athlete_In_Competition
    {
        public string athleteid { get; set; }
        public string competitionid { get; set; }
        public string status { get; set; }
        public string receipt { get; set; }
        public string duration { get; set; }
    }
}
