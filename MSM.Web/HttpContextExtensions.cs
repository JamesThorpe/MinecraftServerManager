using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MSM.Core;
using MSM.Core.Config;

namespace MSM.Web
{
    public static class HttpContextExtensions
    {
        public static User GetUser(this HttpContext httpContext)
        {
            if (httpContext.User == null || !httpContext.User.Identity.IsAuthenticated) {
                return null;
            }

            var userRepo = (IUserRepository)httpContext.RequestServices.GetService(typeof(IUserRepository));
            return userRepo.RetrieveUser(httpContext.User.Claims.First(c => c.Type == "MojangId").Value);
        }
    }
}
