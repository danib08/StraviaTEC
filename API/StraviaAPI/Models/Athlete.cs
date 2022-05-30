using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Athlete Model

namespace StraviaAPI.Models
{
    public class Athlete
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public int Age { get; set; }
        public DateTime  BirthDate {get; set;}
        public string Pass { get; set; }
        public string Nationality { get; set; }
        public string Category { get; set; } 
    }
}
 