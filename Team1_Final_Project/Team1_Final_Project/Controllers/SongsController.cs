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
    public class SongsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Songs
        public ActionResult Index()
        {
            return View(db.Songs.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        /* create post
         * 
         * Bind[Include(fields)] MusicRating SongRating, int SongID {
         *      SongRating.Song = db.Songs.Find(songID);
         *      db.Ratings.Add(MusicRating)
         *      db.SaveChanges()
         * }
         * 
         * 
         * in your view:
         * <input type="number" name = "SongID" value="@ViewBag.SongID" style="display:none" />
         */

       
        // GET: Songs/Create
        [Authorize (Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.AllGenres = GetAllGenres();
            ViewBag.AllArtists = GetAllArtists();
            ViewBag.AllAlbums = GetAllAlbums();
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "SongID,SongName,SongPrice")] Song song, int[] SelectedArtists, int[] SelectedGenres, int[] SelectedAlbums, string NewGenreName)
        {
            if (ModelState.IsValid)
            {
                //associate genres with song
                if (SelectedGenres != null)
                {
                    foreach (int genreID in SelectedGenres)
                    {
                        Genre GenreToAdd = db.Genres.Find(genreID);
                        song.SongGenres.Add(GenreToAdd);
                    }

                }

                //associate artists with song
                if (SelectedArtists != null)
                {
                    foreach (int artistID in SelectedArtists)
                    {
                        Artist ArtistToAdd = db.Artists.Find(artistID);
                        song.SongArtists.Add(ArtistToAdd);
                    }

                }

                if (SelectedAlbums != null)
                {
                    foreach (int albumID in SelectedAlbums)
                    {
                        Album albumToAdd = db.Albums.Find(albumID);
                        song.SongAlbums.Add(albumToAdd);
                    }
                }

                if (NewGenreName != null && NewGenreName != "")
                {
                    Genre NewGenre = new Genre();
                    NewGenre.GenreName = NewGenreName;
                    db.Genres.Add(NewGenre);
                    db.SaveChanges();

                    song.SongGenres.Add(db.Genres.Last(a => a.GenreName == NewGenreName));


                }

                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllGenres = GetAllGenres(song);
            ViewBag.AllArtists = GetAllArtists(song);
            ViewBag.AllAlbums = GetAllAlbums(song);
            return View(song);
        }

        // GET: Songs/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllGenres = GetAllGenres(song);
            ViewBag.AllArtists = GetAllArtists(song);
            ViewBag.AllAlbums = GetAllAlbums(song);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit([Bind(Include = "SongID,SongName,SongPrice")] Song song, int[] SelectedGenres, int[] SelectedArtists, int[] SelectedAlbums, string NewGenreName)
        {
            if (ModelState.IsValid)
            {
                Song songToChange = db.Songs.Find(song.SongID);

                //remove any existing genres and artists
                songToChange.SongGenres.Clear();
                songToChange.SongArtists.Clear();
                songToChange.SongAlbums.Clear();

                //if there are genres to add, add them
                if (SelectedGenres != null)
                {
                    foreach (int genreID in SelectedGenres)
                    {
                        Genre genreToAdd = db.Genres.Find(genreID);
                        songToChange.SongGenres.Add(genreToAdd);
                    }
                }

                //if there are genres to add, add them
                if (SelectedAlbums != null)
                {
                    foreach (int albumID in SelectedAlbums)
                    {
                        Album albumToAdd = db.Albums.Find(albumID);
                        songToChange.SongAlbums.Add(albumToAdd);
                    }
                }

                if (NewGenreName != null && NewGenreName != "")
                {
                    Genre NewGenre = new Genre();
                    NewGenre.GenreName = NewGenreName;
                    db.Genres.Add(NewGenre);
                    db.SaveChanges();

                    songToChange.SongGenres.Add(db.Genres.Last(a => a.GenreName == NewGenreName));


                }

                //if there are genres to add, add them
                if (SelectedArtists != null)
                {
                    foreach (int artistID in SelectedArtists)
                    {
                        Artist artistToAdd = db.Artists.Find(artistID);
                        songToChange.SongArtists.Add(artistToAdd);
                    }
                }

                //update the rest of the fields
                songToChange.SongName = song.SongName;
                songToChange.SongPrice = song.SongPrice;
                //songToChange.SongDiscount = song.SongDiscount;

                db.Entry(songToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //re-populate genre and artist list and add to viewbag
            ViewBag.AllGenres = GetAllGenres(song);
            ViewBag.AllArtists = GetAllArtists(song);
            ViewBag.AllAlbums = GetAllAlbums(song);

            db.Entry(song).State = EntityState.Modified;
            db.SaveChanges();

            return View(song);


        }

        // GET: Songs/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteConfirmed(short id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
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

        public MultiSelectList GetAllGenres(Song song)
        {
            //find the list of members
            var query = from m in db.Genres
                        select m;


            //convert to list and execute query
            List<Genre> allGenres = query.ToList();

            //create list of selected members
            List<Int32> SelectedGenres = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Genre g in song.SongGenres)
            {
                SelectedGenres.Add(g.GenreID);
            }

            //convert to multiselect
            MultiSelectList allGenresList = new MultiSelectList(allGenres, "GenreID", "GenreName", SelectedGenres);

            return allGenresList;
        }

        public MultiSelectList GetAllAlbums()
        {
            //find the list of members
            var query = from m in db.Albums
                        select m;

            //convert to list and execute query
            List<Album> allAlbums = query.ToList();

            //convert to multiselect
            MultiSelectList allAlbumsList = new MultiSelectList(allAlbums, "AlbumID", "AlbumName");

            return allAlbumsList;
        }

        public MultiSelectList GetAllAlbums(Song song)
        {
            //find the list of members
            var query = from m in db.Albums
                        select m;


            //convert to list and execute query
            List<Album> allAlbums = query.ToList();

            //create list of selected members
            List<Int32> SelectedAlbums = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Album g in song.SongAlbums)
            {
                SelectedAlbums.Add(g.AlbumID);
            }

            //convert to multiselect
            MultiSelectList allAlbumsList = new MultiSelectList(allAlbums, "AlbumID", "AlbumName", SelectedAlbums);

            return allAlbumsList;
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

        public MultiSelectList GetAllArtists(Song song)
        {
            //find the list of members
            var query = from m in db.Artists
                        select m;

            //convert to list and execute query
            List<Artist> allArtists = query.ToList();

            //create list of selected members
            List<Int32> SelectedArtists = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Artist g in song.SongArtists)
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
