﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;
using Team1_Final_Project.Models.Identity;

namespace Team1_Final_Project.Models.Rating
{
    public class MusicRating
    {
        public int MusicRatingID { get; set; }

        public Decimal RatingNumber { get; set; }

        public String Review { get; set; }

        // navigational
        
        public virtual Song ReviewedSong { get; set; }
        public virtual Artist ReviewedArtist { get; set; }
        public virtual Album ReviewedAlbum { get; set; }
        public virtual AppUser Customer { get; set; }

    }
}