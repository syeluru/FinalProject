using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1_Final_Project.Models.Identity;
using Team1_Final_Project.Models.Purchases;
using Team1_Final_Project.Models.Music;
using System.Net;
using System.Net.Mail;

namespace Team1_Final_Project.Controllers
{
    public class EmailController : Controller
    {
        /*
        private AppDbContext db = new AppDbContext();

        AppUser userLoggedIn = db.Users.Find(User.Identity.GetUserId());
        */

        //Need to send additional email to gift recipient
        public static void ReceiptEmail(AppUser userLoggedIn, string toEmailAddress, String emailSubject, String emailBody)
        {

            //create an email client to send the emails
            var client = new SmtpClient("Smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                    EnableSsl = true
                };

                /*
                Should create link to own website
                string url = Url.Action("action", "controller",
                    new System.Web.Routing.RouteValueDictionary(new { id = id }),
                    "http", Request.Url.Host);
                    https://msdn.microsoft.com/en-us/library/dd460297.aspx
                */

                emailBody = "Thank you for making a purchase with us today. _List of songs & albums_. Follow this link for all returns:  ";

                string finalMessage = emailBody + "\n\n MIS 333K Team 1 ";
                
                emailSubject = "Team 1: Order Receipt";

                MailAddress senderEmail = new MailAddress(toEmailAddress, "Longhorn Music Order Receipt");

                MailMessage mm = new MailMessage();
                mm.Subject = emailSubject;
                mm.Sender = senderEmail;
                mm.From = senderEmail;
                mm.To.Add(new MailAddress(toEmailAddress));
                mm.Body = finalMessage;
                client.Send(mm);
            }

        public static void AccountCreationEmail(AppUser userLoggedIn, string toEmailAddress, String emailSubject, String emailBody)
        {

            //create an email client to send the emails
            var client = new SmtpClient("Smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            emailBody = "Hello" + userLoggedIn.FName + ", \n\n Thank you for creating a new account. Your username is: " + userLoggedIn.Email + ". \n\n Cheers, \n Longhorn Music"; 

            string finalMessage = emailBody + "\n\n MIS 333K Team 1 ";
            emailSubject = "Team 1: Account Created";

            MailAddress senderEmail = new MailAddress(toEmailAddress, "Longhorn Music Account Created");

            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }

        public static void RefundEmail(AppUser userLoggedIn, string toEmailAddress, String emailSubject, String emailBody)
        {

            //create an email client to send the emails
            var client = new SmtpClient("Smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            //Need to send additional email to gift recipient
            emailBody = userLoggedIn.FName + ", \n\n We are sorry for any inconveniences. You should receive your refund of _totalprice_. The following songs have been removed from your library: _list of songs_ \n\n Cheers, \n Longhorn Music"  

            string finalMessage = emailBody + "\n\n MIS 333K Team 1 ";
            emailSubject = "Team 1: Refund Processed";

            MailAddress senderEmail = new MailAddress(toEmailAddress, "Longhorn Music Refund");

            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }
    }
}

*/