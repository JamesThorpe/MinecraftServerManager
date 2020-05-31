using MSM.Core.GameData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace MSM.Core
{
    public class MinecraftServer
    {
        public ServerProperties Properties { get; set; }

        public ServerState State { get; }

        private Process _process;

        public MinecraftServer()
        {
            Properties = new ServerProperties();
            State = ServerState.Stopped;
        }


        public void StartServer()
        {
            if (State != ServerState.Stopped)
            {
                return;
            }
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files\Java\jre1.8.0_251\bin\java.exe";
            psi.Arguments = "-Xmx1536M -jar server.jar nogui";
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            _process = new Process();
            _process.StartInfo = psi;
            _process.OutputDataReceived += (s, e) =>
            {
                
            };
            _process.ErrorDataReceived += (s, e) =>
            {
                
            };
            _process.Exited += (s, e) =>
            {

            };
            var p = _process.Start();
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();
        }
    }
}
