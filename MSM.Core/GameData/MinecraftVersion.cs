using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MSM.Core.GameData
{
    public class MinecraftVersion
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonIgnore]
        public bool IsRelease => Type == "release";

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("releaseTime")]
        public DateTime ReleaseTime { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}