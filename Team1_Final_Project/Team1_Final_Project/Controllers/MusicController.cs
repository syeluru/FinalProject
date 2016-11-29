﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Controllers
{
    public enum Operation
    {
        GreaterThan,
        LessThan,
        EqualTo
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }


    public class MusicController : Controller
    {
        // appdbcontext
        public static AppDbContext db = new AppDbContext();


        /*-----------------------------SEARCH------------------------------*/

        // GET: Basic Music Search
        public ActionResult BasicSearch(string SearchString)
        {

            // create the list of songs with no data
            List<Song> SelectedSongs = new List<Song>();

            // create the instance of the music viewmodel
            MusicViewModel SearchMusicViewModel = new MusicViewModel();

            //TODO: write the following methods
            SearchMusicViewModel.Songs = GetSearchedSongs(SearchString);
            SearchMusicViewModel.Albums = GetSearchedAlbums(SearchString);
            SearchMusicViewModel.Artists = GetSearchedArtists(SearchString);
            
            return View(SearchMusicViewModel);
        }

        /*
        // GET: Advanced Song Search
        public ActionResult SongAdvancedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View("SongAdvancedSearch");
        }

        // GET: Advanced Song Search
        public ActionResult SongAdvancedSearchResults(string SongSearchString, string ArtistSearchString, string AlbumSearchString, int[] SelectedGenres, decimal? SongRatingDec, Operation? SelectedSongRatingOperation, SortOrder? SelectedSortOrder)
        {
            // create the list of songs with no data
            List<Song> SelectedSongList = new List<Song>();

            var query = from c in db.Songs
                        select c;

            //code to find songs by name
            if (SongSearchString != null && SongSearchString != "")
            {
                query = query.Where(c => c.SongName == SongSearchString);
            }

            //code to find songs by genre
            if (SelectedGenres != null)
            {
                foreach (var item in SelectedGenres)
                {
                    query = query.Where(c => c.SongGenres.Any(g => g.GenreID == item));
                }
            }

            //code to find songs by artist
            if (ArtistSearchString != null && ArtistSearchString != "")
            {
                query = query.Where(c => c.SongArtists.Any(g => g.ArtistName == ArtistSearchString));
            }

            //code to find songs by album
            if (AlbumSearchString != null && AlbumSearchString != "")
            {
                query = query.Where(c => c.SongAlbums.Any(g => g.AlbumName == AlbumSearchString));
            }

            //code to find songs by rating

            if (SongRatingDec != null && SelectedSongRatingOperation != null)
            {
                //if the user entered a number <1 or >5
                if (SongRatingDec < 1 || SongRatingDec > 5)
                {
                    ViewBag.ErrorMessage = "Please specify a number between 1.0 and 5.0 inclusive.";
                    return View("SongAdvancedSearch");
                }

                Decimal AverageRatingsSearch;
                try
                {
                    AverageRatingsSearch = Convert.ToDecimal(SongRatingDec);
                    if (SelectedSongRatingOperation == Operation.LessThan)
                    {
                        query = query.Where(c => GetSongAverage(c.SongID, SongRatingDec) < AverageRatingsSearch);
                    }
                    else
                    {
                        query = query.Where(c => GetSongAverage(c.SongID, SongRatingDec) < AverageRatingsSearch);
                    }

                }
                catch  // will display when something is wrong
                {
                    //Add error message to viewbag
                    ViewBag.ErrorMessage = "Please specify a valid rating number";
                    return View("SongAdvancedSearch");
                }
            }

            SelectedSongList = query.ToList();
            ViewBag.TotalCount = db.Songs.Count();
            ViewBag.ResultsCount = SelectedSongList.Count();

            return View("SongSearchIndex", SelectedSongList);
        }

        // GET: Advanced Artist Search
        public ActionResult ArtistAdvancedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View("ArtistAdvancedSearch");
        }

        // GET: Advanced Artist Search
        public ActionResult ArtistAdvancedSearchResults(string ArtistSearchString, int[] SelectedGenres, decimal? ArtistRatingDec, Operation? SelectedArtistRatingOperation, SortOrder? SelectedSortOrder)
        {   
            // create the list of Artists with no data
            List<Artist> SelectedArtistList = new List<Artist>();

            var query = from c in db.Artists
                        select c;

            //code to find artists by name
            if (ArtistSearchString != null && ArtistSearchString != "")
            {
                query = query.Where(c => c.ArtistName == ArtistSearchString);
            }

            //code to find artists by genre
            if (SelectedGenres != null)
            {
                foreach( var item in SelectedGenres)
                {
                    query = query.Where(c => c.ArtistGenres.Any(g => g.GenreID == item));
                }
            }

            //code to find artists by rating
            if (ArtistRatingDec != null && SelectedArtistRatingOperation != null)
            {
                //if the user entered a number <1 or >5
                if (ArtistRatingDec < 1 || ArtistRatingDec > 5)
                {
                    ViewBag.ErrorMessage = "Please specify a number between 1.0 and 5.0 inclusive.";
                    return View("ArtistAdvancedSearch");
                }

                Decimal AverageRatingsSearch;
                try
                {
                    AverageRatingsSearch = Convert.ToDecimal(ArtistRatingDec);
                    if (SelectedArtistRatingOperation == Operation.LessThan)
                    {     
                         query = query.Where(c => GetArtistAverage(c.ArtistID, ArtistRatingDec) < AverageRatingsSearch);
                    }
                    else
                    {
                        query = query.Where(c => GetArtistAverage(c.ArtistID, ArtistRatingDec) > AverageRatingsSearch);
                    }
                }
                catch  // will display when something is wrong
                {
                    //Add error message to viewbag
                    ViewBag.ErrorMessage = "Please specify a valid rating number";
                    return View("ArtistAdvancedSearch");
                }
            }
            //-------------end of code to find artists by rating--------------------

            SelectedArtistList = query.ToList();
            ViewBag.TotalCount = db.Artists.Count();
            ViewBag.ResultsCount = SelectedArtistList.Count();

            return View("ArtistSearchIndex", SelectedArtistList);

        }

        // GET: Advanced Album Search
        public ActionResult AlbumAdvancedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View("AlbumAdvancedSearch");
        }

        // GET: Advanced Album Search
        public ActionResult AlbumAdvancedSearchResults(string AlbumSearchString, int[] SelectedGenres, string ArtistSearchString, decimal? AlbumRatingDec, Operation? SelectedAlbumRatingOperation, SortOrder? SelectedSortOrder)
        {
            // create the list of Albums with no data
            List<Album> SelectedAlbumList = new List<Album>();

            var query = from c in db.Albums
                        select c;

            //code to find albums by name
            if (AlbumSearchString != null && AlbumSearchString != "")
            {
                query = query.Where(c => c.AlbumName == AlbumSearchString);
            }

            //code to find albums by genre
            if (SelectedGenres != null)
            {
                foreach (var item in SelectedGenres)
                {
                    query = query.Where(c => c.AlbumGenres.Any(g => g.GenreID == item));
                }
            }

            //code to find albums by artist
            if (ArtistSearchString != null && ArtistSearchString != "")
            {
                query = query.Where(c => c.AlbumArtists.Any(g => g.ArtistName == ArtistSearchString));
            }

            //code to find albums by rating
            if (AlbumRatingDec != null && SelectedAlbumRatingOperation != null)
            {
                //if the user entered a number <1 or >5
                if (AlbumRatingDec < 1 || AlbumRatingDec > 5)
                {
                    ViewBag.ErrorMessage = "Please specify a number between 1.0 and 5.0 inclusive.";
                    return View("AlbumAdvancedSearch");
                }

                Decimal AverageRatingsSearch;
                try
                {
                    AverageRatingsSearch = Convert.ToDecimal(AlbumRatingDec);
                    if (SelectedAlbumRatingOperation == Operation.LessThan)
                    {
                        query = query.Where(c => GetAlbumAverage(c.AlbumID, AlbumRatingDec) < AverageRatingsSearch);
                    }
                    else
                    {
                        query = query.Where(c => GetAlbumAverage(c.AlbumID, AlbumRatingDec) < AverageRatingsSearch);
                    }

                }
                catch  // will display when something is wrong
                {
                    //Add error message to viewbag
                    ViewBag.ErrorMessage = "Please specify a valid rating number";
                    return View("AlbumAdvancedSearch");
                }
            }

            SelectedAlbumList = query.ToList();
            ViewBag.TotalCount = db.Albums.Count();
            ViewBag.ResultsCount = SelectedAlbumList.Count();

            return View("AlbumSearchIndex", SelectedAlbumList);
        }
        */
        /*-----------------------other stuff---------------------------------*/


        public decimal GetSongAverage(int SongID, decimal? SongRatingDec)
        {
            Song FoundSong = db.Songs.Find(SongID);

            decimal countVariable = 0;
            decimal count = 0;

            foreach (var rating in FoundSong.SongRatings)
            {

                countVariable += rating.RatingNumber;
                count += 1;
            }

            decimal RatingAverage = countVariable / count;
            
            return RatingAverage;

        }

        public decimal GetAlbumAverage(int AlbumID, decimal? AlbumRatingDec)
        {
            Album FoundAlbum = db.Albums.Find(AlbumID);

            decimal countVariable = 0;
            decimal count = 0;

            foreach (var rating in FoundAlbum.AlbumRatings)
            {

                countVariable += rating.RatingNumber;
                count += 1;
            }

            decimal RatingAverage = countVariable / count;
            return RatingAverage;

        }

        public decimal GetArtistAverage(int ArtistID, decimal? ArtistRatingDec)
        {
            Artist FoundArtist = db.Artists.Find(ArtistID);

            decimal countVariable = 0;
            decimal count = 0;

            foreach (var rating in FoundArtist.ArtistRatings)
            {

                countVariable += rating.RatingNumber;
                count += 1;
            }

            decimal RatingAverage = countVariable / count;
            return RatingAverage;

        }

        public List<Song> GetSearchedSongs(String SearchString)
        {
            var query = from c in db.Songs
                        select c;

            // search the SearchString for nulls
            if (SearchString == null || SearchString == "")
            {
                query = query;
            } else
            {
                // search for a song by name, a song by its artists' names, or by its albums' names
                query = query.Where(c => c.SongName.Contains(SearchString) || c.SongArtists.Any(art => art.ArtistName.Contains(SearchString)) || c.SongAlbums.Any(alb => alb.AlbumName.Contains(SearchString)));
                //c.SongArtists.Contains(SearchString) || c.SongGenres.Contains(SearchString)
            }

            List<Song> SelectedSongs = query.ToList();
            return SelectedSongs;
        }

        public List<Artist> GetSearchedArtists(String SearchString)
        {
            var query = from c in db.Artists   
                        select c;

            // search the SearchString for nulls
            if (SearchString == null || SearchString == "")
            {
                query = query;
            }
            else
            {
                query = query.Where(c => c.ArtistName.Contains(SearchString));
                //c.SongArtists.Contains(SearchString) || c.SongGenres.Contains(SearchString)
            }

            List<Artist> SelectedArtists = query.ToList();
            return SelectedArtists;
        }

        public List<Album> GetSearchedAlbums(String SearchString)
        {
            var query = from c in db.Albums
                        select c;

            // search the SearchString for nulls
            if (SearchString == null || SearchString == "")
            {
                query = query;
            }
            else
            {
                query = query.Where(c => c.AlbumName.Contains(SearchString) || c.AlbumSongs.Any(song => song.SongName.Contains(SearchString)) || c.AlbumArtists.Any(art => art.ArtistName.Contains(SearchString)));
                //c.SongArtists.Contains(SearchString) || c.SongGenres.Contains(SearchString)
            }

            List<Album> SelectedAlbums = query.ToList();
            return SelectedAlbums;
        }

        public MultiSelectList GetAllGenres()
        {
            var query = from c in db.Genres
                        orderby c.GenreName
                        select c;

            List<Genre> GenreList = query.Distinct().ToList();

            //Add in choice for not selecting a frequency
            Genre NoChoice = new Genre() { GenreID = 0, GenreName = "All Genres" };
            GenreList.Add(NoChoice);
            MultiSelectList SelectedGenreList = new MultiSelectList(GenreList.OrderBy(f => f.GenreName), "GenreID", "GenreName");
            return SelectedGenreList;
        }

        ///*---------------------------SONGS--------------------------------*/
        //        // GET: Music/SongDetails/5
        //        public ActionResult SongDetails(int? id)
        //        {

        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Song song = db.Songs.Find(id);
        //            if (song == null)
        //            {
        //                return HttpNotFound();
        //            }


        //            return View(song);
        //        }

        //        // GET
        //        public ActionResult CreateSong()
        //        {
        //            return View();
        //        }

        //        // POST
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult CreateSong([Bind(Include = "SongID,SongName")] Song song)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Songs.Add(song);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //            return View(song);
        //        }






        ///*-----------------------------ARTISTS------------------------------*/
        //        // GET: Music/ArtistDetails/5
        //        public ActionResult ArtistDetails(int? id)
        //        {

        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Artist artist = db.Artists.Find(id);
        //            if (artist == null)
        //            {
        //                return HttpNotFound();
        //            }


        //            return View(artist);
        //        }

        //        // GET
        //        public ActionResult CreateArtist()
        //        {
        //            return View();
        //        }

        //        // POST
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult CreateArtist([Bind(Include = "ArtistID,ArtistName")] Artist artist)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Artists.Add(artist);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //            return View(artist);
        //        }

        //        /*---------------------------ALBUM-------------------------------*/

        //        // GET: Music/AlbumDetails/5
        //        public ActionResult AlbumDetails(int? id)
        //        {

        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Album album = db.Albums.Find(id);
        //            if (album == null)
        //            {
        //                return HttpNotFound();
        //            }


        //            return View(album);
        //        }

        //        // GET 
        //        public ActionResult CreateAlbum()
        //        {
        //            return View();
        //        }

        //        // POST
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult CreateAlbum([Bind(Include = "AlbumID,AlbumName")] Album album)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Albums.Add(album);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //            return View(album);
        //        }

        //        // GET: Music/EditAlbums/5
        //        public ActionResult EditAlbum(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Album album = db.Albums.Find(id);
        //            if (album == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(album);
        //        }




    }
}