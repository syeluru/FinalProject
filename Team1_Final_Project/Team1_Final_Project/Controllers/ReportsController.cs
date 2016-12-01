using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;

namespace Team1_Final_Project.Controllers
{
    public class ReportsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        /*
        // GET: Reports/Create
        public ActionResult SongsReport()
        {

            return View();

        }

        // POST: Reports/Create
        [HttpPost]
        public ActionResult SongsReport()
        {
   
            foreach (var item in db.Songs)
            {

                var query = from songs in item.SongOrderBridges
                            select songs;

                query = item.SongOrderBridges.Count()

                    Songs.Song

            }

            int count = (from x in  select x).Count();

            query = 

            return View();
            }


    */

        
    }
}