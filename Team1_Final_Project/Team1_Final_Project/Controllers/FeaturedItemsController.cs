using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Controllers
{
    public class FeaturedItemsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: FeaturedItems
        public ActionResult Index()
        {
            return View(db.FeaturedItems.ToList());
        }

        // GET: FeaturedItems/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            if (featuredItem == null)
            {
                return HttpNotFound();
            }
            return View(featuredItem);
        }

        // GET: FeaturedItems/Create
        public ActionResult SetFeaturedSong()
        {
            ViewBag.AllSongs = GetAllSongs();
            return View();
        }

        // GET: FeaturedItems/Create
        public ActionResult SetFeaturedArtist()
        {
            ViewBag.AllArtists = GetAllArtists();
            return View();
        }

        // GET: FeaturedItems/Create
        public ActionResult SetFeaturedAlbum()
        {
            ViewBag.AllAlbums = GetAllAlbums();
            return View();
        }

        //// POST: FeaturedItems/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FeaturedItemID,IsActiveFeaturedItem")] FeaturedItem featuredItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.FeaturedItems.Add(featuredItem);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(featuredItem);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetFeaturedSong([Bind(Include = "FeaturedItemID,IsActiveFeaturedItem")] FeaturedItem featuredItem, int SelectedSong)
        {
            if (ModelState.IsValid)
            {
                try {
                    // Find the currently featured item (if any) and make it not featured
                    var query = from c in db.FeaturedItems
                                where c.IsActiveFeaturedItem == true
                                select c;

                    List<FeaturedItem> TheFeaturedItem = query.ToList();
                    if (TheFeaturedItem.Count() > 0)
                    {
                        db.FeaturedItems.Find(TheFeaturedItem[0].FeaturedItemID).IsActiveFeaturedItem = false;

                    }


                    // Create a new entry in the featured items database
                    featuredItem.FeaturedSong = db.Songs.Find(SelectedSong);
                    db.FeaturedItems.Add(featuredItem);
                    db.SaveChanges();
                    return RedirectToAction("ManagerDashboard", "Account", null);
                } catch (Exception e)
                {
                    ViewBag.Exception = e.ToString();
                    return View(featuredItem);

                }


            }

            return View(featuredItem);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetFeaturedAlbum([Bind(Include = "FeaturedItemID,IsActiveFeaturedItem")] FeaturedItem featuredItem, int SelectedAlbum)
        {
            if (ModelState.IsValid)
            {

                try {
                    // Find the currently featured item (if any) and make it not featured
                    var query = from c in db.FeaturedItems
                                where c.IsActiveFeaturedItem == true
                                select c;

                    List<FeaturedItem> TheFeaturedItem = query.ToList();

                    if (TheFeaturedItem.Count() > 0)
                    {
                        db.FeaturedItems.Find(TheFeaturedItem[0].FeaturedItemID).IsActiveFeaturedItem = false;

                    }

                    // Create a new entry in the featured items database
                    featuredItem.FeaturedAlbum = db.Albums.Find(SelectedAlbum);
                    db.FeaturedItems.Add(featuredItem);
                    db.SaveChanges();
                    return RedirectToAction("ManagerDashboard", "Account", null);


                }
                catch (Exception e) {
                    ViewBag.Exception = e.ToString();
                    return View(featuredItem);

                }
            }

            return View(featuredItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetFeaturedArtist([Bind(Include = "FeaturedItemID,IsActiveFeaturedItem")] FeaturedItem featuredItem, int SelectedArtist)
        {
            if (ModelState.IsValid)
            {
                try {
                    // Find the currently featured item (if any) and make it not featured
                    var query = from c in db.FeaturedItems
                                where c.IsActiveFeaturedItem == true
                                select c;

                    List<FeaturedItem> TheFeaturedItem = query.ToList();

                    if (TheFeaturedItem.Count() > 0)
                    {
                        db.FeaturedItems.Find(TheFeaturedItem[0].FeaturedItemID).IsActiveFeaturedItem = false;

                    }

                    // Create a new entry in the featured items database
                    featuredItem.FeaturedArtist = db.Artists.Find(SelectedArtist);
                    db.FeaturedItems.Add(featuredItem);
                    db.SaveChanges();
                    return RedirectToAction("ManagerDashboard", "Account", null);

                } catch (Exception e)
                {
                    ViewBag.Exception = e.ToString();
                    return View(featuredItem);
                }
            }

            return View(featuredItem);
        }



        // GET: FeaturedItems/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            if (featuredItem == null)
            {
                return HttpNotFound();
            }
            return View(featuredItem);
        }

        // POST: FeaturedItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeaturedItemID,IsActiveFeaturedItem")] FeaturedItem featuredItem)
        {
            if (ModelState.IsValid)
            {

                FeaturedItem itemToChange = db.FeaturedItems.Find(featuredItem.FeaturedItemID);

                // check to see that there are not two featured items
                var query = from c in db.FeaturedItems
                            where c.IsActiveFeaturedItem == true
                            select c;

                List<FeaturedItem> AllActiveFeaturedItems = query.ToList();

                if (AllActiveFeaturedItems.Count() > 0)
                {
                    db.FeaturedItems.Find(AllActiveFeaturedItems[0].FeaturedItemID).IsActiveFeaturedItem = false;

                }

                // the rest of the shizz
                itemToChange.IsActiveFeaturedItem = featuredItem.IsActiveFeaturedItem;
                db.Entry(itemToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featuredItem);
        }

        // GET: FeaturedItems/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            if (featuredItem == null)
            {
                return HttpNotFound();
            }
            return View(featuredItem);
        }

        // POST: FeaturedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            db.FeaturedItems.Remove(featuredItem);
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

        public SelectList GetAllArtists()
        {

            //find the list of albums
            var query = from m in db.Artists
                        select m;

            //convert to list and execute query
            List<Artist> allArtists = query.ToList();

            //convert to multiselect
            SelectList allArtistsList = new SelectList(allArtists, "ArtistID", "ArtistName");

            return allArtistsList;
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
