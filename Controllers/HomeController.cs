using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinecraftServerManager.Filters;
using MinecraftServerManager.Utility;

namespace MinecraftServerManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrent _current;

        public HomeController(ICurrent current)
        {
            _current = current;
        }
        public IActionResult Index()
        {
            var u = _current.User;
            return View();
        }
    }
}