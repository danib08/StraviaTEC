using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Challenge Model

namespace StraviaAPI.Models
{
    public class Challenge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Privacy { get; set; }
        public float Kilometers { get; set; }
        public string Type { get; set; }
        
    }
}
