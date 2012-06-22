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
using System.Configuration;
using Metin2ServerEmulatorCommon.Util;

namespace Metin2AuthServerEmulator
{
    internal static class Config
    {
#pragma warning disable 612,618
        internal static readonly string AppDir = AppDomain.CurrentDomain.BaseDirectory;
        internal static readonly string LogFolder = AppDir + ConfigurationSettings.AppSettings["logFolder"];
        internal static readonly int Port = Convert.ToInt32(ConfigurationSettings.AppSettings["port"]);

        internal static readonly byte[] Pong =
            ByteSupport.HexStringToByteArray(ConfigurationSettings.AppSettings["pong"]);

        internal static readonly string DbHost = ConfigurationSettings.AppSettings["dbHost"];
        internal static readonly string DbName = ConfigurationSettings.AppSettings["dbName"];
        internal static readonly string DbUser = ConfigurationSettings.AppSettings["dbUser"];
        internal static readonly string DbPass = ConfigurationSettings.AppSettings["dbPass"];

        internal static readonly int LocalNetPort = Convert.ToInt32(ConfigurationSettings.AppSettings["localNetPort"]);
        internal static readonly string LocalNetPassowrd = ConfigurationSettings.AppSettings["localNetPassword"];
#pragma warning restore 612,618
    }
}