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
        None,
        Visa,
        AmericanExpress,
        Discover,
        MasterCard
    }

    public class CreditCard
    {
        // scalar properties
        public Int32 CreditCardID { get; set; }

        [Required(ErrorMessage = "Credit Card Number is required.")]
        [Display(Name = "Credit Card Number")]
        public String CreditCardNumber { get; set; }

        //[Required]
        [Display(Name = "Credit Card Type")]
        //Automatically set default value to null
        [DefaultValue(CreditCardType.None)]
        public CreditCardType CreditCardType { get; set; }

        //navigational properties
        public virtual AppUser Person { get; set; }
    }
}