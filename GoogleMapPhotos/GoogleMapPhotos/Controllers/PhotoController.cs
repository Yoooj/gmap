using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoogleMapPhotos.Models.PhotoModel;
using GoogleMapPhotos.ModelView;
using TripGMap.Models.PhotoModel;

namespace GoogleMapPhotos.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository db;

        public PhotoController(IPhotoRepository db)
        {
            this.db = db;
        }

        public ActionResult GetMarkerContent(int photoId)
        {
            var photo = db.GetPhoto(photoId);

            var markerContent = new MarkerContent
            {
                Author = db.GetAuthor(photoId),
                Description = photo.Description,
                PhotoId = photoId
            };

            return PartialView("MarkerContent", markerContent);
        }

        // GET: Photo
        public ActionResult Index(int tripId)
        {
            var photos = db.GetPhotos(tripId);

            var tripPhotos = photos.Select(photo => new TripPhoto
            {
                Date = photo.Date, 
                Description = photo.Description, 
                IsPublic = photo.IsPublic, 
                Location = photo.Location, 
                PhotoId = photo.PhotoId, 
                TripId = photo.TripId

            }).ToList();

            return View(tripPhotos);
        }

        [Authorize]
        // GET: Photo/Create
        public ActionResult Create(string tripUrl)
        {
            int tripId = GetTripId(tripUrl);
            return View("Create", tripId);
        }

        [HttpPost]
        public ActionResult Create(Photo photo)
        {
            return View("Create");
        }

        private int GetTripId(string tripUrl)
        {
            return Convert.ToInt32(tripUrl.Substring(tripUrl.LastIndexOf("=", StringComparison.Ordinal) + 1));
        }

        // POST: Photo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult SaveDropzoneJsUploadedFiles(int tripId)
        {
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];

                db.AddPhoto(new Photo
                {
                    Image = GetBytesFromFile(file),
                    TripId = tripId
                });
            }

            return null;
        }

        public byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            var target = new MemoryStream();
            file.InputStream.CopyTo(target);
            return target.ToArray();
        }

        public ActionResult GetImage(int photoId)
        {
            var byteArrayImage = db.GetPhoto(photoId).Image;
            return File(byteArrayImage, "image/jpeg");
        }

        [Authorize]
        // GET: Photo/Create
        public ActionResult SetLocation(int photoId)
        {
            var photo = db.GetPhoto(photoId);
            var tripPhoto = new TripPhoto
            {
                Location = photo.Location,
                PhotoId = photo.PhotoId,
                TripId = photo.TripId,
                Latitude = photo.Latitude,
                Longtitude = photo.Longtitude,
                IsPublic = true
            };
            return View("SetLocation", tripPhoto);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SetLocation(Photo photo)
        {
            var realPhoto = db.GetPhoto(photo.PhotoId);

            realPhoto.Location = photo.Location;
            realPhoto.Longtitude = photo.Longtitude;
            realPhoto.Latitude = photo.Latitude;

            db.RemovePhoto(photo.PhotoId);
            db.AddPhoto(realPhoto);
            return RedirectToAction("Index",new{tripId = realPhoto.TripId });
        }

        public ActionResult Delete(int photoId)
        {
            var photo = db.GetPhoto(photoId);
            return View(photo);
        }

        // POST: Bla/Delete/5
        [HttpPost]
        public ActionResult Delete(int photoId, FormCollection collection)
        {
            try
            {
                var tripId_ = db.GetPhoto(photoId).TripId; 
                db.RemovePhoto(photoId);           
                return RedirectToAction("Index", new{ tripId = tripId_});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int photoId)
        {
            var photo = db.GetPhoto(photoId);
            return View(photo);
        }

        // POST: Bla/Edit/5
        [HttpPost]
        public ActionResult Edit(int photoId, Photo photo)
        {
            try
            {
                var tripId = db.GetPhoto(photoId).TripId;
                var editPhoto = db.GetPhoto(photoId);

                editPhoto.IsPublic = photo.IsPublic;
                editPhoto.Description = photo.Description;
                editPhoto.Date = photo.Date;

                db.RemovePhoto(photoId);
                db.AddPhoto(editPhoto);

                return RedirectToAction("Index", new { tripId = tripId });
            }
            catch
            {
                return View();
            }
        }

    }
}
