using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MinecraftServerManager.Components
{
    public class UserViewComponent:ViewComponent
    {
        private readonly ICurrent _current;

        public UserViewComponent(ICurrent current)
        {
            _current = current;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_current.User);
        }
    }
}
