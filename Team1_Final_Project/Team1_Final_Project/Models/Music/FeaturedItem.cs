using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team1_Final_Project.Models.Music
{
    public class FeaturedItem
    {

        [Required(ErrorMessage = "Featured Item ID is required.")]
        [Display(Name = "Featured Item ID")]
        public Int16 FeaturedItemID { get; set; }

        [Required(ErrorMessage = "Need to know if this is the active featured item or not.")]
        [Display(Name = "Is Featured Item?")]
        public Boolean IsActiveFeaturedItem { get; set; }

        // navigational properties
        public virtual Song FeaturedSong { get; set; }
        public virtual Album FeaturedAlbum { get; set; }
        public virtual Artist FeaturedArtist{ get; set; }


    }

}