using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaAPI.Models
{
    public class Competition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Route{ get; set; }
        public DateTime Date { get; set; }
        public string Privacy { get; set; }
        public string BankAccount { get; set; }
        public float Price{ get; set; }
        public string ActivityID { get; set; }
    }
}
