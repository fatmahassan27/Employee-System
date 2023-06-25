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
                var smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("fatma3499@gmail.com", "2772000fatma");
                    smtp.Send("fatma3499@gmail.com", "fatma3499@gmail.com", mail.Title, mail.Message);

                return "Succed";

            }
            catch 
            {             
                return "faild";
            }
        }
    }
}
