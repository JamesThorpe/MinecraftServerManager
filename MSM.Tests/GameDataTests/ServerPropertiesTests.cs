using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSM.Core.GameData;

namespace MSM.Tests.GameDataTests
{
    [TestClass]
    public class ServerPropertiesTests
    {
        private const string TestFile = @"
#comment on one line
allow-flight=true
allow-nether=false
difficulty=3#comment on line no space
enable-query=true
enable-rcon=false
enable-command-block=true
force-gamemode=false
gamemode=2 #comment on line with space
generate-structures=false
generator-settings=abc
hardcore=true
level-name=my test name
level-seed=xyz123
level-type=LARGEBIOMES
max-build-height=123
max-players=5
max-tick-time=5678

max-world-size=3456#
motd=A test file
network-compression-threshold=55
  #comment indented
online-mode=false
op-permission-level=2

player-idle-timeout=42
prevent-proxy-connections=true
pvp=false
#pvp=true
query.port=3
rcon.password=secr3t
rcon.port=456
resource-pack=none
resource-pack-sha1=sasdf3453xx
server-ip=localhost
server-port=888
snooper-enabled=false
spawn-animals=false
spawn-monsters=false
spawn-npcs=false
spawn-protection=32
use-native-transport=false
view-distance=17
white-list=true
";

        private const string NonDefaultTestFile = @"allow-flight=true
difficulty=2
generator-settings=abc
max-build-height=5
";

        [TestMethod]
        public void DoesItParseAFileCorrectly()
        {
            var sp = ServerProperties.FromFileFormat(TestFile);
            Assert.AreEqual(true, sp.AllowFlight);
            Assert.AreEqual(false, sp.AllowNether);
            Assert.AreEqual(GameDifficulty.Hard, sp.Difficulty);
            Assert.AreEqual(true, sp.EnableQuery);
            Assert.AreEqual(false, sp.EnableRcon);
            Assert.AreEqual(true, sp.EnableCommandBlock);
            Assert.AreEqual(false, sp.ForceGamemode);
            Assert.AreEqual(GameMode.Adventure, sp.GameMode);
            Assert.AreEqual(false, sp.GenerateStructures);
            Assert.AreEqual("abc", sp.GeneratorSettings);
            Assert.AreEqual(true, sp.IsHardcore);
            Assert.AreEqual("my test name", sp.LevelName);
            Assert.AreEqual("xyz123", sp.LevelSeed);
            Assert.AreEqual("LARGEBIOMES", sp.LevelType);
            Assert.AreEqual(123, sp.MaxBuildHeight);
            Assert.AreEqual(5, sp.MaxPlayers);
            Assert.AreEqual(5678, sp.MaxTickTime);
            Assert.AreEqual(3456, sp.MaxWorldSize);
            Assert.AreEqual("A test file", sp.MessageOfTheDay);
            Assert.AreEqual(55, sp.NetworkCompressionThreshold);
            Assert.AreEqual(false, sp.OnlineMode);
            Assert.AreEqual(PermissionLevel.SinglePlayerCheats, sp.OpPermissionLevel);
            Assert.AreEqual(42, sp.PlayerIdleTimeout);
            Assert.AreEqual(true, sp.PreventProxyConnections);
            Assert.AreEqual(false, sp.Pvp);
            Assert.AreEqual(3, sp.QueryPort);
            Assert.AreEqual("secr3t", sp.RconPassword);
            Assert.AreEqual(456, sp.RconPort);
            Assert.AreEqual("none", sp.ResourcePack);
            Assert.AreEqual("sasdf3453xx", sp.ResourcePackDigest);
            Assert.AreEqual("localhost", sp.ServerIp);
            Assert.AreEqual(888, sp.ServerPort);
            Assert.AreEqual(false, sp.SnooperEnabled);
            Assert.AreEqual(false, sp.SpawnAnimals);
            Assert.AreEqual(false, sp.SpawnMonsters);
            Assert.AreEqual(false, sp.SpawnNpcs);
            Assert.AreEqual(32, sp.SpawnProtectionRadius);
            Assert.AreEqual(false, sp.UseNativeTransport);
            Assert.AreEqual(17, sp.ViewDistance);
            Assert.AreEqual(true, sp.WhiteList);
        }

        [TestMethod, ExpectedException(typeof(FileParseException))]
        public void DoesNonIntThrowError()
        {
            ServerProperties.FromFileFormat("max-players=bob");
        }

        [TestMethod, ExpectedException(typeof(FileParseException))]
        public void DoesNonBoolThrowError()
        {
            ServerProperties.FromFileFormat("pvp=hello");
        }

        [TestMethod, ExpectedException(typeof(FileParseException))]
        public void DoesUnknownPropertyThrowError()
        {
            ServerProperties.FromFileFormat("cool-features-enabled=false");
        }


        [TestMethod]
        public void DoesLeavingSettingsOnDefaultGenerateBlankFile()
        {
            var sp = new ServerProperties();
            var f = sp.ToFileFormat();
            Assert.AreEqual("", f);
        }

        [TestMethod]
        public void DoesItWriteAFileCorrectly()
        {
            var sp = new ServerProperties {
                AllowFlight = true,
                Difficulty = GameDifficulty.Normal,
                GeneratorSettings = "abc",
                MaxBuildHeight = 5
            };
            Assert.AreEqual(NonDefaultTestFile, sp.ToFileFormat());
        }
    }
}
