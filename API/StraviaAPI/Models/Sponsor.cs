using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Sponsor Models

namespace StraviaAPI.Models
{
    public class Sponsor
    {
        public string id { get; set; }
        public string name { get; set; }
        public string bankAccount { get; set; }
        public string competitionID { get; set; }
        
    }
}
