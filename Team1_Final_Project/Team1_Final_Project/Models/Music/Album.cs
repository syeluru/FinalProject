using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Rating;


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

        // navigational properties
        public virtual List<Genre> AlbumGenres { get; set; }

        public virtual List<Artist> AlbumArtists { get; set; }

        public virtual List<Song> AlbumSongs { get; set; }

        public virtual List<AppUser> SongOwners { get; set; }

        public virtual List<MusicRating> AlbumRatings { get; set; }

        public Album()
        {
            this.AlbumGenres = new List<Genre>();
            this.AlbumArtists = new List<Artist>();
        }
        
    }
}