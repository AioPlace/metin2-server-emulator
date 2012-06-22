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

using Metin2AuthServerEmulator.Services;
using Metin2ServerEmulatorCommon.LocalNetwork;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets.Game;
using Metin2ServerEmulatorCommon.LocalNetwork.Packets.Login;

namespace Metin2AuthServerEmulator.LocalNetwork
{
    internal partial class LocalClient
    {
        private void ProcessPasswordPacket(IPacket packet)
        {
// ReSharper disable PossibleNullReferenceException
            if ((packet as PasswordPacket).Password != Config.LocalNetPassowrd)
// ReSharper restore PossibleNullReferenceException
            {
                SendPacket(new PasswordResponsePacket(PasswordResponse.WrongPassword));
                Disconnect("Wrong Password");
            }
            else
            {
                SendPacket(new PasswordResponsePacket(PasswordResponse.Ok));
                _log.Info("GameServer " + _ip + " logged in");
            }
        }

        private void ProcessSessionActionPacket(IPacket packet)
        {
            SessionActionPacket sap = (SessionActionPacket) packet;
            if (sap.Action == SessionAction.Login)
                AuthQueue.LogIn(sap.Username);
            else if (sap.Action == SessionAction.Logout)
                AuthQueue.LogOut(sap.Username);
            else if (sap.Action == SessionAction.Disconnect)
                AuthQueue.Remove(sap.Username);
        }

        private void ProcessSessionRequestPacket(IPacket packet)
        {
            SessionRequestPacket p = (SessionRequestPacket) packet;
            if (AuthQueue.CheckAuth(p.Username, p.SessionID))
                SendPacket(new SessionResponsePacket(p.Username, SessionResponse.Ok));
            else if (AuthQueue.IsAlreadyLoggedIn(p.Username))
                SendPacket(new SessionResponsePacket(p.Username, SessionResponse.AlreadyLoggedIn));
            else
                SendPacket(new SessionResponsePacket(p.Username, SessionResponse.Wrong));
        }
    }
}