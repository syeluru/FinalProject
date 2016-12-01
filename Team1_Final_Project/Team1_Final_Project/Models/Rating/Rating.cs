using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;
using Team1_Final_Project.Models.Identity;
using Microsoft.AspNet.Identity;

namespace Team1_Final_Project.Models.Rating
{
    public class MusicRating
    {
        public int MusicRatingID { get; set; }

        [Required]
        [RatingNumberValidation(ErrorMessage = "rating must be between 1.0 and 5.0 inclusive")]
        public Decimal RatingNumber { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public String Review { get; set; }

        // navigational
        
        public virtual Song ReviewedSong { get; set; }
        public virtual Artist ReviewedArtist { get; set; }
        public virtual Album ReviewedAlbum { get; set; }
        public virtual AppUser Customer { get; set; }

    }

    public class RatingNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            decimal getal;
            if (decimal.TryParse(value.ToString(), out getal))
            {

                if (getal < 1)
                    return false;

                if (getal > 5)
                    return false;
            }
            return true;
        }
    }
}