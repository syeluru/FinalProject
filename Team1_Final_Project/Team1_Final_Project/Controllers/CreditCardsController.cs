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
using System.Text;
using Microsoft.AspNet.Identity;

namespace Team1_Final_Project.Controllers
{

    public class CreditCardsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: CreditCards
        public ActionResult Index()
        {
            AppUser UserToChange = db.Users.Find(User.Identity.GetUserId());
            //create list of selected Cards
            List<CreditCard> SelectedCreditCards = new List<CreditCard>();

            //Loop through list of Cards and add CardId
            foreach (CreditCard m in UserToChange.CreditCards)
            {
                SelectedCreditCards.Add(m);
            }

            //add to viewbag
            ViewBag.UserCreditCards = SelectedCreditCards;

            return View();
        }

        // GET: CreditCards/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCards/Create
        [Authorize]
        public ActionResult Create()
        {

            AppUser UserToChange = db.Users.Find(User.Identity.GetUserId());

            if (UserToChange != null)
            {
                if (UserToChange.CreditCards != null)
                {
                    //find the list of credit cards
                    var query2 = (from m in UserToChange.CreditCards
                                  select m).ToList();
                    var count = query2.Count;

                    if (count >= 2)
                    {
                        ViewBag.ErrorMessage = "You already have 2 credit cards on file.";
                        return RedirectToAction("CustomerDashboard", "Account");
                    }
                }
            }

            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditCardID, CreditCardNumber")] CreditCard creditCard)
        {
            
            //Find associated Member
            AppUser UserToChange = db.Users.Find(User.Identity.GetUserId());



            if (creditCard.CreditCardNumber.Length == 15)
            {
                creditCard.CreditCardType = CreditCardType.AmericanExpress;

                //add credit cards
                UserToChange.CreditCards.Add(creditCard);

                //add the credit card to the db
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (creditCard.CreditCardNumber.Length == 16)
            {
                if (creditCard.CreditCardNumber.Substring(0, 2) == "54")
                {
                    creditCard.CreditCardType = CreditCardType.MasterCard;
                }
                else if (creditCard.CreditCardNumber.Substring(0, 1) == "4")
                {
                    creditCard.CreditCardType = CreditCardType.Visa;
                }
                else if (creditCard.CreditCardNumber.Substring(0, 1) == "6")
                {
                    creditCard.CreditCardType = CreditCardType.Discover;
                }
                else
                {
                    ViewBag.ErrorMessage = "Please enter a valid credit card number";
                    return View(creditCard);
                }
                //add credit cards
                UserToChange.CreditCards.Add(creditCard);

                //add the credit card to the db
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                 //this means something is wrong
                 ViewBag.ErrorMessage = "Please enter a valid credit card number";
                 return View(creditCard);
            }
        }

        // GET: CreditCards/Create
        [Authorize]
        public ActionResult AddCreditCard()
        {

            AppUser UserToChange = db.Users.Find(User.Identity.GetUserId());

            if (UserToChange != null)
            {
                if (UserToChange.CreditCards != null)
                {
                    //find the list of credit cards
                    var query2 = (from m in UserToChange.CreditCards
                                  select m).ToList();
                    var count = query2.Count;

                    if (count >= 2)
                    {
                        ViewBag.ErrorMessage = "You already have 2 credit cards on file.";
                        return RedirectToAction("CheckoutPage", "ShoppingCart");
                    }
                }
            }

            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddCreditCard([Bind(Include = "CreditCardID, CreditCardNumber")] CreditCard creditCard)
        {

            //Find associated Member
            AppUser UserToChange = db.Users.Find(User.Identity.GetUserId());



            if (creditCard.CreditCardNumber.Length == 15)
            {
                creditCard.CreditCardType = CreditCardType.AmericanExpress;

                //add credit cards
                UserToChange.CreditCards.Add(creditCard);

                //add the credit card to the db
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return View("CheckoutPage", "ShoppingCart");
            }
            else if (creditCard.CreditCardNumber.Length == 16)
            {
                if (creditCard.CreditCardNumber.Substring(0, 2) == "54")
                {
                    creditCard.CreditCardType = CreditCardType.MasterCard;
                }
                else if (creditCard.CreditCardNumber.Substring(0, 1) == "4")
                {
                    creditCard.CreditCardType = CreditCardType.Visa;
                }
                else if (creditCard.CreditCardNumber.Substring(0, 1) == "6")
                {
                    creditCard.CreditCardType = CreditCardType.Discover;
                }
                else
                {
                    ViewBag.ErrorMessage = "Please enter a valid credit card number";
                    return View(creditCard);
                }
                //add credit cards
                UserToChange.CreditCards.Add(creditCard);

                //add the credit card to the db
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return View("CheckoutPage","ShoppingCart");
            }

            else
            {
                //this means something is wrong
                ViewBag.ErrorMessage = "Please enter a valid credit card number";
                return View(creditCard);
            }
        }

        // GET: CreditCards/Create
        [Authorize]
        public ActionResult AddCreditCardForGiftCheckout()
        {

            AppUser UserToChange = db.Users.Find(User.Identity.GetUserId());

            if (UserToChange != null)
            {
                if (UserToChange.CreditCards != null)
                {
                    //find the list of credit cards
                    var query2 = (from m in UserToChange.CreditCards
                                  select m).ToList();
                    var count = query2.Count;

                    if (count >= 2)
                    {
                        ViewBag.ErrorMessage = "You already have 2 credit cards on file.";
                        return RedirectToAction("CheckoutPage", "ShoppingCart");
                    }
                }
            }

            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddCreditCardForGiftCheckout([Bind(Include = "CreditCardID, CreditCardNumber")] CreditCard creditCard)
        {

            //Find associated Member
            AppUser UserToChange = db.Users.Find(User.Identity.GetUserId());



            if (creditCard.CreditCardNumber.Length == 15)
            {
                creditCard.CreditCardType = CreditCardType.AmericanExpress;

                //add credit cards
                UserToChange.CreditCards.Add(creditCard);

                //add the credit card to the db
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return View("GiftCheckoutPage", "ShoppingCart");
            }
            else if (creditCard.CreditCardNumber.Length == 16)
            {
                if (creditCard.CreditCardNumber.Substring(0, 2) == "54")
                {
                    creditCard.CreditCardType = CreditCardType.MasterCard;
                }
                else if (creditCard.CreditCardNumber.Substring(0, 1) == "4")
                {
                    creditCard.CreditCardType = CreditCardType.Visa;
                }
                else if (creditCard.CreditCardNumber.Substring(0, 1) == "6")
                {
                    creditCard.CreditCardType = CreditCardType.Discover;
                }
                else
                {
                    ViewBag.ErrorMessage = "Please enter a valid credit card number";
                    return View(creditCard);
                }
                //add credit cards
                UserToChange.CreditCards.Add(creditCard);

                //add the credit card to the db
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return View("GiftCheckoutPage", "ShoppingCart");
            }

            else
            {
                //this means something is wrong
                ViewBag.ErrorMessage = "Please enter a valid credit card number";
                return View(creditCard);
            }
        }



        // GET: CreditCards/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CreditCardID,CreditCardNumber")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditCard);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //public SelectList GetAllCards(AppUser UserToChange)
        //{
        //    //create list of selected Cards
        //    List<String> SelectedCreditCards = new List<String>();

        //    //Loop through list of Cards and add CardId
        //    foreach (CreditCard m in UserToChange.CreditCards)
        //    {
        //        SelectedCreditCards.Add(m.CreditCardNumber);
        //    }

        //    //SelectList allCreditCardsList = new SelectList(SelectedCreditCards);
            
        //    return SelectedCreditCards;
        //}




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
