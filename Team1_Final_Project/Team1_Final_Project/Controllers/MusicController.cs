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

        // GET: Music
        public ActionResult SearchIndex(string SearchString)
        {

            // create the list of songs with no data
            List<Song> SelectedSongs = new List<Song>();

            // create the instance of the music viewmodel
            MusicViewModel SearchMusicViewModel = new MusicViewModel();

            //TODO: write the following methods
            //SearchMusicViewModel.Songs = GetSearchedSongs(SearchString);
            //SearchMusicViewModel.Albums = GetSearchedAlbums(SearchString);
            //SearchMusicViewModel.Artists = GetSearchedArtists(SearchString);



            return View(SearchMusicViewModel);
        }

        //public SelectList GetSearchedSongs(String SearchString)
        //{
        //    var query = from c in db.Songs
        //                orderby c.SongName
        //                select c;
        //}

    }
}