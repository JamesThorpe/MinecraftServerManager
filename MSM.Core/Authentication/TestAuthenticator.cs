using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSM.Data;

namespace MSM.Core.Authentication {
    class TestAuthenticator : IMojangAuthenticator
    {
        public Task<MojangAuthInfo> AuthenticateAsync(string username, string password)
        {
            var p = new MojangAuthInfo.Profile {
                Id = GetId(),
                Name = username
            };
            return Task.FromResult(new MojangAuthInfo {
                AccessToken = GetId(),
                ClientToken = GetId(),
                AvailableProfiles = new List<MojangAuthInfo.Profile>{p},
                SelectedProfile = p,
                User = new MojangAuthInfo.UserInfo {
                    Id = GetId()
                }
            });
        }

        private static string GetId() => Guid.NewGuid().ToString().ToLowerInvariant().Replace("-", "");
    }
}