using Microsoft.AspNet.Identity;
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
                AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());

                SongRating.ReviewedSong = db.Songs.Find(SongID);

                userLoggedIn.Ratings.Add(SongRating);

                db.Ratings.Add(SongRating);
                db.SaveChanges();

                
                return RedirectToAction("Details", "Songs", new { id = SongRating.ReviewedSong.SongID});


            }
            else
            {
                ViewBag.SongID = SongID;
                return View(SongRating);
            }
        }

        // GET: Ratings/AddArtistReview
        public ActionResult AddArtistReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MusicRating ArtistRating = new MusicRating();
            ArtistRating.ReviewedArtist = db.Artists.Find(id);


            ViewBag.ArtistID = id;

            return View(ArtistRating);
        }

        // POST: Ratings/AddArtistReview
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddArtistReview([Bind(Include = "RatingID,RatingNumber,Review")] MusicRating ArtistRating, int ArtistID)
        {
            if (ModelState.IsValid)
            {
                AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());

                ArtistRating.ReviewedArtist = db.Artists.Find(ArtistID);
                db.Ratings.Add(ArtistRating);
                db.SaveChanges();

                userLoggedIn.Ratings.Add(ArtistRating);

                return RedirectToAction("Details", "Artists", new { id = ArtistRating.ReviewedArtist.ArtistID });
            }
            else
            {
                ViewBag.ArtistID = ArtistID;
                return View(ArtistRating);
            }
        }

        // GET: Ratings/AddAlbumReview
        public ActionResult AddAlbumReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MusicRating AlbumRating = new MusicRating();
            AlbumRating.ReviewedAlbum = db.Albums.Find(id);


            ViewBag.AlbumID = id;

            return View(AlbumRating);
        }

        // POST: Ratings/AddAlbumReview
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddAlbumReview([Bind(Include = "RatingID,RatingNumber,Review")] MusicRating AlbumRating, int AlbumID)
        {
            if (ModelState.IsValid)
            {
                AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());

                AlbumRating.ReviewedAlbum = db.Albums.Find(AlbumID);
                db.Ratings.Add(AlbumRating);
                db.SaveChanges();

                userLoggedIn.Ratings.Add(AlbumRating);

                return RedirectToAction("Details", "Albums", new { id = AlbumRating.ReviewedAlbum.AlbumID });
            }
            else
            {
                ViewBag.AlbumID = AlbumID;
                return View(AlbumRating);
            }
        }

        // GET: Ratings/EditSongReview/5
        public ActionResult EditSongReview(int? id)
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

        // POST: Ratings/EditSongReview/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSongReview([Bind(Include = "MusicRatingID,RatingNumber,Review")] MusicRating musicRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicRating);
        }

        // GET: Ratings/EditAlbumReview/5
        public ActionResult EditAlbumReview(int? id)
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

        // POST: Ratings/EditAlbumReview/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAlbumReview([Bind(Include = "MusicRatingID,RatingNumber,Review")] MusicRating musicRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicRating);
        }

        // GET: Ratings/EditArtistReview/5
        public ActionResult EditArtistReview(int? id)
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

        // POST: Ratings/EditArtistReview/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArtistReview([Bind(Include = "MusicRatingID,RatingNumber,Review")] MusicRating musicRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicRating);
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
