using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Athlete in Challenge Model

namespace StraviaAPI.Models
{
    public class Athlete_In_Challenge
    {
        public string athleteid { get; set; }
        public string challengeid { get; set; }
        public string status { get; set; }
        public float kilometers { get; set; }
    }
}
