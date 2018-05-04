using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MSM.Web.Components
{
    public class UserViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated) {
                return Task.FromResult<IViewComponentResult>(View("LoggedIn", User));
            } else {
                return Task.FromResult<IViewComponentResult>(View());
            }
        }
    }
}