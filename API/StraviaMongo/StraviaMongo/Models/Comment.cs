using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaMongo.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [BsonElement("ActivityID")]
        public string ActivityID { get; set; }
        [BsonElement("AthleteID")]
        public string AthleteID { get; set; }
        [BsonElement("Text")]
        public string Text { get; set; }
    }
}
