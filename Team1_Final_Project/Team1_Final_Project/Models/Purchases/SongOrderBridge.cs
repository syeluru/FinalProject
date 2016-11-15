using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Models.Purchases
{
    public class SongOrderBridge
    {
        public Int32 SongOrderBridgeID { get; set; }
        public virtual Song Song { get; set; }
        public virtual Order Order { get; set; }
    }
}