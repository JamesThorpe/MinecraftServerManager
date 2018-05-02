using Microsoft.AspNetCore.Mvc;

namespace MinecraftServerManager.Controllers {
    public class AuthenticationController : Controller {
        public IActionResult LogOn()
        {
            return View();
        }
    }
}