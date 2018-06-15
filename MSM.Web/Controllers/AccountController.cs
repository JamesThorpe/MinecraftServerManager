using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MSM.Core;
using MSM.Core.Authentication;
using MSM.Core.Config;
using MSM.Web.Models.Authentication;

namespace MSM.Web.Controllers {
    public class AccountController : Controller {
        private readonly IMojangAuthenticator _authProvider;
        private readonly IUserRepository _userRepository;

        public AccountController(IMojangAuthenticator authProvider, IUserRepository userRepository)
        {
            _authProvider = authProvider;
            _userRepository = userRepository;
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
                if (user == null) {
                    ModelState.AddModelError("", "Invalid username / password");
                    return View(credentials);
                }
                var u = _userRepository.RetrieveUser(user.SelectedProfile.Id);

                if (u == null) {
                    if (_userRepository.CountUsers() > 0) {
                        ModelState.AddModelError("", "Your user is unknown to this instance of Minecraft Server Manager.  Please contact the administrator.");
                        return View(credentials);
                    } else {
                        //First login to this server, auto add this user and make them admin
                        u = new User {
                            Id = user.SelectedProfile.Id,
                            Name = user.SelectedProfile.Name,
                            IsServerAdmin = true
                        };
                        _userRepository.AddUser(u);
                    }
                }

                const string issuer = "MinecraftServerManager";

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, u.Name, ClaimValueTypes.String, issuer),
                    new Claim("MojangId", u.Id, ClaimValueTypes.String, issuer),
                    new Claim(ClaimTypes.Role, u.IsServerAdmin ? "admin" : "user")
                };

                var identity = new ClaimsIdentity(claims, "MsmUser");
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