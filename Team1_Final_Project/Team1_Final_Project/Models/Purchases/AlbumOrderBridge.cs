using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Models.Purchases
{
    public class AlbumOrderBridge
    {
        public Int32 AlbumOrderBridgeID { get; set; }
        public virtual Album Album { get; set; }
        public virtual Order Order { get; set; }
    }
}