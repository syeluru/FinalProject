using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Controllers
{
    public class FeaturedItemsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: FeaturedItems
        public ActionResult Index()
        {
            return View(db.FeaturedItems.ToList());
        }

        // GET: FeaturedItems/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            if (featuredItem == null)
            {
                return HttpNotFound();
            }
            return View(featuredItem);
        }

        // GET: FeaturedItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeaturedItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeaturedItemID,IsActiveFeaturedItem")] FeaturedItem featuredItem)
        {
            if (ModelState.IsValid)
            {
                db.FeaturedItems.Add(featuredItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(featuredItem);
        }

        // GET: FeaturedItems/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            if (featuredItem == null)
            {
                return HttpNotFound();
            }
            return View(featuredItem);
        }

        // POST: FeaturedItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeaturedItemID,IsActiveFeaturedItem")] FeaturedItem featuredItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featuredItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featuredItem);
        }

        // GET: FeaturedItems/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            if (featuredItem == null)
            {
                return HttpNotFound();
            }
            return View(featuredItem);
        }

        // POST: FeaturedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            FeaturedItem featuredItem = db.FeaturedItems.Find(id);
            db.FeaturedItems.Remove(featuredItem);
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
