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
using Metin2GameServerEmulator.Network.Packets;
using Metin2GameServerEmulator.Services;
using Metin2ServerEmulatorCommon;
using Metin2ServerEmulatorCommon.LocalNetwork;
using Metin2ServerEmulatorCommon.Util;

namespace Metin2GameServerEmulator.Network
{
    /// <summary>
    ///   Represents a Metin2 client.
    /// </summary>
    internal partial class Client
    {
        private readonly TcpClient _client;
        private readonly string _ip;

        private readonly Thread _listenerThread;

        private readonly Logger _log = Server.ServerInstance.Logger;
        private readonly NetworkStream _stream;

// ReSharper disable ConvertToConstant.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private int _bufferSize = 1;
        private byte[] _idBuffer;
        private bool _needToDecrypt = false;
// ReSharper restore FieldCanBeMadeReadOnly.Local
// ReSharper restore ConvertToConstant.Local

        /// <summary>
        ///   Initialize a new instance of Client.
        /// </summary>
        /// <param name="tcpClient"> The TcpClient instance used to communicate with the client. </param>
        internal Client(TcpClient tcpClient)
        {
            _client = tcpClient;
            _stream = tcpClient.GetStream();

            _ip = IPAddress.Parse(((IPEndPoint) tcpClient.Client.RemoteEndPoint).Address.ToString()).ToString();

            IsConnected = true;

            _log.Info("Accepted connection from address: " + _ip);

            AccountName = "";

            _listenerThread = new Thread(Listen) {Name = "Client Listener: " + _ip};
            _listenerThread.Start();

            SendFirstHandshake();
        }

        internal string AccountName { get; private set; }

        /// <summary>
        ///   Returns true if the server is connected. Otherwise false.
        /// </summary>
        internal bool IsConnected { get; private set; }

        /// <summary>
        ///   Waits for an incoming packet.
        /// </summary>
        private void Listen()
        {
            while (IsConnected)
            {
                ReadData();
            }
        }

        /// <summary>
        ///   Reads an incoming packet from the client.
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

                _log.Packet("Read buffer of " + size + " bytes");

                return buffer;
            }
            catch (IOException e)
            {
                _log.Error("Error with client: " + _ip + " Exception: " + e);
                Drop("IOException: " + e.Message);
            }
            return null;
        }

        private IPacketIn ReadPacket(IPacketIn packet)
        {
            try
            {
                byte[] buffer = ReadBuffer(packet.Length - _bufferSize);
                if (_needToDecrypt)
                {
                    if (_idBuffer == null)
                    {
                        //throw new Exception("InitialBuffer not provided for packet id " + packet.Id);
                        _idBuffer = ReadBuffer(8);
                    }
                    buffer = XTEA.Decrypt(buffer, Config.Pong);
                    byte[] newBuffer = new byte[_idBuffer.Length + buffer.Length];
                    _idBuffer.CopyTo(newBuffer, 0);
                    buffer.CopyTo(newBuffer, _idBuffer.Length);
                    buffer = newBuffer;
                }
                packet.ParseBuffer(buffer); //Length - buffer because of alread read data

                _log.Packet("Read PacketID: " + packet.Id.ToString("X2"));

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
        ///   Sends a packet to the client.
        /// </summary>
        /// <param name="packet"> The Packet. </param>
        internal void SendPacket(IPacket packet)
        {
            try
            {
                _stream.Write(_needToDecrypt ? XTEA.Encrypt(packet.Data, Config.Pong) : packet.Data, 0,
                              packet.Data.Length);

                _log.Packet("Sent PacketID: " + packet.Id.ToString("X2"));
            }
            catch (IOException e)
            {
                _log.Error("Error with client: " + _ip + " Exception: " + e);
                Drop("IOException: " + e.Message);
            }
        }

        /// <summary>
        ///   Disconnects (or kicks) the client.
        /// </summary>
        /// <param name="reason"> Reason of disconnection, will be sent to client. </param>
        internal void Disconnect(string reason = "")
        {
            //TODO Send Disconnect packet

            if (AccountName != "")
                AuthService.SendSessionAction(AccountName, SessionAction.Disconnect);

            IsConnected = false;
            _log.Info("Disconnecting client Address: " + _ip + " Reason: " + reason);
            _stream.Dispose();
            _client.Close();
            _listenerThread.Abort();
        }

        /// <summary>
        ///   Drops connection from client, same as Disconnect but no packet is sent.
        /// </summary>
        /// <param name="reason"> Reason of drop, will be logged. </param>
        internal void Drop(string reason = "")
        {
            //NOTE No packet here

            if (AccountName != "")
                AuthService.SendSessionAction(AccountName, SessionAction.Disconnect);

            IsConnected = false;
            _log.Warn("Dropping client Address: " + _ip + " Reason: " + reason);
            _stream.Dispose();
            _client.Close();
            _listenerThread.Abort();
        }

        /// <summary>
        ///   Processes received packet
        /// </summary>
        /// <param name="id"> Packet id or Packet initial buffer </param>
        private void ProcessPacket(byte[] id)
        {
            if (_needToDecrypt)
            {
                id = XTEA.Decrypt(id, Config.Pong);
            }

            _idBuffer = id;

            switch (id[0])
            {
                case (byte) InPackets.HandshakePacket:
                    _log.Packet("Received HandshakePacket");
                    ContinueHandshake();
                    break;
                case (byte) InPackets.GameAuthorizationPacket:
                    _log.Packet("Received GameAuthorizationPacket");
                    VerifyAuth();
                    break;
                case (byte) InPackets.CharacterCreatePacket:
                    _log.Packet("Received CharacterCreatePacket");
                    CreateCharacter();
                    break;
                case (byte) InPackets.CharacterChoosePacket:
                    // TODO Code for map enter
                    _log.Packet("Received CharacterChoosePacket");
                    GameEnter();
                    break;
                case (byte) InPackets.LauncherNamePacket:
                    _log.Packet("Received LauncherNamePacket.\nSooooo useful...");
                    break;
                case (byte) InPackets.UnknownPacket1:
                    _log.Packet("Received UnknownPacket1.");
                    break;
                default:
                    _log.PacketConsole("Unknown Packet ID: " + id[0]);
                    Drop("Unknown Packet ID " + id[0]);
                    break;
            }
        }
    }
}