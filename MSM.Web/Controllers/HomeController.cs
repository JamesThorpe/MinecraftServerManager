using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSM.Core;
using MSM.Core.GameData;

namespace MSM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMetadataProvider _metadataProvider;

        public HomeController(ILogger<HomeController> logger, IMetadataProvider metadataProvider)
        {
            _logger = logger;
            _metadataProvider = metadataProvider;
        }

        public IActionResult Index()
        {
            var s = new MinecraftServer();
            s.StartServer();
            return View();
        }

        [Route("AvailableVersions/{a?}/{b?}")]
        public async Task<IActionResult> AvailableVersions(string a, string b)
        {
            var includeSnapshots = (a == "snapshots" || b == "snapshots");
            var refresh = (a == "refresh" || b == "refresh");
            
            var versions = await _metadataProvider.GetManifestAsync(refresh);

            return View(includeSnapshots ? versions.Versions : versions.Versions.Where(v => v.IsRelease));
        }
    }
}