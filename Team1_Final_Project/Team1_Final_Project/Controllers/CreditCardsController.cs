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
            return View(db.CreditCards.ToList());
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

            if (UserToChange != null)
            {
                if (UserToChange.CreditCards != null)
                {
                    var result = (from n in UserToChange.CreditCards select n).ToList();
                    //ViewBag.UserCreditCards = result;
                    var count = result.Count;

                    if (count >= 2)
                    {
                        ViewBag.ErrorMessage = "You already have 2 credit cards on file.";
                        return View(creditCard);
                    }
                }
            }

            if (creditCard.CreditCardNumber.Length == 15)
            {
                creditCard.CreditCardType = CreditCardType.AmericanExpress;

                //add credit cards
                UserToChange.CreditCards.Add(creditCard);

                //add to viewbag
                //ViewBag.UserCreditCards = UserToChange.CreditCards;

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

                //add to viewbag
                ViewBag.UserCreditCards = UserToChange.CreditCards;

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


        //public SelectList GetAllCreditCards(CreditCard creditCard) //Card ALREADY CHOSEN
        //{
        //    //populate list of committees
        //    var query = from c in db.CreditCards
        //                where c.Person.Id = User.Identity.GetUserId()
        //                orderby c.Name
        //                select c;
        //    //create list and execute query
        //    List<CreditCard> allCreditCards = query.ToList();

        //    //convert to select list
        //    SelectList list = new SelectList(allCreditCards, "CreditCardID", "CreditCardType", creditCard.Person.Id);

        //    return list;
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
