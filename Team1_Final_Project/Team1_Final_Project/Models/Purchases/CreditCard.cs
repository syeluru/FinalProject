using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models;
using Team1_Final_Project.Models.Identity;

namespace Team1_Final_Project.Models.Purchases
{
    public enum CreditCardType
    {
        Visa,
        AmericanExpress,
        Discover,
        MasterCard,
        None
    }
    //testing
    public class CreditCard
    {
        // scalar properties
        [Required(ErrorMessage = "Credit Card Number is required.")]
        [Display(Name = "Credit Card ID")]
        public Int16 CreditCardID { get; set; }

        [Required]
        [Display(Name = "Credit Card Type")]
        [DefaultValue(CreditCardType.None)]
        public CreditCardType CreditCardType { get; set; }

        //navigational properties
        public virtual AppUser Person { get; set; }
    }
}