using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripGMap.Models.PhotoModel;
using TripGMap.Models.ProfileModel;

namespace TripGMap.Models.TripModel
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        [StringLength(20, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Required]
        public string TripName { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public Trip()
        {
            Photos = new HashSet<Photo>();
        }
    }
}