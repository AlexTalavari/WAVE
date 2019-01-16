using System.Net.Mail;
using System.Text;

namespace WAVE.Website.Classes
{
    public class Email
    {
        public static void SendEmail(string email, string subj, string msg)
        {
            var message = new MailMessage();
            //message.From = new System.Net.Mail.MailAddress("save@quarinos.com");
            message.To.Add(new MailAddress(email));
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subj;
            message.Body = msg;
            var client = new SmtpClient();
            client.Send(message);
        }


        public static void SendEmail(string email, string subj, string msg, Encoding enc)
        {
            var message = new MailMessage();
            //message.From = new System.Net.Mail.MailAddress("save@quarinos.com");
            message.To.Add(new MailAddress(email));
            message.IsBodyHtml = true;
            message.BodyEncoding = enc;
            message.Subject = subj;
            message.Body = msg;
            var client = new SmtpClient();
            client.SendAsync(message, null);
        }

        public static void SendEmail(string to, string email, string subj, string msg)
        {
            var message = new MailMessage {From = new MailAddress(to)};
            message.To.Add(new MailAddress(email));
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subj;
            message.Body = msg;
            var client = new SmtpClient();
            client.SendAsync(message, null);
        }
    }
}