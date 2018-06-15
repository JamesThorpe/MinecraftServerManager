using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSM.Core.Authentication
{
    public interface IMojangAuthenticator
    {
        Task<MojangAuthInfo> AuthenticateAsync(string username, string password);
    }
}
