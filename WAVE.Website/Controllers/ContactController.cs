using System;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace WAVE.Website.Controllers
{
    public class ContactController : CultureController
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var username = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                username = HttpContext.User.Identity.Name;
            }
                var name = collection["Name"];
                var email = collection["Email"];
                var subject = "WAVE Contact Form";
                var msg = collection["Message"];
                var message = new MailMessage();

                message.From = new MailAddress(email);
                message.To.Add(new MailAddress("alex.talavari@gmail.com"));

                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = subject;
                message.Body = "Send from:" + name + Environment.NewLine + "Username:" + username + Environment.NewLine +
                               "Message:" + msg;

                var client = new SmtpClient();
                client.Send(message);
                Session["NotificationMessage"] = "Mail Sent successfully";
                return View();
            
        }


        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }
    }
}