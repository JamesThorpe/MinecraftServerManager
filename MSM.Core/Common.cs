using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSM.Core
{
    public static class Common
    {
        public static string BaseDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MinecraftServerManager");
    }
}
