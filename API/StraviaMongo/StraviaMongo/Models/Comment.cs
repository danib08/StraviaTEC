﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaMongo.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("activityid")]
        public string ActivityID { get; set; }

        [BsonElement("athleteid")]
        public string AthleteID { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }
    }
}
