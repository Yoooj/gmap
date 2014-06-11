using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GoogleMapPhotos.Models.PhotoModel;
using TripGMap.Models.PhotoModel;

namespace GoogleMapPhotos.Models.CF
{
    public class CfPhotoRepository : IPhotoRepository
    {
        private readonly TripDataContext db = new TripDataContext();

        public IEnumerable<Photo> GetAllPhotos()
        {
            return db.Photos.ToList();
        }

        public IEnumerable<Photo> GetPhotos(Func<Photo, bool> predicate)
        {
            return db.Photos.Where(predicate).ToList();
        }

        public IEnumerable<Photo> GetPhotos(int tripId)
        {
            return db.Photos.Where(n => n.TripId == tripId).ToList();
        }

        public Photo GetPhoto(int id)
        {
            return db.Photos.First(n => n.PhotoId == id);
        }

        public void RemovePhoto(int id)
        {
            db.Photos.Remove(GetPhoto(id));
            db.SaveChanges();
        }

        public void EditPhoto(Photo photo)
        {
            db.Entry(photo).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddPhoto(Photo photo)
        {
            db.Photos.Add(photo);
            db.SaveChanges();
        }

        public string GetAuthor(int photoId)
        {
            var tripId = db.Photos.First(n => n.PhotoId == photoId).TripId;
            var profileId = db.Trips.First(n => n.TripId == tripId).ProfileId;

            return db.Profiles.First(n => n.ProfileId == profileId).NickName;
        }
    }
}