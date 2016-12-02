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
        [Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            ViewBag.SongTotal = CalculateSongTotal();
            ViewBag.AlbumTotal = CalculateAlbumTotal();
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult ShoppingCartIndex(String ErrorMessage)
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            ViewBag.ErrorMessage = ErrorMessage;
            return View("Index");

        }

        //// GET: ShoppingCarts/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
        //    if (shoppingCart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(shoppingCart);
        //}

        // GET: ShoppingCarts/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ShoppingCartID = new SelectList(db.Users, "Id", "FName");
        //    return View();
        //}

        // Add a song to the shopping cart
        [Authorize(Roles = "Customer")]
        public ActionResult AddSong(int SongID)
        {

            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            SongInShoppingCart newsong = new SongInShoppingCart();
            newsong.Song = db.Songs.Find(SongID);
            newsong.Customer = userLoggedIn;

            userLoggedIn.SongsInShoppingCart.Add(newsong);
            db.SaveChanges();





            // return them to song index after they're done browsing
            return RedirectToAction("ShoppingCartIndex");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult AddAlbum(int AlbumID)
        {

            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            AlbumInShoppingCart newalbum = new AlbumInShoppingCart();
            newalbum.Album = db.Albums.Find(AlbumID);
            newalbum.Customer = userLoggedIn;

            userLoggedIn.AlbumsInShoppingCart.Add(newalbum);
            db.SaveChanges();
            

            // return them to song index after they're done browsing
            return RedirectToAction("ShoppingCartIndex");
        }

        //still need to do
        [Authorize(Roles = "Customer")]
        public ActionResult CheckoutPage()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            if (!userLoggedIn.IsAccountEnabled)
            {
                return RedirectToAction("ShoppingCartIndex", new { ErrorMessage = "Your account is disabled. Please contact a manager to re-enable your account." });

            }
            else if (userLoggedIn.AlbumsInShoppingCart.Count() == 0 && userLoggedIn.SongsInShoppingCart.Count() == 0)
            {
                return RedirectToAction("ShoppingCartIndex", new { ErrorMessage = "You need at least one item in your shopping cart before you can check out! I hear Taylor Swift has been quite the hit lately." });
            }

            if (userLoggedIn.CreditCards == null)
            {
                userLoggedIn.CreditCards = new List<CreditCard>();
                db.SaveChanges();
            }

            ViewBag.Subtotal = (CalculateSongTotal() + CalculateAlbumTotal()) * 1.0825m;
            return View(userLoggedIn);
        }

        //still need to do
        [Authorize(Roles = "Customer")]
        public ActionResult Checkout(int? SelectedCreditCardID)
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            // check to see if they have a credit card that they're purchasing with
            if (SelectedCreditCardID == null)
            {
                return RedirectToAction("CheckoutPage", new { ErrorMessage = "Looks like you forgot to select a credit card. If you don't have a credit card, make sure to add one!" });

            }

            // check to see that there are no duplicates first of all
            if (DuplicatesExist())
            {
                return RedirectToAction("ShoppingCartIndex", new { ErrorMessage = "Looks like you have some duplicates in your shopping cart. Check back through your shopping cart!" });
            }
            else
            {

                // throw in total price, songs, and albums into a new order
                Order NewOrder = new Order();
                NewOrder.Customer = userLoggedIn;

                // add all the songs from shopping cart to this order
                foreach (var song in userLoggedIn.SongsInShoppingCart)
                {
                    Song songToAdd = db.Songs.Find(song.Song.SongID);
                    SongOrderBridge songBridgeToAdd = new SongOrderBridge();
                    decimal SongPricePostDiscounts = 0.0m;

                    // calculate total discounts
                    foreach (Discount discount in songToAdd.SongDiscounts)
                    {
                        if (discount.IsActiveDiscount)
                        {
                            SongPricePostDiscounts = songToAdd.SongPrice * (1 - discount.DiscountPercentage);

                        }

                    }

                    //properties of songbridgetoadd
                    songBridgeToAdd.PriceAtPointOfPurchase = SongPricePostDiscounts * 1.0825m;
                    songBridgeToAdd.SongInOrder = songToAdd;

                    // add to the order
                    NewOrder.SongsInOrder.Add(songBridgeToAdd);

                    // add all the songs to the customer's songs list
                    userLoggedIn.Songs.Add(songToAdd);
                    db.SaveChanges();

                }

                // add all the albums from shopping cart to this order
                foreach (var album in userLoggedIn.AlbumsInShoppingCart)
                {
                    Album albumToAdd = db.Albums.Find(album.Album.AlbumID);
                    AlbumOrderBridge albumBridgeToAdd = new AlbumOrderBridge();
                    decimal AlbumPricePostDiscounts = 0.0m;
                    foreach (Discount discount in albumToAdd.AlbumDiscounts)
                    {
                        if (discount.IsActiveDiscount)
                        {
                            AlbumPricePostDiscounts = albumToAdd.AlbumPrice * (1 - discount.DiscountPercentage);

                        }

                    }

                    //properties of songbridgetoadd
                    albumBridgeToAdd.PriceAtPointOfPurchase = AlbumPricePostDiscounts * 1.0825m;
                    albumBridgeToAdd.AlbumInOrder = albumToAdd;

                    // add to the order
                    NewOrder.AlbumsInOrder.Add(albumBridgeToAdd);

                    // add all the albums to the customer's albums list
                    userLoggedIn.Albums.Add(albumToAdd);


                }

                // add the total price to the order
                NewOrder.TotalPrice = (CalculateSongTotal() + CalculateAlbumTotal()) * 1.0825m;

                // add the credit card to the order
                NewOrder.CreditCardUsed = db.CreditCards.Find(SelectedCreditCardID);

                //isgift?
                NewOrder.IsGift = false;
                NewOrder.RecipientID = userLoggedIn.Id;

                // clear out the shopping cart
                userLoggedIn.SongsInShoppingCart.Clear();
                userLoggedIn.AlbumsInShoppingCart.Clear();

                // add the order to the database
                db.Orders.Add(NewOrder);
                db.SaveChanges();

                // send a new email

                // take the customer to the confirmation pageso they can see the songs/albums they just purchased
                return RedirectToAction("CheckoutConfirmationPage", "ShoppingCarts", new { RecipientID = userLoggedIn.Id, PlacedOrderID = NewOrder.OrderID });
            }

            
        }

        [Authorize(Roles = "Customer")]
        public ActionResult GiftCheckoutPage(string FriendEmail)
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());

            if (!userLoggedIn.IsAccountEnabled)
            {
                return RedirectToAction("ShoppingCartIndex", new { ErrorMessage = "Your account is disabled. Please contact a manager to re-enable your account." });
            }

            else if (db.Users.Any(c => c.Email == FriendEmail))
            {
                if (userLoggedIn.CreditCards == null)
                {
                    userLoggedIn.CreditCards = new List<CreditCard>();
                    db.SaveChanges();
                }
                ViewBag.Recipient = FriendEmail;
                ViewBag.Subtotal = (CalculateSongTotal() + CalculateAlbumTotal()) * 1.0825m;
                return View(userLoggedIn);
            } else 
            {
                return RedirectToAction("ShoppingCartIndex", new { ErrorMessage = "Sorry, doesn't look like that person is a customer yet. Check the email address on that!" });
            }

        }

        [Authorize(Roles = "Customer")]
        public ActionResult GiftCheckout(string FriendEmail, int? SelectedCreditCardID)
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            AppUser friend = db.Users.First(a => a.Email == FriendEmail);

            if (SelectedCreditCardID == null)
            {
                return RedirectToAction("CheckoutPage", new { ErrorMessage = "Looks like you forgot to select a credit card. If you don't have a credit card, make sure to add one!" });

            }

            if (DuplicatesExist())
            {
                return RedirectToAction("ShoppingCartIndex", new { ErrorMessage = "Looks like you have some duplicates in your shopping cart. Check back through your shopping cart!" });
            }
            else
            {

                // throw in total price, songs, and albums into a new order
                Order NewOrder = new Order();
                NewOrder.Customer = userLoggedIn;
                
                // add all the songs from shopping cart to this order
                foreach (var song in userLoggedIn.SongsInShoppingCart)
                {
                    Song songToAdd = db.Songs.Find(song.Song.SongID);
                    SongOrderBridge songBridgeToAdd = new SongOrderBridge();

                    decimal SongPricePostDiscounts = 0.0m;


                    // calculate total discounts
                    foreach (Discount discount in songToAdd.SongDiscounts)
                    {
                        if (discount.IsActiveDiscount)
                        {
                            SongPricePostDiscounts = songToAdd.SongPrice * (1 - discount.DiscountPercentage);

                        }

                    }

                    //properties of songbridgetoadd
                    songBridgeToAdd.PriceAtPointOfPurchase = SongPricePostDiscounts * 1.0825m;
                    songBridgeToAdd.SongInOrder = songToAdd;

                    // add to order
                    NewOrder.SongsInOrder.Add(songBridgeToAdd);

                    // add all the songs to the recipient's songs list
                    friend.Songs.Add(songToAdd);

                }

                // add all the albums from shopping cart to this order
                foreach (var album in userLoggedIn.AlbumsInShoppingCart)
                {
                    Album albumToAdd = db.Albums.Find(album.Album.AlbumID);
                    AlbumOrderBridge albumBridgeToAdd = new AlbumOrderBridge();

                    decimal AlbumPricePostDiscounts = 0.0m;

                    foreach (Discount discount in albumToAdd.AlbumDiscounts)
                    {
                        if (discount.IsActiveDiscount)
                        {
                            AlbumPricePostDiscounts = albumToAdd.AlbumPrice * (1 - discount.DiscountPercentage);

                        }

                    }

                    //properties of songbridgetoadd
                    albumBridgeToAdd.PriceAtPointOfPurchase = AlbumPricePostDiscounts * 1.0825m;
                    albumBridgeToAdd.AlbumInOrder = albumToAdd;

                    // add to the order
                    NewOrder.AlbumsInOrder.Add(albumBridgeToAdd);

                    // add all the albums to the recipient's albums list
                    friend.Albums.Add(albumToAdd);


                }

                // add the total price to the order
                NewOrder.TotalPrice = (CalculateSongTotal() + CalculateAlbumTotal()) * 1.0825m;

                // add the credit card used
                NewOrder.CreditCardUsed = db.CreditCards.Find(SelectedCreditCardID);

                // is gift?
                NewOrder.IsGift = true;
                NewOrder.RecipientID = friend.Id;

                // clear out the shopping cart
                userLoggedIn.SongsInShoppingCart.Clear();
                userLoggedIn.AlbumsInShoppingCart.Clear();

                // add the order to the database
                db.Orders.Add(NewOrder);
                db.SaveChanges();

                // send a new email to the recipient and the user who just placed the order

                // take the customer to the order confirmation page so they can see the songs/albums they just purchased
                return RedirectToAction("CheckoutConfirmationPage", "ShoppingCarts", new { RecipientID = friend.Id, PlacedOrderID = NewOrder.OrderID });
            }


        }

        [Authorize(Roles = "Customer")]
        public ActionResult CheckoutConfirmationPage(string RecipientID, int PlacedOrderID)
        {
            ViewBag.RecipientID = RecipientID;
            ViewBag.PlacedOrderID = PlacedOrderID;
            return View();
        }

        [Authorize(Roles = "Customer")]
        public decimal CalculateSongTotal()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            decimal TotalValueOfSongs = 0.0m;
           
            foreach (SongInShoppingCart scsong in userLoggedIn.SongsInShoppingCart)
            {

                decimal SongPricePostDiscounts = 0.0m;
                // calculate total discounts
                foreach (Discount discount in scsong.Song.SongDiscounts)
                {
                    if (discount.IsActiveDiscount)
                    {
                        SongPricePostDiscounts = scsong.Song.SongPrice * (1 - discount.DiscountPercentage);

                    }

                }

                TotalValueOfSongs += SongPricePostDiscounts;
            }

            return TotalValueOfSongs;


        }

        [Authorize(Roles = "Customer")]
        public decimal CalculateAlbumTotal()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            decimal TotalValueOfAlbums = 0.0m;

            foreach (AlbumInShoppingCart scalbum in userLoggedIn.AlbumsInShoppingCart)
            {
                decimal AlbumPricePostDiscounts = 0.0m;
                // calculate total discounts
                foreach (Discount discount in scalbum.Album.AlbumDiscounts)
                {
                    if (discount.IsActiveDiscount)
                    {
                        AlbumPricePostDiscounts = scalbum.Album.AlbumPrice * (1 - discount.DiscountPercentage);

                    }

                }

                TotalValueOfAlbums += AlbumPricePostDiscounts;
            }

            return TotalValueOfAlbums;


        }
        

        //public decimal CalculateSongTotalDiscount(int SongID)
        //{
        //    Song SongToCalc = db.Songs.Find(SongID);
        //    decimal TotalDiscount = 0.0m;
        //    foreach (Discount discount in SongToCalc.SongDiscounts)
        //    {
        //        if (discount.IsActiveDiscount)
        //        {
        //            TotalDiscount += discount.DiscountAmount;
        //        }
                
        //    }

        //    return TotalDiscount;
        //}

        //public decimal CalculateAlbumTotalDiscount(int AlbumID)
        //{
        //    Album AlbumToCalc = db.Albums.Find(AlbumID);
        //    decimal TotalDiscount = 0.0m;
        //    foreach (Discount discount in AlbumToCalc.AlbumDiscounts)
        //    {
        //        if (discount.IsActiveDiscount)
        //        {
        //            TotalDiscount += discount.DiscountAmount;
        //        }
                
        //    }

        //    return TotalDiscount;
        //}

        //TODO: need to test

        public bool DuplicatesExist()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            // get a list of all the songs in a given shopping cart
            List<Song> SongsList = new List<Song>();
            foreach (var song in userLoggedIn.SongsInShoppingCart)
            {
                SongsList.Add(db.Songs.Find(song.Song.SongID));
            }

            foreach (var album in userLoggedIn.AlbumsInShoppingCart)
            {
                foreach (var song in album.Album.AlbumSongs)
                {
                    SongsList.Add(db.Songs.Find(song.SongID));
                }
            }

            //now SongsList has all the songs in the shopping cart

            // return true if duplicates
            var duplicateSongs = SongsList.GroupBy(a => new { a.SongName, a.SongArtists }).Where(g => g.Count() > 1);
            return (duplicateSongs.Count() > 0);            
            




        }

        //// POST: ShoppingCarts/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public void Create([Bind(Include = "Id,TotalPrice")] ShoppingCart shoppingCart, AppUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ShoppingCarts.Add(new ShoppingCart {
        //            TotalPrice = shoppingCart.TotalPrice,
        //            Customer = user
        //        });
        //        db.SaveChanges();

        //    }

        //}

        //// GET: ShoppingCarts/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
        //    if (shoppingCart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ShoppingCartID = new SelectList(db.AppUsers, "Id", "FName", shoppingCart.ShoppingCartID);
        //    return View(shoppingCart);
        //}

        //// POST: ShoppingCarts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ShoppingCartID,TotalPrice")] ShoppingCart shoppingCart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(shoppingCart).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ShoppingCartID = new SelectList(db.AppUsers, "Id", "FName", shoppingCart.ShoppingCartID);
        //    return View(shoppingCart);
        //}

        //UNFINISHED
        //Delete Song Method
        [Authorize(Roles = "Customer")]
        public ActionResult DeleteSong(int SongID)
        {
            AppUser userloggedin = db.Users.Find(User.Identity.GetUserId());
            SongInShoppingCart songToRemove = db.SongsInShoppingCart.First(a => a.Song.SongID == SongID);
            userloggedin.SongsInShoppingCart.Remove(userloggedin.SongsInShoppingCart.First(x => x.Song.SongID == SongID));
            db.SaveChanges();

            return View("Index");

        }
        //find the song in DB that was sent to delete method & delete from shoppingcart.songs

        //Delete Album Method   
        //Delete Song Method
        [Authorize(Roles = "Customer")]
        public ActionResult DeleteAlbum(int AlbumID)
        {
            AppUser userloggedin = db.Users.Find(User.Identity.GetUserId());
            AlbumInShoppingCart albumToRemove = db.AlbumsInShoppingCart.First(a => a.Album.AlbumID == AlbumID);
            userloggedin.AlbumsInShoppingCart.Remove(userloggedin.AlbumsInShoppingCart.First(x => x.Album.AlbumID == AlbumID));
            db.SaveChanges();

            return View("Index");

        }

        //// GET: ShoppingCarts/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
        //    if (shoppingCart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(shoppingCart);
        //}

        //// POST: ShoppingCarts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
        //    db.ShoppingCarts.Remove(shoppingCart);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
