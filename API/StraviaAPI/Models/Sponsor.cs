using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaAPI.Models
{
    public class Sponsor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BankAccount { get; set; }
        public string CompetitionID { get; set; }
        public string ChallengeID { get; set; }
    }
}
