using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MSM.Core.Authentication;
using MSM.Web.Models.Authentication;

namespace MSM.Web.Controllers {
    public class AccountController : Controller {
        private readonly IMojangAuthenticator _authProvider;

        public AccountController(IMojangAuthenticator authProvider)
        {
            _authProvider = authProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new Credentials());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials, string returnUrl = null)
        {
            if (ModelState.IsValid) {
                var user = await _authProvider.AuthenticateAsync(credentials.Username, credentials.Password);
                const string issuer = "https://authserver.mojang.com";

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.SelectedProfile.Name, ClaimValueTypes.String, issuer),
                    new Claim("MojangId", user.SelectedProfile.Id, ClaimValueTypes.String, issuer)
                };

                var identity = new ClaimsIdentity(claims, "MojangUser");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties {
                    AllowRefresh = false,
                    IsPersistent = true
                });
                return Redirect(returnUrl ?? "/");
            } else {
                return View(credentials);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}