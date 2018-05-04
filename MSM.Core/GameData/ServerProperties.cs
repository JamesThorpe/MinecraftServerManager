using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MSM.Core.GameData
{
    // Server properties and defaults are documented here: https://minecraft.gamepedia.com/Server.properties
    public class ServerProperties
    {
        [ServerProperty("allow-flight")]
        public bool AllowFlight { get; set; } = false;

        [ServerProperty("allow-nether")]
        public bool AllowNether { get; set; } = true;

        [ServerProperty("difficulty", isCommon:true)]
        public GameDifficulty Difficulty { get; set; } = GameDifficulty.Easy;

        [ServerProperty("enable-query")]
        public bool EnableQuery { get; set; } = false;

        [ServerProperty("enable-rcon")]
        public bool EnableRcon { get; set; } = false;

        [ServerProperty("enable-command-block")]
        public bool EnableCommandBlock { get; set; } = false;

        [ServerProperty("force-gamemode")]
        public bool ForceGamemode { get; set; } = false;

        [ServerProperty("gamemode", isCommon:true)]
        public GameMode GameMode { get; set; } = GameMode.Survival;

        [ServerProperty("generate-structures",isCommon:true)]
        public bool GenerateStructures { get; set; } = true;

        [ServerProperty("generator-settings")]
        public string GeneratorSettings { get; set; } = "";

        [ServerProperty("hardcore")]
        public bool IsHardcore { get; set; } = false;

        [ServerProperty("level-name",isCommon:true)]
        public string LevelName { get; set; } = "world";

        [ServerProperty("level-seed",isCommon:true)]
        public string LevelSeed { get; set; } = "";

        [ServerProperty("level-type",isCommon:true)]
        public string LevelType { get; set; } = "DEFAULT"; //TODO: add choices - DEFAULT, FLAT, LARGEBIOMES, AMPLIFIED, CUSTOMIZED

        [ServerProperty("max-build-height")]
        public int MaxBuildHeight { get; set; } = 256;

        [ServerProperty("max-players")]
        public int MaxPlayers { get; set; } = 20;

        [ServerProperty("max-tick-time")]
        public int MaxTickTime { get; set; } = 60000;

        [ServerProperty("max-world-size")]
        public int MaxWorldSize { get; set; } = 29999984;

        [ServerProperty("motd",isCommon:true)]
        public string MessageOfTheDay { get; set; } = "A Minecraft Server"; //TODO: add support for colors/formatting

        [ServerProperty("network-compression-threshold")]
        public int NetworkCompressionThreshold { get; set; } = 256;

        [ServerProperty("online-mode")]
        public bool OnlineMode { get; set; } = true;

        [ServerProperty("op-permission-level")]
        public PermissionLevel OpPermissionLevel { get; set; } = PermissionLevel.AllCommands;

        [ServerProperty("player-idle-timeout")]
        public int PlayerIdleTimeout { get; set; } = 0;

        [ServerProperty("prevent-proxy-connections")]
        public bool PreventProxyConnections { get; set; } = false;

        [ServerProperty("pvp",isCommon:true)]
        public bool Pvp { get; set; } = true;

        [ServerProperty("query.port",isManaged:true)]
        public int QueryPort { get; set; } = 25565;

        [ServerProperty("rcon.password",isManaged:true)]
        public string RconPassword { get; set; } = "";

        [ServerProperty("rcon.port",isManaged:true)]
        public int RconPort { get; set; } = 25575;

        [ServerProperty("resource-pack")]
        public string ResourcePack { get; set; } = "";

        [ServerProperty("resource-pack-sha1")]
        public string ResourcePackDigest { get; set; } = "";

        [ServerProperty("server-ip",isManaged:true)]
        public string ServerIp { get; set; } = "";

        [ServerProperty("server-port",isManaged:true)]
        public int ServerPort { get; set; } = 25565;

        [ServerProperty("snooper-enabled")]
        public bool SnooperEnabled { get; set; } = true;

        [ServerProperty("spawn-animals",isCommon:true)]
        public bool SpawnAnimals { get; set; } = true;

        [ServerProperty("spawn-monsters",isCommon:true)]
        public bool SpawnMonsters { get; set; } = true;

        [ServerProperty("spawn-npcs",isCommon:true)]
        public bool SpawnNpcs { get; set; } = true;

        [ServerProperty("spawn-protection")]
        public int SpawnProtectionRadius { get; set; } = 16;

        [ServerProperty("use-native-transport")]
        public bool UseNativeTransport { get; set; } = true;

        [ServerProperty("view-distance")]
        public int ViewDistance { get; set; } = 10; //TODO: limit 2-32

        [ServerProperty("white-list",isCommon:true)]
        public bool WhiteList { get; set; } = false;


        public static ServerProperties FromFileFormat(string fileSettings)
        {
            var props = new ServerProperties();
            using (var tr = new StringReader(fileSettings)) {
                string line;
                while ((line = tr.ReadLine()) != null) {
                    var i = line.IndexOf('#');
                    if (i >= 0) {
                        line = line.Substring(0, i);
                    }
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    
                    i = line.IndexOf('=');
                    if (i > 0 && i < line.Length-1) {
                        var propName = line.Substring(0, i);
                        var value = line.Substring(i + 1);


                        var property = typeof(ServerProperties).GetProperties().SingleOrDefault(p => p.GetCustomAttributes<ServerPropertyAttribute>().SingleOrDefault()?.Name == propName);
                        if (property != null) {
                            if (property.PropertyType.IsEnum) {
                                property.SetValue(props,Enum.Parse(property.PropertyType, value));
                            } else if (property.PropertyType == typeof(int)) {
                                if (int.TryParse(value, out var ival)) {
                                    property.SetValue(props, ival);
                                } else {
                                    //error parsing
                                }
                            } else if (property.PropertyType == typeof(bool)) {
                                if (bool.TryParse(value, out var bval)) {
                                    property.SetValue(props, bval);
                                } else {
                                    //error parsing
                                }
                            } else {
                                property.SetValue(props, value);
                            }
                        } else {
                            //unknown server property?
                        }

                    }
                }
            }

            return props;
        }

        public string ToFileFormat()
        {
            var defaults = new ServerProperties();
            var sb = new StringBuilder();
            foreach (var p in typeof(ServerProperties).GetProperties()) {
                var info = (ServerPropertyAttribute)p.GetCustomAttributes(typeof(ServerPropertyAttribute), true).SingleOrDefault();
                if (info != null) {

                    var val = p.GetValue(this);
                    if (!val.Equals(p.GetValue(defaults))) {
                        sb.Append(info.Name);
                        sb.Append("=");
                        if (p.PropertyType.IsEnum) {
                            sb.AppendLine(((int) val).ToString());
                        } else if (p.PropertyType == typeof(bool)) {
                            sb.AppendLine((bool) val ? "true" : "false");
                        } else {
                            sb.AppendLine(val.ToString());
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
