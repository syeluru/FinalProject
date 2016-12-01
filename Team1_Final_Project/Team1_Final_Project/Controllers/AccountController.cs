
using Team1_Final_Project.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Team1_Final_Project.Models.Music;
using Team1_Final_Project.Models.Rating;
using Team1_Final_Project.Models.Purchases;
using System.Collections.Generic;
using System.Web.Security;
using System.Net;
using System.Data.Entity;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;

namespace Team1_Final_Project.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private AppDbContext db = new AppDbContext();

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private AppSignInManager _signInManager;
        private AppUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(AppUserManager userManager, AppSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)//NOTE: User has been re-directed here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            AuthenticationManager.SignOut();  //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (db.Users.Any(a => a.Email == model.Email))
            {
                AppUser user = db.Users.First(a => a.Email == model.Email);
                if (user.Roles.Count(a => a.RoleId == "302b3f6a-ff8b-4ffb-96f4-5f829a34d5d0") > 0)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, change to shouldLockout: true
                    var CustResult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                    switch (CustResult)
                    {
                        case SignInStatus.Success:
                            return RedirectToLocal(returnUrl);
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }
                }
                else if (user.Roles.Count(a => a.RoleId == "d2a5f583-4bd5-4470-a241-855a1047a407") > 0)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, change to shouldLockout: true
                    var ManagerResult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                    switch (ManagerResult)
                    {
                        case SignInStatus.Success:
                            return RedirectToAction("ManagerDashboard");
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }

                }
                else if (user.Roles.Count(a => a.RoleId == "4c919864-b4b2-4837-9e2f-7d171580fc99") > 0)
                {
                    var EmpResult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                    switch (EmpResult)
                    {
                        case SignInStatus.Success:
                            return RedirectToAction("EmployeeDashboard");
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }
                }


                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }


            }
            else { 
            
                var initialresult = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);

            }
            //pseudocode:
            // get the user that has the corresponding email from the LoginViewModel
            // add the following code at the bottom into three if statements, each with similar code but different return views

            
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Add fields to user here so they will be saved to the database
                //Create a new user with all the properties you need for the class
                var user = new AppUser { UserName = model.Email, Email = model.Email, FName = model.FName, MName = model.MName, LName = model.LName, StreetAddress = model.StreetAddress, City = model.City, State = model.State, ZipCode = model.ZipCode, PhoneNumber = model.PhoneNumber, IsAccountEnabled = model.IsAccountEnabled};
                
                
                db.SaveChanges();
                                                    
                //Add the new user to the database
                var result = await UserManager.CreateAsync(user, model.Password);

                //TODO: Once you get roles working, you may want to add users to roles upon creation
                //await UserManager.AddToRoleAsync(user.Id, "User"); //adds user to role called "User"
                // --OR--
                await UserManager.AddToRoleAsync(user.Id, "Customer"); //adds user to role called "Customer"

                if (result.Succeeded) //user was created successfully
                {
                    //sign the user in
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    //send them to the page to add their credit cards
                    return RedirectToAction("CustomerDashboard", "Account");
                }

                //if there was a problem, add the error messages to what we will display
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //ADDING NEW STUFF STARTING NOW
        //


        // GET: Account/CustomerDashboard
        [Authorize]
        public ActionResult CustomerDashboard(string SuccessMessage)
        {
            if (SuccessMessage != null)
            {
                AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
                ViewBag.SuccessMessage = "";
                return View(userLoggedIn);
            } else
            {
                AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
                ViewBag.SuccessMessage = SuccessMessage;
                return View(userLoggedIn);
            }

        }

        // GET: Account/ManagerDashboard
        [Authorize(Roles = "Employee, Manager")]
        public ActionResult EmployeeDashboard(string SuccessMessage)
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            ViewBag.SuccessMessage = SuccessMessage;
            return View(userLoggedIn);
        }


        // GET: Account/ManagerDashboard
        [Authorize(Roles = "Manager")]
        public ActionResult ManagerDashboard()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            return View(userLoggedIn);
        }

        // GET: /Account/RegisterEmployee
        [Authorize(Roles = "Manager")]
        public ActionResult RegisterEmployee()
        {
            return View();
        }

        // POST: /Account/RegisterEmployee
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Add fields to user here so they will be saved to the database
                //Create a new user with all the properties you need for the class
                var user = new AppUser { UserName = model.Email, Email = model.Email, FName = model.FName, MName = model.MName, LName = model.LName, StreetAddress = model.StreetAddress, City = model.City, State = model.State, ZipCode = model.ZipCode, IsAccountEnabled = model.IsAccountEnabled, EmpType = model.EmpType, SSN = model.SSN, SongsInShoppingCart = null, AlbumsInShoppingCart = null, Songs = null, Albums = null, CreditCards = null };

                //Add the new user to the database
                var result = await UserManager.CreateAsync(user, model.Password);

                //TODO: Once you get roles working, you may want to add users to roles upon creation
                //await UserManager.AddToRoleAsync(user.Id, "User"); //adds user to role called "User"
                // --OR--
                await UserManager.AddToRoleAsync(user.Id, "Employee"); //adds user to role called "Employee"

                if (result.Succeeded) //user was created successfully
                {
                    //sign the user in
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //send them to the home page
                    return RedirectToAction("ManagerDashboard", "Account");
                }

                //if there was a problem, add the error messages to what we will display
                AddErrors(result);
            }

            //comment

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //ADDING NEW STUFF ENDING NOW


        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        // GET: /Account/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // GET: /Account/ChangeCustomerPassword?Id=whatever
        public ActionResult ChangeCustomerPassword(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }



        // see all songs
        //TODO: seeing as this is quite the gamble in terms of code, need to display a list of songs that a customer owns. this is easy if we want to recreate the 
        // song index view, but i'm trying to re-use that view. will get back to you on how that works.
        public ActionResult SeeAllSongs()
        {
            AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
            return View("Index", "Songs", userLoggedIn.Songs);
        }

        // see all artists

        // GET: Account/EmployeeEdit/5
        //TODO: Modify phone number and address
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeEdit(string id)
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

        // POST: Account/Edit/5
        //Done: Modify address and phone number
        // if anything looks strange, check out this
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]

        public ActionResult EmployeeEdit([Bind(Include = "StreetAddress,City,State,ZipCode,PhoneNumber")] AppUser Member)//, int[] SelectedEvents)
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


                db.Entry(MemberToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EmployeeDashboard");
            }

            return View(Member);
        }


        // GET: Account/EditFromManager/
        //TODO: Modify phone number and address
        [Authorize(Roles = "Manager")]
        public ActionResult EditFromManager(string id)
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

        // POST: Account/EditFromManager/5
        //Done: Modify address and phone number
        // if anything looks strange, check out this
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]

        public ActionResult EditFromManager([Bind(Include = "StreetAddress,City,State,ZipCode,PhoneNumber,IsActive")] AppUser Member)//, int[] SelectedEvents)
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
                MemberToChange.IsActive = Member.IsActive;


                db.Entry(MemberToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EmployeeDashboard");
            }

            return View(Member);
        }


        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeCustomerPassword(/*ChangePasswordViewModel model, */string Id, string NewPassword, string ConfirmPassword)
        {
            var provider = new DpapiDataProtectionProvider("YourAppName");
            UserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, string>(provider.Create("UserToken"))
                as IUserTokenProvider<AppUser, string>;

            if (!ModelState.IsValid)


            {
                ViewBag.Id = Id;
                ViewBag.TestMessage = "Test Message from ChangeCustomerPassword Post Method in the initial IsValid if statement";
                return View();
            }

            // check to see that the two passwords are the same
            if (NewPassword != ConfirmPassword)
            {
                ViewBag.Id = Id;
                ViewBag.ErrorMessage = "Make sure your passwords are the same.";
                return View();
            }

            
            try
            {
                string resetToken = await UserManager.GeneratePasswordResetTokenAsync(Id);
                IdentityResult passwordChangeResult = await UserManager.ResetPasswordAsync(Id, resetToken, NewPassword);

                db.SaveChanges();

                return RedirectToAction("EmployeeDashboard", new { SuccessMessage = ManageMessageId.ChangePasswordSuccess });
                


            } catch(Exception e)
            {
                ViewBag.ErrorMessage = "Looks like some error happened. It might be " + e.ToString();
                ViewBag.Id = Id;
                return View("ChangeCustomerPassword");

            }





            //var result = await UserManager.ChangePasswordAsync(Id, model.OldPassword, model.NewPassword);
            //if (result.Succeeded)
            //{

            //    return RedirectToAction("EmployeeDashboard", new { SuccessMessage = ManageMessageId.ChangePasswordSuccess });
            //}
            //AddErrors(result);
            //ViewBag.Id = Id;
            //ViewBag.TestMessage = "Test Message from ChangeCustomerPassword Post Method in the end of the method";
        }

        // GET: /Account/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

       
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}