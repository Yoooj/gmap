using System.Collections.Generic;
using System.Web.Mvc;
using GoogleMapPhotos.Models.ProfileModel;
using GoogleMapPhotos.ModelView;
using TripGMap.Models.ProfileModel;

namespace GoogleMapPhotos.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository db;

        public ProfileController(IProfileRepository db)
        {
            this.db = db;
        }

        // GET: Profile
        public ActionResult Index(string author)
        {
            var pictures = db.GetAllPictures(author);

            var profileWithPictures = new ProfileWithPictures {NickName = author, Photos = new List<PhotoShortInfo>()};

            foreach (var picture in pictures)
            {
                profileWithPictures.Photos.Add(new PhotoShortInfo
                {
                    Description = picture.Description,
                    PhotoId = picture.PhotoId,
                    TripName = db.GetTrip(picture.TripId)
                });
            }

            return View(profileWithPictures);
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            var profile = db.GetProfile(User.Identity.Name);

            return View("MyProfile", profile);
        }
    }
}
