using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Rating;

namespace Team1_Final_Project.Controllers
{
    public class RatingsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Ratings
        public ActionResult Index()
        {
            return View(db.Ratings.ToList());
        }

        // GET: Ratings/AddSongReview
        public ActionResult AddSongReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MusicRating SongRating = new MusicRating();
            SongRating.ReviewedSong = db.Songs.Find(id);
            

            ViewBag.SongID = id;

            return View(SongRating);
        }

        // POST: Ratings/AddSongReview
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddSongReview([Bind(Include = "RatingID,RatingNumber,Review")] MusicRating SongRating, int SongID)
        {
            if (ModelState.IsValid)
            {
                SongRating.ReviewedSong = db.Songs.Find(SongID);
                db.Ratings.Add(SongRating);
                db.SaveChanges();
                return RedirectToAction("Details", "Songs", new { id = SongRating.ReviewedSong.SongID});
            }
            else
            {
                return View(SongRating);
            }
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicRating musicRating = db.Ratings.Find(id);
            if (musicRating == null)
            {
                return HttpNotFound();
            }
            return View(musicRating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MusicRatingID,RatingNumber,Review")] MusicRating musicRating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(musicRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musicRating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicRating musicRating = db.Ratings.Find(id);
            if (musicRating == null)
            {
                return HttpNotFound();
            }
            return View(musicRating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MusicRatingID,RatingNumber,Review")] MusicRating musicRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicRating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicRating musicRating = db.Ratings.Find(id);
            if (musicRating == null)
            {
                return HttpNotFound();
            }
            return View(musicRating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicRating musicRating = db.Ratings.Find(id);
            db.Ratings.Remove(musicRating);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
