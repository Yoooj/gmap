using System;
using System.Collections.Generic;
using TripGMap.Models.PhotoModel;
using TripGMap.Models.ProfileModel;
using TripGMap.Models.TripModel;

namespace GoogleMapPhotos.Models.ProfileModel
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetAllProfiles();

        IEnumerable<Profile> GetProfiles(Func<Trip, bool> predicate);

        Profile GetProfile(string ownerName);

        Profile GetProfile(int authorId);

        void RemoveProfile(int id);

        void EditProfile(Profile profile);

        void AddProfile(Profile profile);

        IEnumerable<Photo> GetAllPictures(string author);

        string GetTrip(int tripId);
    }
}