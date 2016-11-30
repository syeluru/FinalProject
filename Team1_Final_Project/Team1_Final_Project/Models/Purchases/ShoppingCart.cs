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
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Shopping Cart ID")]
        public int ShoppingCartID { get; set; }

        [Display(Name = "Total Price")]
        public Decimal TotalPrice { get; set; }

        public ShoppingCart()
        {
            this.TotalPrice = 0.0m;

        }

        // navigational properties
        
        public virtual AppUser Customer { get; set; }

        public virtual List<Song> Songs { get; set; }

        public virtual List<Album> Albums { get; set; }


    }
}