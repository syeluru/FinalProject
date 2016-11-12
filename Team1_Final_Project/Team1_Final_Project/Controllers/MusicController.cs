using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Controllers
{
    public class MusicController : Controller
    {
        // GET: Music
        public ActionResult Index(string SearchString)
        {

            // create the list of songs with no data
            List<Song> SelectedSongs = new List<Song>();




            return View();
        }
    }
}