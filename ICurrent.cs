using MinecraftServerManager.Data;

namespace MinecraftServerManager
{
    public interface ICurrent
    {
        MojangAuthInfo User { get; }
    }
}