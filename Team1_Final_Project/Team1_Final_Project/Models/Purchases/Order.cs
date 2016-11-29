using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;

namespace Team1_Final_Project.Models.Purchases
{
    public class Order
    {
        // scalar properties
        [Required(ErrorMessage = "Order ID is required")]
        [Display(Name = "Order ID")]
        public Int16 OrderID { get; set; }

        [Required(ErrorMessage = "Total Price is required")]
        [Display(Name = "Total Price")]
        public Decimal TotalPrice { get; set; }

        // navigational properties
        public virtual AppUser Customer { get; set; }
        public virtual CreditCard CreditCardUsed { get; set; }
        public virtual List<SongOrderBridge> SongsInOrder { get; set; }
        public virtual List<AlbumOrderBridge> AlbumsInOrder { get; set; }
        
    }
}