using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Rating;
using Team1_Final_Project.Models.Purchases;


namespace Team1_Final_Project.Models.Music
{
    public class Album
    {
        // scalar properties
        [Required(ErrorMessage = "Album ID is required.")]
        [Display(Name = "Album ID")]
        public Int16 AlbumID { get; set; }

        [Required(ErrorMessage = "Album Name is required.")]
        [Display(Name = "Album Name")]
        public String AlbumName { get; set; }

        [Required(ErrorMessage = "Album Price is required.")]
        [Display(Name = "Album Price")]
        public Decimal AlbumPrice { get; set; }
        
        [Display(Name = "Album Discount")]
        public Decimal AlbumDiscount { get; set; }

        //[Display(Name = "Average Album Rating")]
        //public decimal AverageAlbumRating { get; set; }

        // navigational properties
        public virtual List<Genre> AlbumGenres { get; set; }

        public virtual List<Artist> AlbumArtists { get; set; }

        public virtual List<Song> AlbumSongs { get; set; }

        public virtual List<AppUser> AlbumOwners { get; set; }

        public virtual List<MusicRating> AlbumRatings { get; set; }

        public virtual List<AlbumInShoppingCart> AlbumsInShoppingCart { get; set; }

        public Album()
        {
            this.AlbumGenres = new List<Genre>();
            this.AlbumArtists = new List<Artist>();
            this.AlbumSongs = new List<Song>();
            this.AlbumOwners = new List<AppUser>();
            this.AlbumRatings = new List<MusicRating>();
            this.AlbumsInShoppingCart = new List<AlbumInShoppingCart>();
        }
        
    }
}