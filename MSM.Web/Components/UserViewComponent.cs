using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSM.Core;

namespace MSM.Web.Components
{
    public class UserViewComponent : ViewComponent
    {

        public Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext.GetUser();
            if (user != null) {
                return Task.FromResult<IViewComponentResult>(View("LoggedIn", user));
            } else {
                return Task.FromResult<IViewComponentResult>(View());
            }
        }
    }
}