using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MSM.Core.GameData {
    public class VersionManifest
    {
        public class LatestInfo
        {
            [JsonProperty("release")]
            public string Release { get; set; }

            [JsonProperty("snapshot")]
            public string Snapshot { get; set; }
        }

        [JsonProperty("latest")]
        public LatestInfo Latest { get; set; }

        public List<MinecraftVersion> Versions { get; set; }

        [JsonIgnore]
        public MinecraftVersion LatestRelease => Versions.Single(v => v.Id == Latest.Release);

    }
}