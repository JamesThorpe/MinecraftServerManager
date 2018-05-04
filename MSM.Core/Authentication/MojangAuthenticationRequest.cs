using Newtonsoft.Json;

namespace MSM.Core.Authentication {
    public class MojangAuthenticationRequest
    {
        public class AgentType
        {
            [JsonProperty("name")]
            public string Name => "Minecraft";

            [JsonProperty("version")]
            public int Version => 1;
        }

        [JsonProperty("agent")]
        public AgentType Agent => new AgentType();

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }

        [JsonProperty("requestUser")]
        public bool RequestUser => true;

    }
}