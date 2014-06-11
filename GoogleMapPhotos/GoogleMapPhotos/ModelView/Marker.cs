using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TripGMap.ModelView
{
    public class Marker
    {
        public double Latitude { get; set; }

        public double Longtitude { get; set; }

        public int ImageId { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
    }
}