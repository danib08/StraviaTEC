using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Competition Models

namespace StraviaAPI.Models
{
    public class Competition
    {
        public string id { get; set; }
        public string name { get; set; }
        public string route{ get; set; }
        public DateTime date { get; set; }
        public string privacy { get; set; }
        public string bankAccount { get; set; }
        public float price{ get; set; }
        public string activityID { get; set; }
    }
}
