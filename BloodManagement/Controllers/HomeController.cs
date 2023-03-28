using BloodManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;

namespace BloodManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactModel contact)
		{
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            //sending email using smtp
            //using(var client = new SmtpClient())
            //{
            //    client.Connect("smtp.gmail.com");
            //    client.Authenticate("methraskarpraveen@gmail.com", "svmtscrcdkjwzato");

            //    var bodyBuilder = new BodyBuilder()
            //    {

            //        HtmlBody = $"<p>{contact.Name}</p><p>{contact.Email}</p><p>{contact.Phone}</p><p>{contact.Address}</p><p>{contact.City}</p><p>{contact.State}</p><p>{contact.PostalCode}</p><p>{contact.BloodGroup}</p><p>{contact.Message}</p>",
            //        TextBody = "{contact.Name} \r\n {contact.Email} \r\n {contact.Phone} \r\n {contact.Address} \r\n {contact.City} \r\n {contact.State} \r\n {contact.PostalCode} \r\n {contact.BloodGroup} \r\n {contact.Message}"
            //    };
            //    var message = new MimeMessage
            //    {
            //        Body = bodyBuilder.ToMessageBody()
            //    };
            //    message.From.Add(new MailboxAddress(contact.Name, contact.Email));
            //    message.To.Add(new MailboxAddress("Testing", "methraskarpraveen@gmail.com"));
            //    message.Subject = "New contact submitted data";

            //    client.Send(message);

            //    client.Disconnect(true);

            //}
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com");
                client.Authenticate("methraskarpraveen@gmail.com", "svmtscrcdkjwzato");

                // Define the table structure
                var table = $"<table style='border:1px solid #000;border-collapse:collapse'>" +
                                $"<tr style='border:1px solid #000'><td><b>Name:</b></td><td>{contact.Name}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>Email:</b></td><td>{contact.Email}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>Phone:</b></td><td>{contact.Phone}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>Address:</b></td><td>{contact.Address}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>City:</b></td><td>{contact.City}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>State:</b></td><td>{contact.State}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>Postal Code:</b></td><td>{contact.PostalCode}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>Blood Group:</b></td><td>{contact.BloodGroup}</td></tr>" +
                                $"<tr style='border:1px solid #000'><td><b>Message:</b></td><td>{contact.Message}</td></tr>" +
                            $"</table>";

                // Define the HTML and plain text versions of the message body
                var htmlBody = $"<html><body><p>Dear Sir/Madam,</p><p>Greetings of the Day...!,</p><p>I am {contact.Name}, I need uregent requirement of blood . Below is the information I have submitted:</p>{table}</body></html>";
                var textBody = $"Dear Sir/Madam,\n\nI am {contact.Name}, I need uregent requirement of blood . Below is the information I have submitted:\n\n{contact.Name}\n{contact.Email}\n{contact.Phone}\n{contact.Address}\n{contact.City}\n{contact.State}\n{contact.PostalCode}\n{contact.BloodGroup}\n{contact.Message}";

                var bodyBuilder = new BodyBuilder()
                {
                    HtmlBody = htmlBody,
                    TextBody = textBody
                };

                var message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()
                };

                //message.From.Add(new MailboxAddress("Your Name", "your_email@gmail.com"));
                //message.To.Add(new MailboxAddress(contact.Name, contact.Email));
                message.From.Add(new MailboxAddress(contact.Name, contact.Email ));
                message.To.Add(new MailboxAddress("admin", "methraskarpraveen@gmail.com"));
                message.Subject = "New contact submitted data";

                client.Send(message);

                client.Disconnect(true);
            }
            TempData["Message"] = "Thank You for your query. We will reach you soon!";
			return View("Contact");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
