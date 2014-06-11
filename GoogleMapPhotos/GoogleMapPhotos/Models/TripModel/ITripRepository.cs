using System;
using System.Collections.Generic;
using TripGMap.Models.TripModel;

namespace GoogleMapPhotos.Models.TripModel
{
    public interface ITripRepository
    {
        IEnumerable<Trip> GetAllTrips();

        IEnumerable<Trip> GetTrips(Func<Trip, bool> predicate);

        IEnumerable<Trip> GetTrips(string owner);

        Trip GetTrip(int id);

        void RemoveTrip(int id);

        void EditTrip(Trip trip);

        void AddTrip(Trip trip, string owner);
    }
}
