using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Challenge Model

namespace StraviaAPI.Models
{
    public class Challenge
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string privacy { get; set; }
        public float kilometers { get; set; }
        public string type { get; set; }
        
    }
}
