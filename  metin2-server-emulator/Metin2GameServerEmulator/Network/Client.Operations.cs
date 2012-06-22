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

using System.Globalization;
using Metin2GameServerEmulator.DAO;
using Metin2GameServerEmulator.Network.Packets.In;
using Metin2GameServerEmulator.Network.Packets.Out;
using Metin2GameServerEmulator.Services;
using Metin2GameServerEmulator.World.Entities;
using Metin2ServerEmulatorCommon.DAO;
using Metin2ServerEmulatorCommon.LocalNetwork;

namespace Metin2GameServerEmulator.Network
{
    internal partial class Client
    {
        #region "Handshake"

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

            HandshakeMidPacket hmp2 = new HandshakeMidPacket();
            SendPacket(hmp2);

            _stream.ReadByte();
            HandshakePacket hp3 = (HandshakePacket) ReadPacket(new HandshakePacket());

            HandshakeClosePacket hcp = new HandshakeClosePacket();
            SendPacket(hcp);

            _needToDecrypt = true;
            _bufferSize = 8;
        }

        #endregion

        private void VerifyAuth()
        {
            GameAuthorizationPacket gap = (GameAuthorizationPacket) ReadPacket(new GameAuthorizationPacket());
            _log.Info(gap.Username);
            _log.Info(gap.SessionID.ToString(CultureInfo.InvariantCulture));
            if (AuthService.CheckSession(gap.Username, gap.SessionID))
            {
                AccountName = gap.Username;
                AuthService.SendSessionAction(gap.Username, SessionAction.Login);

                //TODO Save characters in class
                CharacterDAO chrMgr = new CharacterDAO(Server.ServerInstance.Database);
                AccountDAO accMgr = new AccountDAO(Server.ServerInstance.Database);
                int accountID = accMgr.GetAccountID(gap.Username);
                CharacterListPacket clp = new CharacterListPacket(chrMgr.GetCharactersByAccount(accountID),
                                                                  accMgr.GetAccountEmpire(accountID));
                SendPacket(clp);
            }
            else
            {
                Disconnect("Invalid session");
            }
        }

        private void GameEnter() //TODO move this to player class
        {
            CharacterChoosePacket ccp = (CharacterChoosePacket) ReadPacket(new CharacterChoosePacket());
            _log.Info(ccp.Index.ToString(CultureInfo.InvariantCulture));

            //TODO Save characters in class
            CharacterDAO chrMgr = new CharacterDAO(Server.ServerInstance.Database);
            AccountDAO accMgr = new AccountDAO(Server.ServerInstance.Database);
            int accountID = accMgr.GetAccountID(AccountName);
            Character[] chars = chrMgr.GetCharactersByAccount(accountID);
            CharacterListPacket clp = new CharacterListPacket(chars,
                                                              accMgr.GetAccountEmpire(accountID));


            //TODO Everything wrong down here

            //inventario ->
            SendPacket(new InventoryItemsPacket());

            //stats
            SendPacket(new CharacterStatsPacket(chars[ccp.Index])); //TODO Check index

            //unknown 1
            ReadBuffer(8);

            //entity spawn
            SendPacket(new EntitySpawnPacket(chars[ccp.Index]));

            LauncherNamePacket lnp = new LauncherNamePacket();
            ReadPacket(lnp);
        }

        private void CreateCharacter() //TODO move this call to player class
        {
            CharacterCreatePacket ccp = (CharacterCreatePacket) ReadPacket(new CharacterCreatePacket());
            Character character = Character.Create(AccountName, ccp);
        }
    }
}