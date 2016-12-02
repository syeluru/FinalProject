using System;
using System.Activities.Expressions;
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
    public class ArtistsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Artists
        public ActionResult Index()
        {
            return View(db.Artists.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            
            ViewBag.AllGenres = GetAllGenres();
            return View();

        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "ArtistID,ArtistName")] Artist artist, int[] SelectedGenres, string NewGenreName)
        {

            if (ModelState.IsValid)
            {
                //associate list of genres with artist
                if (SelectedGenres != null && SelectedGenres.Length > 0)
                {
                    foreach (int genreID in SelectedGenres)
                    {
                        Genre GenreToAdd = db.Genres.Find(genreID);
                        artist.ArtistGenres.Add(GenreToAdd);
                    }

                }

                //if there are genres to add, add them
                if (NewGenreName != null && NewGenreName != "")
                {
                    Genre NewGenre = new Genre();
                    NewGenre.GenreName = NewGenreName;
                    db.Genres.Add(NewGenre);
                    db.SaveChanges();


                    artist.ArtistGenres.Add(db.Genres.First(a => a.GenreName == NewGenreName));
                }

                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllGenres = GetAllGenres(artist);
            return View(artist);
        }

        // GET: Artists/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllGenres = GetAllGenres(artist);
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit([Bind(Include = "ArtistID,ArtistName")] Artist artist, int[] SelectedGenres, string NewGenreName)
        {
            if (ModelState.IsValid)
            {

                //Find associated artist
                Artist artistToChange = db.Artists.Find(artist.ArtistID);

                //remove any existing genres
                artistToChange.ArtistGenres.Clear();

                //if there are genres to add, add them
                if (SelectedGenres != null)
                {
                    foreach (int genreID in SelectedGenres)
                    {
                        Genre genreToAdd = db.Genres.Find(genreID);
                        artistToChange.ArtistGenres.Add(genreToAdd);
                    }
                }

                //if there are genres to add, add them
                if (NewGenreName != null && NewGenreName != "")
                {
                    Genre NewGenre = new Genre();
                    NewGenre.GenreName = NewGenreName;
                    db.Genres.Add(NewGenre);
                    db.SaveChanges();


                    artist.ArtistGenres.Add(db.Genres.First(a => a.GenreName == NewGenreName));
                }

                //update the rest of the fields
                artistToChange.ArtistName = artist.ArtistName;


                db.Entry(artistToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //re-populate genre list and add to viewbag
            ViewBag.AllGenres = GetAllGenres(artist);

            db.Entry(artist).State = EntityState.Modified;
            db.SaveChanges();

            return View(artist);
        }

        // GET: Artists/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteConfirmed(short id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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

        public MultiSelectList GetAllGenres(Artist @artist)
        {
            //find the list of members
            var query = from m in db.Genres
                        select m;


            //convert to list and execute query
            List<Genre> allGenres = query.ToList();

            //create list of selected members
            List<Int32> SelectedGenres = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Genre g in @artist.ArtistGenres)
            {
                SelectedGenres.Add(g.GenreID);
            }

            //convert to multiselect
            MultiSelectList allGenresList = new MultiSelectList(allGenres, "GenreID", "GenreName", SelectedGenres);

            // this line is important when they do it again
            //ViewBag.AllMembers = GetAllMembers(@event);
            return allGenresList;
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
