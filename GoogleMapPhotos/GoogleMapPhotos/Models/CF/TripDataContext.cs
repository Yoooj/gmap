using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TripGMap.Models.PhotoModel;
using TripGMap.Models.ProfileModel;
using TripGMap.Models.TripModel;

namespace GoogleMapPhotos.Models.CF
{
    public class TripDataContext : DbContext
    {
        public TripDataContext() : base("name=GMapConnString") { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}