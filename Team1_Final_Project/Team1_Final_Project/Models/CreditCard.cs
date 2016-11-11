﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team1_Final_Project.Models
{
    public enum CreditCardType
    {
        Visa,
        AmericanExpress,
        Discover,
        MasterCard
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
        public CreditCardType CreditCardType { get; set; }

        //navigational properties
        public virtual AppUser Person { get; set; }
    }
}