using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Rating;
using Team1_Final_Project.Models.Purchases;


namespace Team1_Final_Project.Models.Music
{

    public enum CreditCardType
    {
        Visa,
        AmericanExpress,
        Discover,
        MasterCard
    }

    public class Song
    {

        // scalar properties
        [Required(ErrorMessage = "Song ID is required.")]
        [Display(Name = "Song ID")]
        public Int16 SongID { get; set; }

        [Required(ErrorMessage = "Song Name is required.")]
        [Display(Name = "Song Name")]
        public String SongName { get; set; }

        [Required(ErrorMessage = "Song Price is required.")]
        [Display(Name = "Song Price")]
        public Decimal SongPrice { get; set; }
        
        [Display(Name = "Song Discount")]
        public Decimal SongDiscount { get; set; }

        //[Display(Name = "Average Song Rating")]
        //public decimal AverageSongRating { get; set; }

        // navigational properties
        public virtual List<Genre> SongGenres { get; set; }

        public virtual List<Artist> SongArtists { get; set; }

        public virtual List<Album> SongAlbums { get; set; }

        public virtual List<AppUser> SongOwners { get; set; }

        public virtual List<MusicRating> SongRatings { get; set; }

        public virtual List<SongInShoppingCart> SongsInShoppingCart { get; set; }

        public virtual List<FeaturedItem> FeaturedItems { get; set; }

        public virtual List<SongOrderBridge> SongOrderBridges { get; set; }

        public Song()
        {
            this.SongAlbums = new List<Album>();
            this.SongArtists = new List<Artist>();
            this.SongGenres = new List<Genre>();
            this.SongRatings = new List<MusicRating>();
            this.SongsInShoppingCart = new List<SongInShoppingCart>();
            this.FeaturedItems = new List<FeaturedItem>();
            this.SongOrderBridges = new List<SongOrderBridge>();
        }
    }
}