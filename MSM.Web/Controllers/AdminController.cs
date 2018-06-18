using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSM.Core;
using MSM.Core.Config;
using MSM.Core.GameData;
using MSM.Web.Models.Admin;

namespace MSM.Web.Controllers {
    [Authorize(Roles="admin")]
    public class AdminController : Controller {
        private readonly IUserRepository _userRepository;
        private readonly IMetadataProvider _metadataProvider;

        public AdminController(IUserRepository userRepository, IMetadataProvider metadataProvider)
        {
            _userRepository = userRepository;
            _metadataProvider = metadataProvider;
        }

        public IActionResult Users()
        {
            var users = _userRepository.RetrieveUsers();
            return View(users);
        }

        public IActionResult FindUser()
        {
            return View(new FindUser());
        }

        [HttpPost]
        public async Task<IActionResult> FindUser(FindUser findUser)
        {
            var user = await _metadataProvider.FindMojangUser(findUser.Username);
            findUser.FoundUser = user;
            return View(findUser);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(FindUser findUser)
        {
            var u = new User {
                Id = findUser.FoundUser.Id,
                Name = findUser.FoundUser.Name,
                IsServerAdmin = false
            };
            _userRepository.AddUser(u);
            return RedirectToAction("Users");
        }

        [HttpGet]
        [Route("[controller]/[action]/{userId}")]
        public IActionResult EditUser(string userId)
        {
            return PartialView(_userRepository.RetrieveUser(userId));
        }

        [HttpPost]
        [Route("[controller]/[action]/{userId}")]
        public IActionResult EditUser(User user)
        {
            _userRepository.UpdateUser(user);
            return RedirectToAction("Users");
        }
    }
}