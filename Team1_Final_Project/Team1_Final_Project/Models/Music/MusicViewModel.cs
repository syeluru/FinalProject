using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team1_Final_Project.Models.Music
{
    public class MusicViewModel
    {
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
    }
}