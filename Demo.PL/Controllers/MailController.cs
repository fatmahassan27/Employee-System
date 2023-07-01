using Demo.BL.Helper;
using Demo.BL.ModelVM;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;



namespace Demo.PL.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendMail(MailVM mail)
        {
            try
            {
                if (ModelState.IsValid)
                { 
                   
                    TempData["Msg"] = MailSender.SenderMail(mail);
                    return RedirectToAction("SendMail");
                }
                else
                    return View();
            }
            catch (Exception ex) 
            {
                TempData["Msg"] = "Faild";
                return View();
            }
        }
    }
}
