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
        public string bankaccount { get; set; }
        public string competitionid { get; set; }
        
    }
}
