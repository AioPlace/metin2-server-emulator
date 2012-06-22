#region License

//     This file is part of Metin 2 Server Emulator.
// 
//     Metin 2 Server Emulator is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     Metin 2 Server Emulator is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with Metin 2 Server Emulator.  If not, see <http://www.gnu.org/licenses/>

#endregion

using System;
using Metin2ServerEmulatorCommon;

namespace Metin2GameServerEmulator
{
    internal static class Program
    {
        // ReSharper disable InconsistentNaming
        private static Server sv;
        // ReSharper restore InconsistentNaming

        private static void Main()
        {
            Console.TreatControlCAsInput = false;
            Console.CancelKeyPress += CtrlC;

            Logger log = new Logger(Config.LogFolder);

            sv = new Server(log, Config.Port);
            sv.Start();

            Console.Title = "";
        }

        private static void CtrlC(object sender, ConsoleCancelEventArgs args)
        {
            sv.Stop();
        }
    }
}