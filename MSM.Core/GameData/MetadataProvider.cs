using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MSM.Core.GameData
{
    public class MetadataProvider
    {
        private static string ManifestPath => Path.Combine(Common.BaseDirectory, "metadata", "version_manifest.json");

        public async Task<VersionManifest> GetManifestAsync(bool refresh = false)
        {
            if (!File.Exists(ManifestPath) || refresh) {
                await GetLatestManifest();
            }

            string manifest;
            using (var reader = new StreamReader(ManifestPath)) {
                manifest = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<VersionManifest>(manifest);
        }


        private async Task GetLatestManifest()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ManifestPath));
            using (var client = new HttpClient())
            using (var response = await client.GetAsync("https://launchermeta.mojang.com/mc/game/version_manifest.json"))
            using (var output = new FileStream(ManifestPath, FileMode.Create)) {
                var responseStream = await response.Content.ReadAsStreamAsync();
                await responseStream.CopyToAsync(output);
            }
        }
    }
}
