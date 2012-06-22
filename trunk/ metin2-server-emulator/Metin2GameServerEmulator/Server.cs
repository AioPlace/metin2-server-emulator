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
using System.Reflection;
using Metin2GameServerEmulator.LocalNetwork;
using Metin2GameServerEmulator.Network;
using Metin2ServerEmulatorCommon;

namespace Metin2GameServerEmulator
{
    /// <summary>
    ///   Represent a Metin2 Game Server.
    /// </summary>
    internal class Server
    {
        private static Server _server;

        private readonly Database _database;
        private readonly Logger _log;
        private readonly LoginServerConnection _login;
        private readonly NetworkService _netService;
        private bool _isRunning;

        /// <summary>
        ///   Initialize a instance of Server.
        /// </summary>
        /// <param name="log"> Logger instance. </param>
        /// <param name="port"> Port to listen on. </param>
        internal Server(Logger log, int port = 9993)
        {
            _server = this;
            _log = log;
            _database = new Database(Config.DbHost, Config.DbUser, Config.DbPass, Config.DbName, _log);
            _login = new LoginServerConnection(Config.LoginServerAddress, Config.LocalNetPort, Config.LocalNetPassowrd);
            _netService = new NetworkService(log, port);

            Console.Clear();
            Console.Title = string.Format("Metin2 Game Server Emulator [Version {0}]",
                                          Assembly.GetExecutingAssembly().GetName().Version);

            Console.WriteLine("Metin2 Game Server Emulator [Version {0}]\n",
                              Assembly.GetExecutingAssembly().GetName().Version);
        }

        /// <summary>
        ///   Gets the instance of this server.
        /// </summary>
        internal static Server ServerInstance
        {
            get { return _server; }
        }


        /// <summary>
        ///   Gets the instance of Logger used by this server.
        /// </summary>
        internal Logger Logger
        {
            get { return _log; }
        }

        /// <summary>
        ///   Gets the instance of Database used by this server.
        /// </summary>
        internal Database Database
        {
            get { return _database; }
        }


        internal LoginServerConnection LoginServerConnection
        {
            get { return _login; }
        }


        /// <summary>
        ///   Starts the server.
        /// </summary>
        internal void Start()
        {
            _isRunning = true;
            _log.Info("Connecting to Database");
            if (!_database.Connect())
            {
                Stop();
                return;
            }
            _log.Info("Connecting to LoginServer");
            if (!_login.Connect())
            {
                Stop();
                return;
            }

            _netService.Start();

            Listen(); //Keep this last! - Listens for console commands
        }

        /// <summary>
        ///   Stops The server.
        /// </summary>
        internal void Stop()
        {
            //TODO Save and stop
            _log.Info("Stopping server");
            _isRunning = false;
            _database.Disonnect();
            _netService.Stop();
            _login.Disconnect();
            _log.Stop();
        }

        /// <summary>
        ///   Application main Cycle, listens for console commands.
        /// </summary>
        private void Listen()
        {
            while (_isRunning)
            {
                string readLine = Console.ReadLine();
                if (readLine != null)
                {
                    string[] command = readLine.ToLower().Split(' ');
                    switch (command[0])
                    {
                        case "stop":
                        case "exit":
                            Stop();
                            return;
                        case "help":
                            _log.System("Available Commands:");
                            _log.System("stop/exit");
                            _log.System("clear");
                            _log.System("help");
                            break;
                        case "clear":
                            Console.Clear();
                            break;
                        default:
                            _log.System("Unknown Command. Type help to get a list of commands");
                            break;
                    }
                }
            }
        }
    }
}