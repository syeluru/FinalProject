using System;
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
        AscendingName,
        DescendingName,
        AscendingArtist,
        DescendingArtist,
        AscendingRating,
        DescendingRating,
        AscendingGenre,
        DescendingGenre
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

            ViewBag.TotalCount = db.Songs.Count() + db.Artists.Count() + db.Albums.Count();
            ViewBag.ResultsCount = SearchMusicViewModel.Songs.Count() + SearchMusicViewModel.Albums.Count() + SearchMusicViewModel.Artists.Count();

            return View(SearchMusicViewModel);
        }

        // GET: Advanced Search Index
        public ActionResult AdvancedSearchIndex()
        {
            return View();
        }

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

            foreach (var item in db.Songs)
            {
                item.SongAverage = GetSongAverage(item.SongID);
            }

            var query = from c in db.Songs
                        select c;

            //code to find songs by name
            if (SongSearchString != null && SongSearchString != "")
            {
                query = query.Where(c => c.SongName.Contains(SongSearchString));
            }

            //code to find songs by artist
            if (ArtistSearchString != null && ArtistSearchString != "")
            {
                query = query.Where(c => c.SongArtists.Any(g => g.ArtistName.Contains(ArtistSearchString)));
            }

            //code to find songs by album
            if (AlbumSearchString != null && AlbumSearchString != "")
            {
                query = query.Where(c => c.SongAlbums.Any(g => g.AlbumName.Contains(AlbumSearchString)));
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
                        query = query.Where(c => c.SongAverage < AverageRatingsSearch);
                    }
                    else
                    {
                        query = query.Where(c => c.SongAverage < AverageRatingsSearch);
                    }

                }
                catch  // will display when something is wrong
                {
                    //Add error message to viewbag
                    ViewBag.ErrorMessage = "Please specify a valid rating number";
                    return View("SongAdvancedSearch");
                }
            }

            //code to find songss by genre
            if (SelectedGenres != null)
            {
                List<Song> SongGenreList = new List<Song>();
                foreach (var item in SelectedGenres)
                {
                    foreach (var item2 in query.Where(c => c.SongGenres.Any(g => g.GenreID == item)).ToList())
                    {
                        SongGenreList.Add(item2);
                    }
                }

                //select disctinct item in list
                var distinctNames = (from d in SongGenreList select d).Distinct();

                //if they selected a sort order
                if (SelectedSortOrder != null)
                {
                    if (SelectedSortOrder == SortOrder.AscendingName)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.SongName);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingName)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.SongName);
                    }
                    else if (SelectedSortOrder == SortOrder.AscendingArtist)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.SongArtists.First().ArtistName);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingArtist)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.SongArtists.First().ArtistName);
                    }
                    else if (SelectedSortOrder == SortOrder.AscendingRating)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.SongAverage);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingRating)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.SongAverage);
                    }
                }

                ViewBag.TotalCount = db.Songs.Count();
                ViewBag.ResultsCount = distinctNames.Count();

                return View("SongSearchIndex", distinctNames);
            }

            //if they selected a sort order
            if (SelectedSortOrder != null)
            {
                if (SelectedSortOrder == SortOrder.AscendingName)
                {
                    query = query.OrderBy(d => d.SongName);
                }
                else if (SelectedSortOrder == SortOrder.DescendingName)
                {
                    query = query.OrderByDescending(d => d.SongName);
                }
                else if (SelectedSortOrder == SortOrder.AscendingArtist)
                {
                    query = query.OrderBy(d => d.SongArtists.First().ArtistName);
                }
                else if (SelectedSortOrder == SortOrder.DescendingArtist)
                {
                    query = query.OrderByDescending(d => d.SongArtists.First().ArtistName);
                }
                else if (SelectedSortOrder == SortOrder.AscendingRating)
                {
                    query = query.OrderBy(d => d.SongAverage);
                }
                else if (SelectedSortOrder == SortOrder.DescendingRating)
                {
                    query = query.OrderByDescending(d => d.SongAverage);
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

            foreach (var item in db.Artists)
            {
                item.ArtistAverage = GetArtistAverage(item.ArtistID);
            }


            var query = from c in db.Artists
                        select c;

            //code to find artists by name
            if (ArtistSearchString != null && ArtistSearchString != "")
            {
                query = query.Where(c => c.ArtistName.Contains(ArtistSearchString));
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
                         query = query.Where(c => c.ArtistAverage < AverageRatingsSearch);
                    }
                    else
                    {
                        query = query.Where(c => c.ArtistAverage > AverageRatingsSearch);
                    }
                }
                catch  // will display when something is wrong
                {
                    //Add error message to viewbag
                    ViewBag.ErrorMessage = "Please specify a valid rating number";
                    return View("ArtistAdvancedSearch");
                }
            }

            //code to find artists by genre
            if (SelectedGenres != null)
            {
                List<Artist> ArtistGenreList = new List<Artist>();
                foreach (var item in SelectedGenres)
                {
                    foreach (var item2 in query.Where(c => c.ArtistGenres.Any(g => g.GenreID == item)).ToList())
                    {
                        ArtistGenreList.Add(item2);
                    }
                }

                //select disctinct item in list
                var distinctNames = (from d in ArtistGenreList select d).Distinct();

                //if they selected a sort order
                if (SelectedSortOrder != null)
                {
                    if (SelectedSortOrder == SortOrder.AscendingName)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.ArtistName);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingName)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.ArtistName);
                    }
                    else if (SelectedSortOrder == SortOrder.AscendingRating)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.ArtistAverage);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingRating)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.ArtistAverage);
                    }
                }

                ViewBag.TotalCount = db.Artists.Count();
                ViewBag.ResultsCount = distinctNames.Count();

                return View("ArtistSearchIndex", distinctNames);
            }
            //if they selected a sort order
            if (SelectedSortOrder != null)
            {
                if (SelectedSortOrder == SortOrder.AscendingName)
                {
                    query = query.OrderBy(d => d.ArtistName);
                }
                else if (SelectedSortOrder == SortOrder.DescendingName)
                {
                    query = query.OrderByDescending(d => d.ArtistName);
                }
                else if (SelectedSortOrder == SortOrder.AscendingRating)
                {
                    query = query.OrderBy(d => d.ArtistAverage);
                }
                else if (SelectedSortOrder == SortOrder.DescendingRating)
                {
                    query = query.OrderByDescending(d => d.ArtistAverage);
                }
            }

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

            foreach (var item in db.Albums)
            {
                item.AlbumAverage = GetAlbumAverage(item.AlbumID);
            }

            var query = from c in db.Albums
                        select c;

            //code to find albums by name
            if (AlbumSearchString != null && AlbumSearchString != "")
            {
                query = query.Where(c => c.AlbumName.Contains(AlbumSearchString));
            }

            //code to find albums by artist
            if (ArtistSearchString != null && ArtistSearchString != "")
            {
                query = query.Where(c => c.AlbumArtists.Any(g => g.ArtistName.Contains(ArtistSearchString)));
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
                        query = query.Where(c => c.AlbumAverage < AverageRatingsSearch);
                    }
                    else
                    {
                        query = query.Where(c => c.AlbumAverage < AverageRatingsSearch);
                    }

                }
                catch  // will display when something is wrong
                {
                    //Add error message to viewbag
                    ViewBag.ErrorMessage = "Please specify a valid rating number";
                    return View("AlbumAdvancedSearch");
                }
            }

            //code to find albums by genre
            if (SelectedGenres != null)
            {
                List<Album> AlbumGenreList = new List<Album>();
                foreach (var item in SelectedGenres)
                {
                    foreach (var item2 in query.Where(c => c.AlbumGenres.Any(g => g.GenreID == item)).ToList())
                    {
                        AlbumGenreList.Add(item2);
                    }
                }

                //select disctinct item in list
                var distinctNames = (from d in AlbumGenreList select d).Distinct();

                //if they selected a sort order
                if (SelectedSortOrder != null)
                {
                    if (SelectedSortOrder == SortOrder.AscendingName)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.AlbumName);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingName)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.AlbumName);
                    }
                    else if (SelectedSortOrder == SortOrder.AscendingArtist)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.AlbumArtists.First().ArtistName);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingArtist)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.AlbumArtists.First().ArtistName);
                    }
                    else if (SelectedSortOrder == SortOrder.AscendingRating)
                    {
                        distinctNames = distinctNames.OrderBy(d => d.AlbumAverage);
                    }
                    else if (SelectedSortOrder == SortOrder.DescendingRating)
                    {
                        distinctNames = distinctNames.OrderByDescending(d => d.AlbumAverage);
                    }
                }

                //ViewBag.ArtistRatingAverage = GetArtistAverage(AlbumArtists);

                ViewBag.TotalCount = db.Albums.Count();
                ViewBag.ResultsCount = distinctNames.Count();

                return View("AlbumSearchIndex", distinctNames);
            }

            //if they selected a sort order
            if (SelectedSortOrder != null)
            {
                if (SelectedSortOrder == SortOrder.AscendingName)
                {
                    query = query.OrderBy(d => d.AlbumName);
                }
                else if (SelectedSortOrder == SortOrder.DescendingName)
                {
                    query = query.OrderByDescending(d => d.AlbumName);
                }
                else if (SelectedSortOrder == SortOrder.AscendingArtist)
                {
                    query = query.OrderBy(d => d.AlbumArtists.First().ArtistName);
                }
                else if (SelectedSortOrder == SortOrder.DescendingArtist)
                {
                    query = query.OrderByDescending(d => d.AlbumArtists.First().ArtistName);
                }
                else if (SelectedSortOrder == SortOrder.AscendingRating)
                {
                    query = query.OrderBy(d => d.AlbumAverage);
                }
                else if (SelectedSortOrder == SortOrder.DescendingRating)
                {
                    query = query.OrderByDescending(d => d.AlbumAverage);
                }
            }

            SelectedAlbumList = query.ToList();
            ViewBag.TotalCount = db.Albums.Count();
            ViewBag.ResultsCount = SelectedAlbumList.Count();

            return View("AlbumSearchIndex", SelectedAlbumList);
        }

        /*-----------------------other stuff---------------------------------*/


        public decimal GetSongAverage(int SongID)
        {
            Song FoundSong = db.Songs.Find(SongID);

            decimal countVariable = 0;
            decimal count = 0;
            decimal RatingAverage = 0;

            foreach (var rating in FoundSong.SongRatings)
            {

                countVariable += rating.RatingNumber;
                count += 1;
            }

            if (count != 0)
            {
                RatingAverage = countVariable / count;
            }

            //ViewBag.SongRatingAverage = RatingAverage;
            //FoundSong.SongAverage = RatingAverage;

            return RatingAverage;

        }

        public decimal GetAlbumAverage(int AlbumID)
        {
            Album FoundAlbum = db.Albums.Find(AlbumID);

            decimal countVariable = 0;
            decimal count = 0;
            decimal RatingAverage = 0;

            foreach (var rating in FoundAlbum.AlbumRatings)
            {

                countVariable += rating.RatingNumber;
                count += 1;
            }

            if (count != 0)
            {
                RatingAverage = countVariable / count;
            }

            return RatingAverage;

        }

        public decimal GetArtistAverage(int ArtistID)
        {
            Artist FoundArtist = db.Artists.Find(ArtistID);

            decimal countVariable = 0;
            decimal count = 0;
            decimal RatingAverage = 0;

            foreach (var rating in FoundArtist.ArtistRatings)
            {

                countVariable += rating.RatingNumber;
                count += 1;
            }

            if (count != 0)
            {
                RatingAverage = countVariable / count;
            }

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
    }
}