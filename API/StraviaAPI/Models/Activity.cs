using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Activity Model 

namespace StraviaAPI.Models
{
    public class Activity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public float Kilometers { get; set; }
        public string Type { get; set; }
        public string AthleteUsername { get; set; }
    }
}
