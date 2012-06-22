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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Metin2AuthServerEmulator.Network.Packets;
using Metin2ServerEmulatorCommon;
using Metin2ServerEmulatorCommon.Util;

namespace Metin2AuthServerEmulator.Network
{
    internal partial class Client
    {
        private readonly TcpClient _client;
        private readonly string _ip;

        private readonly Thread _listenerThread;

        private readonly Logger _log = Server.ServerInstance.Logger;
        private readonly uint _sessionId;
        private readonly NetworkStream _stream;

        private int _bufferSize = 1;
        private bool _needToDecrypt;

        internal Client(TcpClient tcpClient, uint sessionId)
        {
            _client = tcpClient;
            _stream = tcpClient.GetStream();
            _sessionId = sessionId;
            try
            {
                _ip = IPAddress.Parse(((IPEndPoint) tcpClient.Client.RemoteEndPoint).Address.ToString()).ToString();
            }
            catch (Exception ex)
            {
                Drop("Invalid Connection. Message: " + ex.Message);
            }
            IsConnected = true;

            _log.Info("Accepted connection from address: " + _ip);


            _listenerThread = new Thread(Listen) {Name = "Client Listener: " + _ip};
            _listenerThread.Start();

            SendFirstHandshake();
        }

        internal bool IsConnected { get; private set; }

        private void Listen()
        {
            while (IsConnected)
            {
                ReadData();
            }
        }

        /// <summary>
        ///   Reads packet id from the client
        /// </summary>
        private void ReadData()
        {
            try
            {
                while (IsConnected)
                {
                    Thread.Sleep(1);
                    if (_stream.DataAvailable)
                    {
                        byte[] buffer = new byte[_bufferSize];
                        _stream.Read(buffer, 0, _bufferSize);
                        ProcessPacket(buffer);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                //Nothing to do
            }
            catch (Exception e)
            {
                _log.Error("Error with client: " + _ip + " Exception: " + e);
                Drop("IOException: " + e.Message);
            }
        }

        private byte[] ReadBuffer(int size)
        {
            try
            {
                byte[] buffer = new byte[size];
                _stream.Read(buffer, 0, size);
                return buffer;
            }
            catch (Exception e)
            {
                _log.Error("Error with client: " + _ip + " Exception: " + e);
                Drop("IOException: " + e.Message);
            }
            return null;
        }

        private IPacketIn ReadPacket(IPacketIn packet, byte[] initialBuffer = null)
        {
            try
            {
                byte[] buffer = ReadBuffer(packet.Length - _bufferSize);
                if (_needToDecrypt)
                {
                    if (initialBuffer == null)
                    {
                        throw new Exception("InitialBuffer not provided for packet id " + packet.Id);
                    }
                    buffer = XTEA.Decrypt(buffer, Config.Pong);
                    byte[] newBuffer = new byte[initialBuffer.Length + buffer.Length];
                    initialBuffer.CopyTo(newBuffer, 0);
                    buffer.CopyTo(newBuffer, initialBuffer.Length);
                    buffer = newBuffer;
                }
                packet.ParseBuffer(buffer); //Length - buffer because of alread read data

                return packet;
            }
            catch (Exception e)
            {
                _log.Error("Error with client: " + _ip + " Exception: " + e);
                Drop("IOException: " + e.Message);
            }
            return null;
        }

        /// <summary>
        ///   Sends a packet to the client
        /// </summary>
        /// <param name="packet"> The Packet </param>
        internal void SendPacket(IPacket packet)
        {
            try
            {
                _stream.Write(_needToDecrypt ? XTEA.Encrypt(packet.Data, Config.Pong) : packet.Data, 0,
                              packet.Data.Length);
            }
            catch (IOException e)
            {
                _log.Error("Error with client: " + _ip + " Exception: " + e);
                Drop("IOException: " + e.Message);
            }
        }

        /// <summary>
        ///   Disconnects (or kicks) the client
        /// </summary>
        /// <param name="reason"> Reason of disconnection, will be sent to client </param>
        internal void Disconnect(string reason = "")
        {
            IsConnected = false;
            _log.Info("Disconnecting client Address: " + _ip + " Reason: " + reason);
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
            _log.Warn("Dropping client Address: " + _ip + " Reason: " + reason);
            _stream.Dispose();
            _client.Close();
            _listenerThread.Abort();
        }

        private void ProcessPacket(byte[] id)
        {
            if (_needToDecrypt)
            {
                id = XTEA.Decrypt(id, Config.Pong);
            }

            switch (id[0])
            {
                case (byte) InPackets.HandshakePacket:
                    ContinueHandshake();
                    break;
                case (byte) InPackets.LoginPacket:
                    VerifyLogin(id);
                    break;
                default:
                    Drop("Unknown Packet");
                    break;
            }
        }
    }
}