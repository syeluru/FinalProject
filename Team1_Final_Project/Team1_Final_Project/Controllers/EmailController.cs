using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Team1_Final_Project.Models.Identity;
using Microsoft.AspNet.Identity;

namespace Team1_Final_Project.Controllers
{
    public class EmailController : Controller
    {
        //private AppDbContext db = new AppDbContext();

        public static void OrderCustomer(AppUser userLoggedIn, String toEmailAddress, String emailSubject, String emailBody)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            //Add anything that you need to the body of the message
            emailBody = "";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("toEmailAddress", "Parkside Breakfast with Santa Auction");

            emailSubject = "";


            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }

        public static void OrderGift(AppUser userLoggedIn, String toEmailAddress, String emailSubject, String emailBody)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            //Add anything that you need to the body of the message
            emailBody = "";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("toEmailAddress", "Parkside Breakfast with Santa Auction");

            emailSubject = "";


            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }

        public static void RefundCustomer(AppUser userLoggedIn, String toEmailAddress, String emailSubject, String emailBody)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            //Add anything that you need to the body of the message
            emailBody = "";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("toEmailAddress", "Parkside Breakfast with Santa Auction");

            emailSubject = "";


            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }

        public static void RefundGift(AppUser userLoggedIn, String toEmailAddress, String emailSubject, String emailBody)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            //Add anything that you need to the body of the message
            emailBody = "";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("toEmailAddress", "Parkside Breakfast with Santa Auction");

            emailSubject = "";


            MailMessage mm = new MailMessage();
            mm.Subject = emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }


        public static void AccountCreation(AppUser userLoggedIn, String toEmailAddress, String emailSubject, String emailBody)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                EnableSsl = true
            };

            emailSubject = "Team 1: Longhorn Music Account Confirmation";

            //Add anything that you need to the body of the message
            emailBody = "Hello " + userLoggedIn.FName + ", \n\n Your account with username " + userLoggedIn.Email + "was created succesfully. \n\n Cheers, \n Longhorn Music";

            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n MIS 333K Team 1 ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress(toEmailAddress, "Account Confirmation");

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
