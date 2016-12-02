using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Rating;


namespace Team1_Final_Project.Models.Music
{
    
    public class Artist
    {
        // scalar properties
        [Required(ErrorMessage = "Artist ID is required.")]
        [Display(Name = "Artist ID")]
        public Int16 ArtistID { get; set; }

        [Required(ErrorMessage = "Artist Name is required.")]
        [Display(Name = "Artist Name")]
        public String ArtistName { get; set; }

        [Display(Name = "Average Artist Rating")]
        public decimal ArtistAverage { get; set; }

        // navigational properties
        public virtual List<Genre> ArtistGenres { get; set; }

        public virtual List<Song> ArtistSongs { get; set; }

        public virtual List<Album> ArtistAlbums { get; set; }

        public virtual List<MusicRating> ArtistRatings { get; set; }

        public virtual List<FeaturedItem> FeaturedItems { get; set; }

        public Artist()
        {
            this.ArtistGenres = new List<Genre>();
            this.ArtistRatings = new List<MusicRating>();
            this.FeaturedItems = new List<FeaturedItem>();
        }
    }
}