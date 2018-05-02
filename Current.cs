using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MinecraftServerManager.Data;
using MinecraftServerManager.Utility;

namespace MinecraftServerManager
{
    public class Current : ICurrent
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Current(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public MojangAuthInfo User => _contextAccessor.HttpContext.Session.Get<MojangAuthInfo>(MojangAuthInfo.SessionKey);
    }
}
