using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Team1_Final_Project.Models.Identity;
using Microsoft.AspNet.Identity;
using Team1_Final_Project.Models.Purchases;
using Team1_Final_Project.Models.Music;

namespace Team1_Final_Project.Controllers
{
    public class EmailController : Controller
    {
        static AppDbContext db = new AppDbContext();

        public static void OrderCustomer(AppUser userLoggedIn, String SongsPurchased, String AlbumsPurchased, String Recommendation, int OrderId)
        {
            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };


            //userLoggedIn.Email
            String link = "< a href =\\Orders\\Refund\\> Refund</a>";

            String emailBody = "Hello " + userLoggedIn.FName + ", \n\n Your Order was completed succesfully. \n\n Songs Purchased: " + SongsPurchased + ". \n Album Purchased: " + AlbumsPurchased + ". \n\n Judging from your taste, you should check out " + Recommendation + ". \n\n If you would like to make a return, click on the following link: " + link + " Thank you! \n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("dpimentel.p@gmail.com", "Order Confirmation");

            String emailSubject = "Team 1: Longhorn Music Order Confirmation";

            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress("dpimentel.p@gmail.com"));
            mm.Body = finalMessage;
            mm.IsBodyHtml = true;
            client.Send(mm);
        }

        public static void OrderGift(AppUser userLoggedIn, AppUser Friend, String SongsGifted, String AlbumsGifted)
        {

            //userLoggedIn = db.Users.Find(User.Identity.GetUserId());

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };


            //RECIPIENT EMAIL
            String link = "< a href =\\Orders\\Refund\\> Refund</a>";

            String emailBody = "Hello " + userLoggedIn.FName + ", \n\n Your order went through. " + Friend.FName + " received a confirmation email notifying them that the gift has been added to their music library. If you would like to make a return, click on the following link: " + link + " Thank you! \n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("dpimentel.p@gmail.com", "Order Confirmation");

            String emailSubject = "Team 1: Order Confirmation";


            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress("dpimentel.p@gmail.com"));
            mm.Body = finalMessage;
            mm.IsBodyHtml = true;
            client.Send(mm);

            //GIFT RECIPIENT EMAIL 
            //Friend.Email

            String emailBody1 = "Hello " + Friend.FName + ", \n\n Your friend, " + userLoggedIn.FName + ", has bought you a gift that has been added to your Longhorn Music library. \n\n Songs: " + SongsGifted + "\n Albums: " + AlbumsGifted + "\n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage1 = emailBody1 + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail1 = new MailAddress("dpimentel.p@gmail.com", "Gift");

            String emailSubject1 = "Team 1: You've got a gift!";

            MailMessage nn = new MailMessage();
            nn.Subject = emailSubject1;
            nn.Sender = senderEmail1;
            nn.From = senderEmail1;
            nn.To.Add(new MailAddress("dpimentel.p@gmail.com"));
            nn.Body = finalMessage1;
            client.Send(nn);
        }

        public static void Refund(AppUser userLoggedIn)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            //userLoggedIn.Email

            String emailBody = "Hello " + userLoggedIn.FName + ", \n\n Your order refund has been processed and the music has been removed from your library. Thank you! \n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("dpimentel.p@gmail.com", "Order Refund");

            String emailSubject = "Team 1: Order Refund";


            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress("dpimentel.p@gmail.com"));
            mm.Body = finalMessage;
            client.Send(mm);

        }

        public static void RefundGift(AppUser userLoggedIn, AppUser Friend)
        {
            
            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            //userLoggedIn.Email

            //Add anything that you need to the body of the message
            String emailBody = "Hello " + userLoggedIn.FName + ", \n\n Your order refund has been processed and the music has been removed from the recipient's library. Thank you! \n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("dpimentel.p@gmail.com", "Order Refund");

            String emailSubject = "Team 1: Order Refund";


            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress("dpimentel.p@gmail.com"));
            mm.Body = finalMessage;
            client.Send(mm);

            //GIFT RECIPIENT EMAIL 
            //Friend.Email

            String emailBody1 = "Hello " + Friend.FName + ", \n\n The purchaser of your gift has chosen to return the order. Thus, we have removed the music from your library. Thank you! \n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage1 = emailBody1 + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail1 = new MailAddress("dpimentel.p@gmail.com", "Gift Removal");

            String emailSubject1 = "Team 1: Gift Return";

            MailMessage nn = new MailMessage();
            nn.Subject = emailSubject1;
            nn.Sender = senderEmail1;
            nn.From = senderEmail1;
            nn.To.Add(new MailAddress("dpimentel.p@gmail.com"));
            nn.Body = finalMessage1;
            client.Send(nn);
        }


        public static void AccountCreation(AppUser userLoggedIn)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            String emailSubject = "Team 1: Longhorn Music Account Confirmation";

            //Add anything that you need to the body of the message
            String emailBody = "Hello " + userLoggedIn.FName + ", \n\n Your account with username " + userLoggedIn.Email + " was created succesfully. \n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team 1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress(userLoggedIn.Email, "Account Confirmation");

            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(userLoggedIn.Email));
            mm.Body = finalMessage;
            client.Send(mm);
        }
    }
}
