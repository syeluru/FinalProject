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
            var shoppingCarts = db.ShoppingCarts.Include(s => s.Customer);
            return View(shoppingCarts.ToList());
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

        public ActionResult Checkout()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());

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

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

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
