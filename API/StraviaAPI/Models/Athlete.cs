using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Athlete Model

namespace StraviaAPI.Models
{
    public class Athlete
    {
        public string username { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string photo { get; set; }
        public int age { get; set; }
        public DateTime  birthdate {get; set;}
        public string pass { get; set; }
        public string nationality { get; set; }
        public string category { get; set; } 
    }
}
 