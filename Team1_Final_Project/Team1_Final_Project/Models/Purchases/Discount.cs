using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Models.Purchases
{
    public class Discount
    {

        [Required(ErrorMessage = "Discounted Item ID is required.")]
        [Display(Name = "Discounted Item ID")]
        public Int16 DiscountID { get; set; }

        [Required(ErrorMessage = "Discount Amount is Required")]
        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "Need to know if this is discount is active or not.")]
        [Display(Name = "Is Discount Active?")]
        public Boolean IsActiveDiscount { get; set; }

        // navigational properties
        public virtual Song DiscountedSong { get; set; }
        public virtual Album DiscountedAlbum { get; set; }
        
        public Discount()
        {

            this.IsActiveDiscount = true;
        }

    }

}