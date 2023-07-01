using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Demo.BL.ModelVM;

namespace Demo.BL.Helper
{
    public class MailSender
    {
        public static string SenderMail(MailVM mail)
        {
            try
            {
                var smtp = new SmtpClient("smtp.office365.com", 587 );
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Timeout = 100000;
                smtp.Credentials = new NetworkCredential("fatmahassan1951@outlook.com", "FATMAHASSAN2772000","fatma");
                MailMessage mailMessage = new MailMessage( "fatmahassan1951@outlook.com",mail.Email );
                mailMessage.Subject= mail.Email;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = $"<h3>{mail.Message}</h3>";
                smtp.Send(mailMessage);

                return "Succed";

            }
            catch 
            {             
                return "Faild";
            }
        }
    }
}
