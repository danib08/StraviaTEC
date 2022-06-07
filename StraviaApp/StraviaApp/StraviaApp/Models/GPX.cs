using System;

namespace StraviaApp.Models
{
    class Gpx
    {

        public Gpx(Double lat, Double lon, Double ele, string time)
        {
            this.lat = lat;
            this.lon = lon;
            this.ele = ele;
            this.time = time;
        }
        public Double lat { get; set; }
        public Double lon { get; set; }
        public Double ele { get; set; }
        public string time { get; set; }

    }
}