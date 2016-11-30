using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Models.Purchases
{
    public class ShoppingCartViewModel
    {

        public IEnumerable<SongInShoppingCart> SongsInShoppingCart { get; set; }
        public IEnumerable<AlbumInShoppingCart> AlbumsInShoppingCart { get; set; }
    }

}