﻿using SQLite;

namespace StraviaApp.Models
{
    class ActivityModel
    {
        [PrimaryKey]
        public string id { get; set; }
        public string name { get; set; }
        public string route { get; set; }
        public string date { get; set; }
        public string duration { get; set; }
        public string kilometers { get; set; }
        public string type { get; set; }
        public string athleteUsername { get; set; }
    }
}