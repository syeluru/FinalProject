using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team1_Final_Project.Models.Music
{
    public class Genre
    {
        // scalar properties
        [Required(ErrorMessage = "Genre ID is required.")]
        [Display(Name = "Genre ID")]
        public Int16 GenreID { get; set; }

        [Required(ErrorMessage = "Genre Name is required")]
        [Display(Name = "Genre Name")]
        public String GenreName { get; set; }

        // navigational properties
        public virtual List<Song> Songs { get; set; }
        public virtual List<Artist> Artists { get; set; }
        public virtual List<Album> Albums { get; set; }
    }
}