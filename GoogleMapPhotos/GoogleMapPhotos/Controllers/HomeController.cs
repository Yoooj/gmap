using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GoogleMapPhotos.Models.PhotoModel;
using TripGMap.Models.PhotoModel;
using TripGMap.ModelView;

namespace GoogleMapPhotos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoRepository photoRepository;

        public HomeController(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }

        public ActionResult Index()
        {
            var photos = photoRepository.GetAllPhotos();

            var markers = GetMarkers(photos);
            return View(markers);
        }

        public List<Marker> GetMarkers(IEnumerable<Photo> photos)
        {
            List<Marker> markers;

            if (User.Identity.IsAuthenticated)
            {
                markers = photos.Where(n => n.Latitude != 0 && n.Longtitude != 0).Select(photo => new Marker
                {
                    ImageId = photo.PhotoId,
                    Latitude = photo.Latitude,
                    Longtitude = photo.Longtitude,
                    Description = photo.Description,
                    Author = photoRepository.GetAuthor(photo.PhotoId)

                }).ToList();
            }

            else
            {
                markers = photos.Where(n => n.IsPublic.Equals(false) && n.Latitude != 0 && n.Longtitude != 0).Select(photo => new Marker
                {
                    ImageId = photo.PhotoId,
                    Latitude = photo.Latitude,
                    Longtitude = photo.Longtitude,
                    Description = photo.Description,
                    Author = photoRepository.GetAuthor(photo.PhotoId)

                }).ToList();
            }

            return markers;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowMarkers(int tripId)
        {
            var photos = photoRepository.GetPhotos(tripId);

            var markers = GetMarkers(photos);
            return View("Index", markers);
        }
    }
}