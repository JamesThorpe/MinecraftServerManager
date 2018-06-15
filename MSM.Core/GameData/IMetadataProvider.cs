using System.Threading.Tasks;

namespace MSM.Core.GameData
{
    public interface IMetadataProvider
    {
        Task<MojangUserData> FindMojangUser(string username);
        Task<VersionManifest> GetManifestAsync(bool refresh = false);
    }
}