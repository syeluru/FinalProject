using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team1_Final_Project.Models.Music;


//**Do we even need this bridge table now that we've gotten rid of the payload??*
namespace Team1_Final_Project.Models.Purchases
{
    public class AlbumOrderBridge
    {   
        //just adding an ID so that the website stops throwing errors, can change the variable name later
        public Int32 AlbumOrderBridgeID { get; set; }

        //these could be lists or virtuals, depending on the relationship. Pretty sure virtuals
        public virtual Album Album { get; set; }
        public virtual Order Order { get; set; }
    }
}