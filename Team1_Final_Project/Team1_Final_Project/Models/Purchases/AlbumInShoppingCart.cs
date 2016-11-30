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
    public class AlbumInShoppingCart
    {
        [Required]
        [Display(Name = "Album in Shopping Cart ID")]
        public int AlbumInShoppingCartID { get; set; }

        // navigational properties
        
        public virtual AppUser Customer { get; set; }

        public virtual Album Album { get; set; }



    }
}