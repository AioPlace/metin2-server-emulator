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
using Metin2AuthServerEmulator.LocalNetwork;
using Metin2AuthServerEmulator.Network;
using Metin2ServerEmulatorCommon;

namespace Metin2AuthServerEmulator
{
    internal class Server
    {
        private static Server _server;

        private readonly Database _database;
        private readonly LocalNetworkService _localNetService;
        private readonly Logger _log;
        private readonly NetworkService _netService;
        private bool _isRunning;

        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name="log"> Logger instance </param>
        /// <param name="port"> Port to listen on </param>
        internal Server(Logger log, int port = 9993)
        {
            _server = this;
            _log = log;
            _database = new Database(Config.DbHost, Config.DbUser, Config.DbPass, Config.DbName, _log);
            _localNetService = new LocalNetworkService(log, Config.LocalNetPort);
            _netService = new NetworkService(log, port);

            Console.Clear();
            Console.Title = string.Format("Metin2 Auth Server Emulator [Version {0}]",
                                          Assembly.GetExecutingAssembly().GetName().Version);

            Console.WriteLine("Metin2 Auth Server Emulator [Version {0}]\n",
                              Assembly.GetExecutingAssembly().GetName().Version);
        }

        internal Logger Logger
        {
            get { return _log; }
        }

        internal Database Database
        {
            get { return _database; }
        }

        internal LocalNetworkService LocalNetService
        {
            get { return _localNetService; }
        }

        internal static Server ServerInstance
        {
            get { return _server; }
        }


        /// <summary>
        ///   Starts the server
        /// </summary>
        internal void Start()
        {
            _isRunning = true;

            if (!_database.Connect())
            {
                Stop();
                return;
            }

            _localNetService.Start();
            _netService.Start();

            _log.Info("Server Running");

            Listen(); //Keep this last! - Listens for console commands
        }

        /// <summary>
        ///   Stops The server
        /// </summary>
        internal void Stop()
        {
            _isRunning = false;
            _database.Disonnect();
            _netService.Stop();
            _localNetService.Stop();
            _log.Stop();
        }

        /// <summary>
        ///   Application main Cycle, listens for console commands
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