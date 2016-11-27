using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Team1_Final_Project.Models.Purchases;
using Team1_Final_Project.Models.Music;
using System.ComponentModel;


//TODO: Change the namespace here to match your project's name
namespace Team1_Final_Project.Models.Identity
{

    public enum UserType
    {
        Customer,
        Employee,
        Manager
    }
    // You can add profile data for the user by adding more properties to your AppUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
        //TODO: Put any additional fields that you need for your users here
        
        //Scalar Properties
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public String FName { get; set; }

        [Display(Name = "Middle Name")]
        public String MName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public String LName { get; set; }

        [Required(ErrorMessage = "Street Address is required.")]
        [Display(Name = "Street Address ")]
        public String StreetAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        public String State { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [Display(Name = "Zip Code")]
        //regular expression zip code to let it be a string - dunno if this is the optimal way to do this
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid Zip Code")]
        public String ZipCode { get; set; }

        [Required(ErrorMessage = "Need to know if account is enabled or not")]
        public Boolean IsAccountEnabled { get; set; }

        //Employee Stuff
        [Required(ErrorMessage = "Employee SSN is required".)]
        [Display(Name = "Social Security Number")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "Employee Type is required.")]
        [Display(Name = "Employee Type")]
        public Int16 EmpType { get; set; }

        //Navigational Properties
        //TODO: need to add validation in terms of how many credit cards a person can or can't have
        public virtual List<CreditCard> CreditCards { get; set; }

        //TODO: add a ShoppingCart model (or whatever we ended up doing to handle purchases)
        public virtual ShoppingCart ShoppingCart { get; set; }

        //TODO: add an Order model (or whatever we ended up doing to handle purchases) 
        public virtual List<Order> Orders { get; set; }

        public virtual List<Song> Songs { get; set; }

        public virtual List<Album> Albums { get; set; }

         

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public AppUser()
        {
            IsAccountEnabled = true;
            this.CreditCards = new List<CreditCard>();
        }
    }

    //NOTE: Here is your dbContext for the entire project.  There should only be ONE DbContext per project
    //Your dbContext (AppDbContext) inherits from IdentityDbContext, which inherits from the "regular" DbContext
    //TODO: If you have an existing dbContext (it may be in your DAL folder), DELETE THE EXISTING dbContext

    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //TODO: Add your dbSets here.  As an example, I've included one for products
        //Remember - the IdentityDbContext already contains a db set for Users.  If you add another one, your code will break
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AlbumOrderBridge> AlbumOrderBridge { get; set; }
        public DbSet<SongOrderBridge> SongOrderBridge { get; set; } 
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        //TODO: add ratings dbSets

                
        public AppDbContext()
            : base("MyDbConnection", throwIfV1Schema: false)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        
        //Add dbSet for roles
         public DbSet<AppRole> AppRoles { get; set; }
    }
}
