using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Models.Purchases
{
    public class SongOrderBridge
    {
        [Required]
        [Display(Name = "Song in Shopping Cart ID")]
        public Int32 SongOrderBridgeID { get; set; }

        public decimal PriceAtPointOfPurchase { get; set; }

        //nav properties
        public virtual Song SongInOrder { get; set; }
        public virtual Order Order { get; set; }
    }
}