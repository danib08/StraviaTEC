using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Activity Model 

namespace StraviaAPI.Models
{
    public class Activity
    {
        public string id { get; set; }
        public string name { get; set; }
        public string route { get; set; }
        public DateTime date { get; set; }
        public string duration { get; set; }
        public float kilometers { get; set; }
        public string type { get; set; }
        public string athleteusername { get; set; }
    }
}
