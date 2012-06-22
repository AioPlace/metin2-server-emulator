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
using Metin2AuthServerEmulator.Network.Packets.In;
using Metin2AuthServerEmulator.Network.Packets.Out;
using Metin2AuthServerEmulator.Services;
using Metin2ServerEmulatorCommon.DAO;

namespace Metin2AuthServerEmulator.Network
{
    internal partial class Client
    {
        //Login Constant Answers
        private const string WrongPasswordMessage = "WRONGPWD";
        private const string PermaBannedMessage = "BLOCK";
        private const string TempBannedMessage = "NOTAVAIL";
        private const string AlreadyLoggedInMessage = "ALREADY";

        private void SendFirstHandshake()
        {
            HandshakeOpenPacket hop = new HandshakeOpenPacket();
            SendPacket(hop);
        }

        private void ContinueHandshake()
        {
            HandshakePacket hp = (HandshakePacket) ReadPacket(new HandshakePacket());

            HandshakeMidPacket hmp = new HandshakeMidPacket();
            SendPacket(hmp);

            _stream.ReadByte();
            HandshakePacket hp2 = (HandshakePacket) ReadPacket(new HandshakePacket());

            HandshakeClosePacket hcp = new HandshakeClosePacket();
            SendPacket(hcp);

            _needToDecrypt = true;
            _bufferSize = 8;
        }

        private void VerifyLogin(byte[] initialBuffer)
        {
            LoginPacket lp = (LoginPacket) ReadPacket(new LoginPacket(), initialBuffer);
            AccountDAO l = new AccountDAO(Server.ServerInstance.Database);

            if (l.VerifyLogin(lp.Username, lp.Password))
            {
                Tuple<bool, bool> banStatus = l.BanStatus(lp.Username);
                if (banStatus.Item1)
                {
                    SendPacket(banStatus.Item2
                                   ? new LoginFailPacket(PermaBannedMessage)
                                   : new LoginFailPacket(TempBannedMessage));
                    return;
                }

                if (AuthQueue.Add(lp.Username, _sessionId))
                {
                    SendPacket(new LoginOkPacket(_sessionId));
                }
                else
                {
                    SendPacket(new LoginFailPacket(AlreadyLoggedInMessage));
                }
            }
            else
            {
                SendPacket(new LoginFailPacket(WrongPasswordMessage));
            }
            Disconnect("Completed");
        }
    }
}