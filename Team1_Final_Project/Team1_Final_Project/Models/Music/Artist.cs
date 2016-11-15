using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;

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

        [Required(ErrorMessage = "Artist Price is required.")]
        [Display(Name = "Artist Price")]
        public Decimal ArtistPrice { get; set; }

        // navigational properties
        public virtual List<Genre> Genres { get; set; }

        public virtual List<Song> ArtistSongs { get; set; }

        public virtual List<Album> ArtistAlbums { get; set; }

        public virtual List<AppUser> SongOwners { get; set; }
    }
}