using System;
using System.Collections.Generic;
using TripGMap.Models.PhotoModel;

namespace GoogleMapPhotos.Models.PhotoModel
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetAllPhotos();

        IEnumerable<Photo> GetPhotos(Func<Photo, bool> predicate);

        IEnumerable<Photo> GetPhotos(int tripId);

        Photo GetPhoto(int id);

        void RemovePhoto(int id);

        void EditPhoto(Photo trip);

        void AddPhoto(Photo trip);
        string GetAuthor(int photoId);
    }
}
