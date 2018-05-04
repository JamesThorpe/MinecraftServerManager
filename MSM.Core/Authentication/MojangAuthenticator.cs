using System.Net.Http;
using System.Threading.Tasks;
using MSM.Data;
using Newtonsoft.Json;

namespace MSM.Core.Authentication {
    public class MojangAuthenticator : IMojangAuthenticator
    {
        public async Task<MojangAuthInfo> AuthenticateAsync(string username, string password)
        {
            using (var client = new HttpClient()) {
                var authRequest = new MojangAuthenticationRequest {
                    Username = username,
                    Password = password,
                    ClientToken = "EEE0B0F8-92A9-4ABF-A54A-9E0B866F7B7A"
                };
                var result = await client.PostAsJsonAsync("https://authserver.mojang.com/authenticate", authRequest);
                if (result.IsSuccessStatusCode) {
                    return JsonConvert.DeserializeObject<MojangAuthInfo>(await result.Content.ReadAsStringAsync());
                } else {
                    return null;
                }
            }
        }
    }
}