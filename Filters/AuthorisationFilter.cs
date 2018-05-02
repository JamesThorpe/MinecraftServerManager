using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinecraftServerManager.Data;
using MinecraftServerManager.Utility;

namespace MinecraftServerManager.Filters
{
    public class AuthorisationFilter:IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auth = ((Controller) context.Controller).HttpContext.Session.Get<MojangAuthInfo>(MojangAuthInfo.SessionKey);
            if (auth == null) {
                context.Result = new UnauthorizedResult();
            } else {
                await next();
            }
        }
    }
}
