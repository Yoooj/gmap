using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GoogleMapPhotos.Models.TripModel;
using TripGMap.Models.PhotoModel;
using TripGMap.Models.TripModel;

namespace GoogleMapPhotos.Models.CF
{
    public class CfTripRepository : ITripRepository
    {
        private readonly TripDataContext db = new TripDataContext();

        public IEnumerable<Trip> GetAllTrips()
        {
            return db.Trips.ToList();
        }

        public IEnumerable<Trip> GetTrips(Func<Trip, bool> predicate)
        {
            return db.Trips.Where(predicate).ToList();
        }

        public IEnumerable<Trip> GetTrips(string owner)
        {
            var firstOrDefault = db.Profiles.FirstOrDefault(n => n.Owner == owner);

            if (firstOrDefault == null) return null;

            var profileId = firstOrDefault.ProfileId;
            return db.Trips.Where(n => n.ProfileId == profileId).ToList();
        }

        public Trip GetTrip(int id)
        {
            return db.Trips.First(n => n.TripId == id);
        }

        public void RemoveTrip(int id)
        {
            db.Trips.Remove(GetTrip(id));
            db.SaveChanges();

            foreach (var source in db.Photos.Where(n => n.TripId == id))
            {
                db.Photos.Remove(source);
                db.SaveChanges();
            }
        }

        public void EditTrip(Trip trip)
        {
            db.Entry(trip).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddTrip(Trip trip, string owner)
        {
            var profileId = db.Profiles.First(n => n.Owner == owner).ProfileId;

            trip.ProfileId = profileId;

            db.Trips.Add(trip);
            db.SaveChanges();
        }
    }
}