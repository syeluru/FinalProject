using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;

namespace Team1_Final_Project.Models.Music
{
    public class CustomerMusicViewModel
    {
        public AppUser Customer { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
    }
}