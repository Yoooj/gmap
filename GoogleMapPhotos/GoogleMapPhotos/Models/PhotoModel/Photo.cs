using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using TripGMap.Models.TripModel;

namespace TripGMap.Models.PhotoModel
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        public int TripId { get; set; }

        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longtitude { get; set; }

        public string Location { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public bool? IsPublic { get; set; }

        public Photo()
        {
            Date = DateTime.Now;
            Description = "";
            Location = "Set up your Location!";
            IsPublic = true;
        }
    }
}