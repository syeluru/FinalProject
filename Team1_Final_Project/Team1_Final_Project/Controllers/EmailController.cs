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

        //might not need userLoggedIn parameter
        public static void SendEmail(AppUser userLoggedIn, string toEmailAddress, String emailSubject, String emailBody)
        {

            //create an email client to send the emails
            var client = new SmtpClient("Smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("TeamOneFinalProject@gmail.com", "MIS333KProject"),
                    EnableSsl = true
                };

                string finalMessage = emailBody + "\n\n This is async disclaimer that will be on all messages. ";
                emailSubject = "Team 1: " + emailSubject;

                MailAddress senderEmail = new MailAddress(toEmailAddress, "Longhorn Music Order Receipt");

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
}