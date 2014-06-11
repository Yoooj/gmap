using System;
using System.Collections.Generic;
using System.Linq;
using GoogleMapPhotos.Models.ProfileModel;
using TripGMap.Models.PhotoModel;
using TripGMap.Models.ProfileModel;
using TripGMap.Models.TripModel;

namespace GoogleMapPhotos.Models.CF
{
    public class CfProfileRepository : IProfileRepository
    {
        private readonly TripDataContext db = new TripDataContext();
        public IEnumerable<Profile> GetAllProfiles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Profile> GetProfiles(Func<Trip, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Profile GetProfile(string ownerName)
        {
            return db.Profiles.FirstOrDefault(n => n.Owner == ownerName);
        }

        public Profile GetProfile(int authorId)
        {
            return db.Profiles.FirstOrDefault(n => n.ProfileId == authorId);
        }

        public void RemoveProfile(int id)
        {
            throw new NotImplementedException();
        }

        public void EditProfile(Profile profile)
        {
            throw new NotImplementedException();
        }

        public void AddProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
        }

        public IEnumerable<Photo> GetAllPictures(string author)
        {
            var profileId = db.Profiles.First(n => n.NickName == author).ProfileId;
            var trips = db.Trips.Where(n => n.ProfileId == profileId).ToList();

            var pictures = new List<Photo>();

            foreach (var trip in trips)
            {
                pictures.AddRange(db.Photos.Where(n => n.TripId == trip.TripId));
            }

            return pictures;
        }

        public string GetTrip(int tripId)
        {
            return db.Trips.FirstOrDefault(n => n.TripId == tripId).TripName;
        }
    }
}