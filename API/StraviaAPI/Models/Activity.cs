using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaAPI.Models
{
    public class Activity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public float Kilometers { get; set; }
        public string Type { get; set; }
        public string ChallengeID { get; set; }
    }
}
