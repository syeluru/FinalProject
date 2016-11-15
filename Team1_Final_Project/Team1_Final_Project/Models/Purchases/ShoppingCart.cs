using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Models.Purchases
{
    public class ShoppingCart
    {
        // scalar properties
        [Required(ErrorMessage = "Shopping Cart ID is required")]
        [Display(Name = "Shopping Cart ID")]
        [ForeignKey("Customer")]
        public String ShoppingCartID { get; set; }

        [Required(ErrorMessage = "Total Price is required")]
        [Display(Name = "Total Price")]
        public Decimal TotalPrice { get; set; }

        // navigational properties
        public virtual AppUser Customer { get; set; }

        public virtual List<Song> Songs { get; set; }

        public virtual List<Album> Albums { get; set; }


    }
}