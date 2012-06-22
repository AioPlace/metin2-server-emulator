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
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Metin2ServerEmulatorCommon;
using Metin2ServerEmulatorCommon.LocalNetwork;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets.Common;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets.Game;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets.Login;

namespace Metin2GameServerEmulator.LocalNetwork
{
    /// <summary>
    ///   An interface to communicate with the Authenticator Server.
    /// </summary>
    internal class LoginServerConnection
    {
        private readonly TcpClient _client;
        private readonly string _ip;
        private readonly Thread _listenerThread;

        private readonly Logger _log = Server.ServerInstance.Logger;
        private readonly string _password;
        private readonly int _port;
        private IFormatter _serializer;
        private NetworkStream _stream;

        /// <summary>
        ///   Initialize a instance of LoginServerConnection.
        /// </summary>
        /// <param name="ip"> The IP address of the authenticator server. </param>
        /// <param name="port"> The port used to communicate with the authenticator server. </param>
        /// <param name="password"> The password used to communicate with the authenticator server. </param>
        internal LoginServerConnection(string ip, int port, string password)
        {
            _ip = ip;
            _port = port;
            _password = password;
            _client = new TcpClient();

            IsConnected = false;

            //_listenerThread = new Thread(Listen) {Name = "LoginServer Listener: " + _ip};
        }

        /// <summary>
        ///   Returns true if the Authenticator Server is connected.
        /// </summary>
        internal bool IsConnected { get; private set; }

        /// <summary>
        ///   Connect to the authenticator server.
        /// </summary>
        /// <returns> If the connection is successful, returns true. Otherwise false. </returns>
        internal bool Connect()
        {
            try
            {
                _client.Connect(_ip, _port);
                _stream = _client.GetStream();
                _serializer = new BinaryFormatter();
                SendPacket(new PasswordPacket(_password));
                ReadPacket();

                //_listenerThread.Start();
            }
            catch (Exception e)
            {
                _log.Critical("Error while connecting to login server: " + e);
                IsConnected = false;
            }
            return IsConnected;
        }

        /// <summary>
        ///   Waits for a packet.
        /// </summary>
        private void Listen()
        {
            while (IsConnected)
            {
                ReadPacket();
            }
        }

        /// <summary>
        ///   Reads packets from the client.
        /// </summary>
        private void ReadPacket()
        {
            try
            {
                ProcessPacket((IPacket) _serializer.Deserialize(_stream));
            }
            catch (Exception e)
            {
                _log.Critical("Error with LoginServer: " + _ip + " Exception: " + e);
                Drop("Error: " + e.Message);
            }
        }

        /// <summary>
        ///   Reads a packet without processing it.
        /// </summary>
        /// <returns> The Packet </returns>
        private IPacket GetPacket()
        {
            try
            {
                return (IPacket) _serializer.Deserialize(_stream);
            }
            catch (Exception e)
            {
                _log.Critical("Error with LoginServer: " + _ip + " Exception: " + e);
                Drop("Error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        ///   Sends a packet to the client.
        /// </summary>
        /// <param name="packet"> The Packet. </param>
        internal void SendPacket(IPacket packet)
        {
            try
            {
                _serializer.Serialize(_stream, packet);
            }
            catch (Exception e)
            {
                _log.Critical("Error with LoginServer: " + _ip + " Exception: " + e);
                Drop("Error: " + e.Message);
            }
        }

        /// <summary>
        ///   Disconnect to the Authenticator Server.
        /// </summary>
        /// <param name="reason"> The reason for the disconnection. </param>
        internal void Disconnect(string reason = "Disconnecting")
        {
            _log.Info("Disconnecting from LoginServer. Reason: " + reason);

            if (IsConnected)
                SendPacket(new DisconnectPacket(reason));

            _stream = null;
            _serializer = null;
            _client.Close();
            GC.Collect();
        }

        /// <summary>
        ///   Release the connection.
        /// </summary>
        /// <param name="reason"> The reason for the release. </param>
        internal void Drop(string reason = "Disconnecting")
        {
            _log.Info("Dropping LoginServer Connection. Reason: " + reason);
            _stream = null;
            _serializer = null;
            _client.Close();
            GC.Collect();
        }

        /// <summary>
        ///   Process an incoming packet.
        /// </summary>
        /// <param name="packet"> The incoming packet. </param>
        private void ProcessPacket(IPacket packet)
        {
            switch (packet.Id)
            {
// ReSharper disable PossibleNullReferenceException
                case (int) CommonPackets.DisconnectPacket:
                    Drop((packet as DisconnectPacket).Reason);
                    break;
                case (int) LoginPackets.PasswordResponsePacket:
                    if ((packet as PasswordResponsePacket).Response == PasswordResponse.Ok)
                    {
                        _log.Info("Succesfully authorized by login server");
                        IsConnected = true;
                    }
                    else
                    {
                        _log.Error("LoginServer Authroziation Error");
                        IsConnected = false;
                    }
                    break;
                default:
                    Drop("Unknown Packet");
                    break;
// ReSharper restore PossibleNullReferenceException
            }
        }

        internal bool AskAuthentication(string username, uint sessionId)
        {
            SendPacket(new SessionRequestPacket(username, sessionId));
            SessionResponsePacket p = (SessionResponsePacket) GetPacket();
            return p.Username == username && (p.Response == SessionResponse.Ok);
        }
    }
}