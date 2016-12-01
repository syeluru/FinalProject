using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Purchases;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Controllers
{
    public class DiscountsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Discounts
        public ActionResult Index()
        {
            return View(db.Discounts.ToList());
        }

        // GET: Discounts/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: Discounts/CreateSongDiscount
        public ActionResult CreateSongDiscount()
        {
            ViewBag.AllSongs = GetAllSongs();
            return View();
        }

        // GET: Discounts/CreateSongDiscount
        public ActionResult CreateAlbumDiscount()
        {
            ViewBag.AllAlbums = GetAllAlbums();
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSongDiscount([Bind(Include = "DiscountID,DiscountAmount,IsActiveDiscount")] Discount discount, int SelectedSong)
        {
            if (ModelState.IsValid)
            {
                discount.DiscountedSong = db.Songs.Find(SelectedSong);
                db.Discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discount);
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAlbumDiscount([Bind(Include = "DiscountID,DiscountAmount,IsActiveDiscount")] Discount discount, int SelectedAlbum)
        {
            if (ModelState.IsValid)
            {
                discount.DiscountedAlbum = db.Albums.Find(SelectedAlbum);
                db.Discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discount);
        }

        // GET: Discounts/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountID,DiscountAmount,IsActiveDiscount,DiscountedSong,DiscountedAlbum")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discount);
        }


        // GET: Discounts/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Discount discount = db.Discounts.Find(id);
            db.Discounts.Remove(discount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public SelectList GetAllSongs()
        {
                    
            //find the list of songs
            var query = from m in db.Songs
                        select m;

            //convert to list and execute query
            List<Song> allSongs = query.ToList();

            //convert to selectlist
            SelectList allSongsList = new SelectList(allSongs, "SongID", "SongName");

            return allSongsList;
        }

        public SelectList GetAllAlbums()
        {

            //find the list of albums
            var query = from m in db.Albums
                        select m;

            //convert to list and execute query
            List<Album> allAlbums = query.ToList();

            //convert to multiselect
            SelectList allAlbumsList = new SelectList(allAlbums, "AlbumID", "AlbumName");

            return allAlbumsList;
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
