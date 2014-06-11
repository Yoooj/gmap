using System;

namespace GoogleMapPhotos.ModelView
{
    public class TripPhoto
    {
        public int TripId { get; set; }

        public int PhotoId { get; set; }

        public string Location { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool? IsPublic { get; set; }

        public TripPhoto()
        {
            Date = DateTime.Now;
        }

    }
}