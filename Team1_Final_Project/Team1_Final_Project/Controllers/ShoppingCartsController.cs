using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Purchases;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            return View(userLoggedIn.ShoppingCart);
        }

        public ActionResult ShoppingCartIndex(String ErrorMessage)
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            return View("Index", userLoggedIn.ShoppingCart);

        }

        // GET: ShoppingCarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            ViewBag.ShoppingCartID = new SelectList(db.Users, "Id", "FName");
            return View();
        }

        // Add a song to the shopping cart
        public ActionResult AddSong(int SongID)
        {

            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            userLoggedIn.ShoppingCart.Songs.Add(db.Songs.Find(SongID));
            RecalculateTotal();


            // return them to song index after they're done browsing
            return RedirectToAction("BasicSearch","Music");
        }

        public ActionResult AddAlbum(int AlbumID)
        {

            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            userLoggedIn.ShoppingCart.Albums.Add(db.Albums.Find(AlbumID));
            RecalculateTotal();


            // return them to song index after they're done browsing
            return RedirectToAction("BasicSearch", "Music");
        }

        public ActionResult CheckoutPage()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (userLoggedIn.ShoppingCart.Albums.Count == 0 && userLoggedIn.ShoppingCart.Songs.Count == 0)
            {
                return RedirectToAction("ShoppingCartIndex",new { ErrorMessage = "You need at least one item in your shopping cart before you can check out! I hear Taylor Swift has been quite the hit lately." });
            }
            return View(userLoggedIn);
        }

        public ActionResult Checkout()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            // check to see that there are no duplicates first of all
            if (DuplicatesExist())
            {
                return RedirectToAction("ShoppingCartIndex", new { ErrorMessage = "Looks like you have some duplicates in your shopping cart. Check back through your shopping cart!" });
            }
            else
            {
                // throw in total price, songs, and albums into a new order
                Order NewOrder = new Order();

                // add all the songs from shopping cart to this order
                foreach (var song in userLoggedIn.ShoppingCart.Songs)
                {
                    Song songToAdd = db.Songs.Find(song.SongID);
                    SongOrderBridge songBridgeToAdd = new SongOrderBridge();
                    songBridgeToAdd.Song = songToAdd;
                    NewOrder.SongsInOrder.Add(songBridgeToAdd);

                }

                // add all the albums from shopping cart to this order
                foreach (var album in userLoggedIn.ShoppingCart.Albums)
                {
                    Album albumToAdd = db.Albums.Find(album.AlbumID);
                    AlbumOrderBridge albumBridgeToAdd = new AlbumOrderBridge();
                    albumBridgeToAdd.Album = albumToAdd;
                    NewOrder.AlbumsInOrder.Add(albumBridgeToAdd);

                }

                // add the total price to the order
                NewOrder.TotalPrice = userLoggedIn.ShoppingCart.TotalPrice;

                // clear out the shopping cart
                userLoggedIn.ShoppingCart.Songs.Clear();
                userLoggedIn.ShoppingCart.Albums.Clear();

                // add the order to the database
                db.Orders.Add(NewOrder);
                db.SaveChanges();

                // return the customer to their customer dashboard so they can see the songs/albums they just purchased
                return RedirectToAction("CustomerDashboard", "Account", new { SuccessMessage = "Congratulations on your purchase!" });

            }




        }

        public void RecalculateTotal()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            decimal TotalValueOfSongs = 0.0m;
            foreach (var song in userLoggedIn.ShoppingCart.Songs)
            {
                TotalValueOfSongs += (song.SongPrice - song.SongDiscount); 
            }

            decimal TotalValueOfAlbums = 0.0m;
            foreach (var album in userLoggedIn.ShoppingCart.Albums)
            {
                TotalValueOfSongs += (album.AlbumPrice - album.AlbumDiscount);
            }

            userLoggedIn.ShoppingCart.TotalPrice = (TotalValueOfAlbums + TotalValueOfSongs);
        }

        public bool DuplicatesExist()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            // get a list of all the songs in a given shopping cart
            List<Song> SongsList = new List<Song>();
            foreach (var song in userLoggedIn.Songs)
            {
                SongsList.Add(db.Songs.Find(song.SongID));
            }

            foreach (var album in userLoggedIn.Albums)
            {
                foreach (var song in album.AlbumSongs)
                {
                    SongsList.Add(db.Songs.Find(song.SongID));
                }
            }

            //now SongsList has all the songs in the shopping cart

            // return true if duplicates
            var duplicateSongs = SongsList.GroupBy(a => new { a.SongName, a.SongArtists }).Where(g => g.Count() > 1);
            return (duplicateSongs.Count() > 0);            
            




        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        /*
        public ActionResult Create([Bind(Include = "ShoppingCartID,TotalPrice")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCarts.Add(shoppingCart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShoppingCartID = new SelectList(db.AppUsers, "Id", "FName", shoppingCart.ShoppingCartID);
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShoppingCartID = new SelectList(db.AppUsers, "Id", "FName", shoppingCart.ShoppingCartID);
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoppingCartID,TotalPrice")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShoppingCartID = new SelectList(db.AppUsers, "Id", "FName", shoppingCart.ShoppingCartID);
            return View(shoppingCart);
        }

        */


        // GET: ShoppingCarts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            db.ShoppingCarts.Remove(shoppingCart);
            db.SaveChanges();
            return RedirectToAction("Index");
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
