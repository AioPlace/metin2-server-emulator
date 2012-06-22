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

namespace Metin2ServerEmulatorCommon.LocalNetwork.Packets
{
    /// <summary>
    ///   Packets sent by both servers
    /// </summary>
    public enum CommonPackets
    {
        DisconnectPacket = 0
    }

    /// <summary>
    ///   Packets sent by login server
    /// </summary>
    public enum LoginPackets
    {
        PasswordResponsePacket = 2,
        AuthPacket = 3,
        SessionResponsePacket = 5
    }

    /// <summary>
    ///   Packets sent by game server
    /// </summary>
    public enum GamePackets
    {
        PasswordPacket = 1,
        SessionRequestPacket = 4,
        SessionActionPacket = 6
    }
}