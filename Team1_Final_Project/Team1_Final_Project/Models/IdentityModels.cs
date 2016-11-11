using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

//TODO: Change the namespace here to match your project's name
namespace Team1_Final_Project.Models
{
    // You can add profile data for the user by adding more properties to your AppUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
        //TODO: Put any additional fields that you need for your users here
        //testing
        
        //Scalar Properties
        public String FName { get; set; }
        public String LName { get; set; }
        public String StreetAddress { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        
        //Navigational Properties
        //TODO: need to add validation in terms of how many credit cards a person can or can't have
        public virtual List<CreditCard> CreditCards { get; set; }

        //TODO: add a ShoppingCart model (or whatever we ended up doing to handle purchases)
        public virtual ShoppingCart ShoppingCart { get; set; }

        //TODO: add an Order model (or whatever we ended up doing to handle purchases) 
        public virtual List<Order> Orders { get; set; }

         

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    //NOTE: Here is your dbContext for the entire project.  There should only be ONE DbContext per project
    //Your dbContext (AppDbContext) inherits from IdentityDbContext, which inherits from the "regular" DbContext
    //TODO: If you have an existing dbContext (it may be in your DAL folder), DELETE THE EXISTING dbContext

    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //TODO: Add your dbSets here.  As an example, I've included one for products
        //Remember - the IdentityDbContext already contains a db set for Users.  If you add another one, your code will break
        //public DbSet<Product> Products { get; set; }
                
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
