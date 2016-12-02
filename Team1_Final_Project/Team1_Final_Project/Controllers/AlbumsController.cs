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
    public class AlbumsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Albums
        public ActionResult Index()
        {
            return View(db.Albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }
        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.AllGenres = GetAllGenres();
            ViewBag.AllArtists = GetAllArtists();

            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,AlbumName,AlbumPrice")] Album album, int[] SelectedArtists, int[] SelectedGenres, string NewGenreName)
        {
            if (ModelState.IsValid)
            {
                //if there are genres to add, add them
                if (SelectedGenres != null)
                {
                    foreach (int GenreID in SelectedGenres)
                    {
                        Genre genreToAdd = db.Genres.Find(GenreID);
                        album.AlbumGenres.Add(genreToAdd);
                    }
                }

                if (NewGenreName != null && NewGenreName != "")
                {
                    Genre NewGenre = new Genre();
                    NewGenre.GenreName = NewGenreName;
                    db.Genres.Add(NewGenre);
                    db.SaveChanges();

                    album.AlbumGenres.Add(db.Genres.Last(a => a.GenreName == NewGenreName));


                }

                //if there are artists to add, add them
                if (SelectedArtists != null)
                {
                    foreach (int ArtistID in SelectedArtists)
                    {
                        Artist artistToAdd = db.Artists.Find(ArtistID);
                        album.AlbumArtists.Add(artistToAdd);
                    }
                }

                //update the rest of the fields
                album.AlbumName = album.AlbumName;
                album.AlbumPrice = album.AlbumPrice;
                


                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllGenres = GetAllGenres();
            ViewBag.AllArtists = GetAllArtists();
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            ViewBag.AllGenres = GetAllGenres(album);
            ViewBag.AllArtists = GetAllArtists(album);

            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumID,AlbumName,AlbumPrice")] Album album, int[] SelectedArtists, int[] SelectedGenres, string NewGenreName )
        {

            if (ModelState.IsValid)
            {

                //Find associated artist
                Album albumToChange = db.Albums.Find(album.AlbumID);

                //remove any existing genres
                albumToChange.AlbumGenres.Clear();
                albumToChange.AlbumArtists.Clear();

                if (SelectedGenres != null)
                {
                    foreach (int GenreID in SelectedGenres)
                    {
                        Genre genreToAdd = db.Genres.Find(GenreID);
                        albumToChange.AlbumGenres.Add(genreToAdd);
                    }
                }

                //if there are artists to add, add them
                if (SelectedArtists != null)
                {
                    foreach (int ArtistID in SelectedArtists)
                    {
                        Artist artistToAdd = db.Artists.Find(ArtistID);
                        albumToChange.AlbumArtists.Add(artistToAdd);
                    }
                }

                //if there are genres to add, add them
                if (NewGenreName != null && NewGenreName != "")
                {
                    Genre NewGenre = new Genre();
                    NewGenre.GenreName = NewGenreName;
                    db.Genres.Add(NewGenre);
                    db.SaveChanges();


                    albumToChange.AlbumGenres.Add(db.Genres.First(a => a.GenreName == NewGenreName));
                }


 
                //update the rest of the fields
                albumToChange.AlbumName = album.AlbumName;
                albumToChange.AlbumPrice = album.AlbumPrice;
                


                db.Entry(albumToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllGenres = GetAllGenres(album);
            ViewBag.AllArtists = GetAllArtists(album);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public MultiSelectList GetAllGenres()
        {
            //find the list of members
            var query = from m in db.Genres
                        select m;

            //convert to list and execute query
            List<Genre> allGenres = query.ToList();

            //convert to multiselect
            MultiSelectList allGenresList = new MultiSelectList(allGenres, "GenreID", "GenreName");
         
            return allGenresList;
        }

        public MultiSelectList GetAllGenres(Album @album)
        {
            //find the list of members
            var query = from m in db.Genres
                        select m;


            //convert to list and execute query
            List<Genre> allGenres = query.ToList();

            //create list of selected members
            List<Int32> SelectedGenres = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Genre g in @album.AlbumGenres)
            {
                SelectedGenres.Add(g.GenreID);
            }

            //convert to multiselect
            MultiSelectList allGenresList = new MultiSelectList(allGenres, "GenreID", "GenreName", SelectedGenres);

            return allGenresList;
        }

        public MultiSelectList GetAllArtists()
        {
            //find the list of members
            var query = from m in db.Artists
                        select m;

            //convert to list and execute query
            List<Artist> allArtists = query.ToList();

            //convert to multiselect
            MultiSelectList allArtistsList = new MultiSelectList(allArtists, "ArtistID", "ArtistName");

            return allArtistsList;
        }

        public MultiSelectList GetAllArtists(Album @album)
        {
            //find the list of members
            var query = from m in db.Artists
                        select m;

            //convert to list and execute query
            List<Artist> allArtists = query.ToList();

            //create list of selected members
            List<Int32> SelectedArtists = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Artist g in @album.AlbumArtists)
            {
                SelectedArtists.Add(g.ArtistID);
            }

            //convert to multiselect
            MultiSelectList allArtistsList = new MultiSelectList(allArtists, "ArtistID", "ArtistName", SelectedArtists);

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
