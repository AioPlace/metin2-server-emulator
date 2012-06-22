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

namespace Metin2GameServerEmulator
{
    /// <summary>
    ///   Represent the entire set of configuration for the server.
    /// </summary>
    internal static class Config
    {
#pragma warning disable 612,618
        /// <summary>
        ///   Gets the base directory that the assembly resolver uses to prob for assemblies.
        /// </summary>
        internal static readonly string AppDir = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        ///   Gets the directory of the log-folder.
        /// </summary>
        internal static readonly string LogFolder = AppDir + ConfigurationSettings.AppSettings["logFolder"];

        /// <summary>
        ///   Gets the port used for the connection with the clients.
        /// </summary>
        internal static readonly int Port = Convert.ToInt32(ConfigurationSettings.AppSettings["port"]);

        /// <summary>
        ///   Gets the pong used to encrypt data.
        /// </summary>
        internal static readonly byte[] Pong =
            ByteSupport.HexStringToByteArray(ConfigurationSettings.AppSettings["pong"]);

        /// <summary>
        ///   Gets the Database's hostname.
        /// </summary>
        internal static readonly string DbHost = ConfigurationSettings.AppSettings["dbHost"];

        /// <summary>
        ///   Gets the Database name.
        /// </summary>
        internal static readonly string DbName = ConfigurationSettings.AppSettings["dbName"];

        /// <summary>
        ///   Gets the username used to access to the Database.
        /// </summary>
        internal static readonly string DbUser = ConfigurationSettings.AppSettings["dbUser"];

        /// <summary>
        ///   Gets the password used to access to the Database.
        /// </summary>
        internal static readonly string DbPass = ConfigurationSettings.AppSettings["dbPass"];

        /// <summary>
        ///   Gets the Authenticator Server's IP address.
        /// </summary>
        internal static readonly string LoginServerAddress = ConfigurationSettings.AppSettings["loginServerAddress"];

        /// <summary>
        ///   Gets the port used to communicate with the Authenticator Server.
        /// </summary>
        internal static readonly int LocalNetPort = Convert.ToInt32(ConfigurationSettings.AppSettings["localNetPort"]);

        /// <summary>
        ///   Gets the password used to communicate with the Authenticator Server.
        /// </summary>
        internal static readonly string LocalNetPassowrd = ConfigurationSettings.AppSettings["localNetPassword"];
#pragma warning restore 612,618
    }
}