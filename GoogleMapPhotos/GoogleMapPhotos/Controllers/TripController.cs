using System.Linq;
using System.Web.Mvc;
using GoogleMapPhotos.Models.TripModel;
using TripGMap.Models.PhotoModel;
using TripGMap.Models.TripModel;

namespace GoogleMapPhotos.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripRepository db;

        public TripController(ITripRepository db)
        {
            this.db = db;
        }

        public ActionResult GetTripName(int tripId)
        {
            var tripName = db.GetTrips(User.Identity.Name).First(n => n.TripId == tripId).TripName;
            return View(tripName);
        }

        // GET: Trip
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyTrips()
        {
            var trips = db.GetTrips(User.Identity.Name);
            return View("MyTrips", trips);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.AddTrip(trip, User.Identity.Name);
                return RedirectToAction("MyTrips");
            }
            return View(trip);
        }


        public ActionResult Delete(int tripId)
        {
            var trip = db.GetTrip(tripId);
            return View(trip);
        }

        // POST: Bla/Delete/5
        [HttpPost]
        public ActionResult Delete(int tripId, FormCollection collection)
        {
            try
            {
                db.RemoveTrip(tripId);
                return RedirectToAction("MyTrips");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int tripId)
        {
            var trip = db.GetTrip(tripId);
            return View(trip);
        }

        // POST: Bla/Edit/5
        [HttpPost]
        public ActionResult Edit(int tripId, Trip trip)
        {
            try
            {
                var editTrip = db.GetTrip(tripId);

                editTrip.TripName = trip.TripName;

                db.RemoveTrip(tripId);
                db.AddTrip(editTrip, User.Identity.Name);

                return RedirectToAction("MyTrips");
            }
            catch
            {
                return View();
            }
        }
    }
}
