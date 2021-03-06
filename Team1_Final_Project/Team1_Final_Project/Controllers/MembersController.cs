﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Microsoft.AspNet.Identity;

//Copied and pasted this controller from HW7, may need to tweak some things

namespace Team1_Final_Project.Controllers
{
    public class MembersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Members
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [Authorize(Roles = "Employee, Manager")]
        public ActionResult CustomersIndex()
        {

            List<AppUser> CustomersList = new List<AppUser>();
            foreach (var user in db.Users)
            {
                if (user.Roles.Any(role => role.RoleId == "302b3f6a-ff8b-4ffb-96f4-5f829a34d5d0"))
                {
                    CustomersList.Add(user);
                }
                   
            }

            return View(CustomersList);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult EmployeesIndex()
        {

            List<AppUser> EmployeesList = new List<AppUser>();
            foreach (var user in db.Users)
            {
                if (user.Roles.Any(role => role.RoleId == "4c919864-b4b2-4837-9e2f-7d171580fc99"))
                {
                    EmployeesList.Add(user);
                }
            }

            return View(EmployeesList);
        }

        // GET: Members/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser Member = db.Users.Find(id);
            if (Member == null)
            {
                return HttpNotFound();
            }
            return View(Member);
        }

        // GET: Members/Details/5
        [Authorize]
        public ActionResult EmployeeDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser Member = db.Users.Find(id);
            if (Member == null)
            {
                return HttpNotFound();
            }
            return View(Member);
        }

        // GET: Members/Edit/5
        //TODO: Modify first and last name, email, address and phone number
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppUser Member = db.Users.Find(id);
            if (Member == null)
            {
                return HttpNotFound();
            }

            if (Member.Id != User.Identity.GetUserId())
            {
                return RedirectToAction("Login", "Account");
            }

            return View(Member);
        }

        // POST: Members/Edit/5
        //Done: Modify first and last name, email, address and phone number
        // if anything looks strange, check out this
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FName,MName,LName,StreetAddress,City,State,ZipCode,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AppUser Member)//, int[] SelectedEvents)
        {
            if (ModelState.IsValid)
            {
                //Find associated Member
                AppUser MemberToChange = db.Users.Find(Member.Id);

                //update the rest of the fields
                MemberToChange.FName = Member.FName;
                MemberToChange.MName = Member.MName;
                MemberToChange.LName = Member.LName;
                MemberToChange.UserName = Member.UserName;
                MemberToChange.Email = Member.UserName;
                MemberToChange.StreetAddress = Member.StreetAddress;
                MemberToChange.City = Member.City;
                MemberToChange.State = Member.State;
                MemberToChange.ZipCode = Member.ZipCode;
                MemberToChange.PhoneNumber = Member.PhoneNumber;


                db.Entry(MemberToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Member);
        }

        // GET: Members/Edit/5
        //TODO: Modify first and last name, email, address and phone number
        [Authorize (Roles = "Employee, Manager")]
        public ActionResult EditFromEmployee(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppUser Member = db.Users.Find(id);
            if (Member == null)
            {
                return HttpNotFound();
            }
            
            return View(Member);
        }

        // POST: Members/Edit/5
        //Done: Modify first and last name, email, address and phone number
        // if anything looks strange, check out this
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO: Ensure bind does not create any issues
        public ActionResult EditFromEmployee([Bind(Include = "Id,FName,MName,LName,StreetAddress,City,State,ZipCode,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AppUser Member)//, int[] SelectedEvents)
        {
            if (ModelState.IsValid)
            {
                //Find associated Member
                AppUser MemberToChange = db.Users.Find(Member.Id);

                //update the rest of the fields
                MemberToChange.StreetAddress = Member.StreetAddress;
                MemberToChange.City = Member.City;
                MemberToChange.State = Member.State;
                MemberToChange.ZipCode = Member.ZipCode;
                MemberToChange.PhoneNumber = Member.PhoneNumber;
                MemberToChange.IsAccountEnabled = Member.IsAccountEnabled;

                db.Entry(MemberToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomersIndex");
            }

            ViewBag.ErrorMessage = "Something went wrong";
            return View(Member);
        }




        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser Member = db.Users.Find(id);
            if (Member == null)
            {
                return HttpNotFound();
            }
            return View(Member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AppUser Member = db.Users.Find(id);
            db.Users.Remove(Member);
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


