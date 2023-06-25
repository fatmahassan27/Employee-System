using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class signalR : Controller
    {
        public IActionResult TestSignalR()
        {
            return View();
        }
    }
}
