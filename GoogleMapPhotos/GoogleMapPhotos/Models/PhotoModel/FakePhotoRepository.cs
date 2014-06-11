using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using GoogleMapPhotos.Models.CF;
using GoogleMapPhotos.Models.PhotoModel;
using TripGMap.Models.TripModel;

namespace TripGMap.Models.PhotoModel
{
    public class FakePhotoRepository : IPhotoRepository
    {
        private readonly TripDataContext db = new TripDataContext();
        public IEnumerable<Photo> GetAllPhotos()
        {
            return new List<Photo>
            {
                new Photo
                {
                    PhotoId = 0, 
                    Date = DateTime.Now,
                    Description = "desc",
                    Image = WriteBitmapIntoStream((GetNewBitMap(50, 50, Color.Blue))),
                    Latitude = 0,
                    Longtitude = 0,
                    TripId = 5,
                    IsPublic = true
                },
                new Photo
                {
                    PhotoId = 1, 
                    Date = DateTime.Now,
                    Description = "To było niesamowite! Oraz niewarygodnie dobre! No Way!",
                    Image = WriteBitmapIntoStream(GetNewBitMap(150, 150, Color.Red)),
                    Latitude = 10,
                    Longtitude = 10,
                    TripId = 5,
                    IsPublic = false
                }
            };
        }


        private static byte[] WriteBitmapIntoStream(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            var data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            return data;
        }

        private Bitmap GetNewBitMap(int x, int y, Color color)
        {
            var b = new Bitmap(1, 1);
            b.SetPixel(0, 0, color);

            return new Bitmap(b, x, y);
        }

        public IEnumerable<Photo> GetPhotos(Func<Photo, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photo> GetPhotos(int tripId)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhoto(int id)
        {
            if (id == 1)
            {
                return new Photo
                {
                    PhotoId = 1,
                    Date = DateTime.Now,
                    Description = "To było niesamowite! Oraz niewarygodnie dobre! No Way!",
                    Image = WriteBitmapIntoStream(GetNewBitMap(150, 150, Color.Red)),
                    Latitude = 10,
                    Longtitude = 10,
                    TripId = 5,
                    IsPublic = false  
                };
            }
            if (id == 0)
            {
                return new Photo
                {
                    PhotoId = 0,
                    Date = DateTime.Now,
                    Description = "desc",
                    Image = WriteBitmapIntoStream((GetNewBitMap(50, 50, Color.Blue))),
                    Latitude = 0,
                    Longtitude = 0,
                    TripId = 5,
                    IsPublic = true
                };
            }

            return null;
        }

        public void RemovePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public void EditPhoto(Photo trip)
        {
            throw new NotImplementedException();
        }

        public void AddPhoto(Photo trip)
        {
            throw new NotImplementedException();
        }

        public string GetAuthor(int photoId)
        {
            return "LaLa";
        }
    }
}