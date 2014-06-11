using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripGMap.Models.TripModel;

namespace TripGMap.Models.ProfileModel
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }

        public string Owner { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string NickName { get; set; }

        public string WelcomeMessage { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public Profile()
        {
            WelcomeMessage = "Welcome Bro!";
            Trips = new HashSet<Trip>();
        }
    }
}