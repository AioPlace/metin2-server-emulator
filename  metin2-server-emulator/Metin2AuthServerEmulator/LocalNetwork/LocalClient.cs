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
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Metin2ServerEmulatorCommon;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets.Common;

namespace Metin2AuthServerEmulator.LocalNetwork
{
    internal partial class LocalClient
    {
        private readonly TcpClient _client;
        private readonly string _ip;

        private readonly Thread _listenerThread;

        private readonly Logger _log = Server.ServerInstance.Logger;
        private readonly IFormatter _serializer;
        private readonly NetworkStream _stream;

        internal LocalClient(TcpClient tcpClient)
        {
            _client = tcpClient;
            _stream = tcpClient.GetStream();

            _ip = IPAddress.Parse(((IPEndPoint) tcpClient.Client.RemoteEndPoint).Address.ToString()).ToString();

            IsConnected = true;

            _log.Info("GameServer connected from address: " + _ip);

            _serializer = new BinaryFormatter();

            _listenerThread = new Thread(Listen) {Name = "GameServer Listener: " + _ip};
            _listenerThread.Start();
        }

        internal bool IsConnected { get; private set; }

        private void Listen()
        {
            while (IsConnected)
            {
                ReadPacket();
            }
        }

        /// <summary>
        ///   Reads packets from the client
        /// </summary>
        private void ReadPacket()
        {
            try
            {
                ProcessPacket((IPacket) _serializer.Deserialize(_stream));
            }
            catch (ThreadAbortException)
            {
                //Nothing to do, closing connection
            }
            catch (Exception e)
            {
                _log.Error("Error with GameServer: " + _ip + " Exception: " + e.ToString());
                Drop("IOException: " + e.Message);
            }
        }

        /// <summary>
        ///   Sends a packet to the client
        /// </summary>
        /// <param name="packet"> The Packet </param>
        internal void SendPacket(IPacket packet)
        {
            try
            {
                _serializer.Serialize(_stream, packet);
            }
            catch (Exception e)
            {
                _log.Error("Error with GameServer: " + _ip + " Exception: " + e);
                Drop("IOException: " + e.Message);
            }
        }

        /// <summary>
        ///   Disconnects (or kicks) the client
        /// </summary>
        /// <param name="reason"> Reason of disconnection, will be sent to client </param>
        internal void Disconnect(string reason = "")
        {
            SendPacket(new DisconnectPacket(reason));
            IsConnected = false;
            _log.Warn("Disconnecting GameServer Address: " + _ip + " Reason: " + reason);
            _stream.Dispose();
            _client.Close();
            _listenerThread.Abort();
        }

        /// <summary>
        ///   Drops connection from client, same as Disconnect but no packet is sent
        /// </summary>
        /// <param name="reason"> Reason of drop, will be logged </param>
        internal void Drop(string reason = "")
        {
            IsConnected = false;
            _log.Warn("Dropping GameServer Address: " + _ip + " Reason: " + reason);
            _stream.Dispose();
            _client.Close();
            _listenerThread.Abort();
        }

        /// <summary>
        ///   Processes received packet
        /// </summary>
        /// <param name="packet"> Received packet </param>
        private void ProcessPacket(IPacket packet)
        {
            // ReSharper disable PossibleNullReferenceException
            switch (packet.Id)
            {
                case (int) CommonPackets.DisconnectPacket:
                    Drop("DisconnectPacket(" + (packet as DisconnectPacket).Reason + ")");
                    break;

                case (int) GamePackets.PasswordPacket:
                    ProcessPasswordPacket(packet);
                    break;

                case (int) GamePackets.SessionActionPacket:
                    ProcessSessionActionPacket(packet);
                    break;

                case (int) GamePackets.SessionRequestPacket:
                    ProcessSessionRequestPacket(packet);
                    break;

                default: //Unknown Packet
                    Drop("Unknown packet received");
                    break;
            }
            // ReSharper restore PossibleNullReferenceException
        }
    }
}