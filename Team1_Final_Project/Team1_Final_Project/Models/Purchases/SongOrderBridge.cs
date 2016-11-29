using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Models.Purchases
{
    public class SongOrderBridge
    {
        //just adding an ID so that the website stops throwing errors, can change the variable name later
        public Int32 SongOrderBridgeID { get; set; }

        public decimal PriceAtPointOfPurchase { get; set; }

        //nav properties
        public virtual Song Song { get; set; }
        public virtual Order Order { get; set; }
    }
}