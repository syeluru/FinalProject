using System;
using System.Collections.Generic;
using System.Linq;
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

    public class MusicController : Controller
    {
        // appdbcontext
        public static AppDbContext db = new AppDbContext();

        // GET: Music Details
        public ActionResult SongDetail(Int32 SongID)
        {
            return View(SongID);
        }

        // GET: Music Search
        public ActionResult SearchIndex(string SearchString)
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
                query = query.Where(c => c.SongName.Contains(SearchString));
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
                query = query.Where(c => c.AlbumName.Contains(SearchString));
                //c.SongArtists.Contains(SearchString) || c.SongGenres.Contains(SearchString)
            }

            List<Album> SelectedAlbums = query.ToList();
            return SelectedAlbums;
        }


    }
}