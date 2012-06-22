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

using System.Collections.Generic;
using Metin2ServerEmulatorCommon.Util;
using PacketDotNet;

namespace Metin2PacketDumper.Parsing
{
    public class MetinPacket
    {
        private readonly ushort _authPort;
        private readonly ushort _gamePort;
        private readonly byte[] _pong = ByteSupport.HexStringToByteArray("00000000000000000000000000000000");
        internal int MaxBuffer = 1452;

        internal MetinPacket(TcpPacket p, ushort authPort, ushort gamePort, string process)
        {
            Length = p.PayloadData.Length;
            Crypted = false;
            Route = GetRouteString(p, authPort, gamePort);
            Metin2Process = process;

            if (Length <= 0)
            {
                PacketString = string.Format("{0} | ID: XX | Len: {1} | NO PSH", Route, Length);
                return;
            }

            Id = p.PayloadData[0];
            Data = p.PayloadData;
            DestinationPort = p.DestinationPort;
            SourcePort = p.SourcePort;
            _authPort = authPort;
            _gamePort = gamePort;

            PSH = p.Psh;

            DecryptPacket();

            PacketString = string.Format("{0} | ID: {1} - {3} | Len: {2} | GuessedType : {4}", Route, Id.ToString("X2"),
                                         Length.ToString("D4"), Id.ToString("D3"), Type);
        }

        internal byte[] XTEAKey1 { get; private set; }
        internal byte[] XTEAKey2 { get; private set; }
        internal string Metin2Process { get; set; }
        internal int Id { get; private set; }
        internal int Length { get; private set; }
        internal ushort SourcePort { get; private set; }
        internal ushort DestinationPort { get; private set; }
        internal byte[] Data { get; private set; }
        internal byte[] DecryptedData { get; private set; }
        internal bool Crypted { get; private set; }
        internal bool PSH { get; private set; }
        internal bool Merged { get; private set; }
        internal string Route { get; private set; }
        internal string PacketString { get; private set; }

        internal string Type
        {
            get { return ((PacketList) Id).ToString(); }
        }


        internal bool IsGameAuthPacket(byte[] data)
        {
            if (data.Length != 56)
                return false;
            data = XTEA.Decrypt(data, _pong);
            return (data[0] == 0x6D);
        }

        internal void DecryptPacket()
        {
            if (Length%8 == 0 || Length == MaxBuffer)
            {
                Crypted = true;
                byte[] _key = new byte[16];
                if (DestinationPort == _authPort || SourcePort == _authPort)
                    _key = _pong;
                else if (DestinationPort == _gamePort)
                {
                    if (IsGameAuthPacket(Data))
                    {
                        DecryptedData = XTEA.Decrypt(Data, _pong);
                        return;
                    }
                    if (XTEAKey1 == null)
                        UpdateKeys();
                    _key = XTEAKey1;
                }
                else if (SourcePort == _gamePort)
                {
                    if (XTEAKey2 == null)
                        UpdateKeys();
                    _key = XTEAKey2;
                }
                else
                    return;
                DecryptedData = XTEA.Decrypt(Data, _key);
                Id = DecryptedData[0];
            }
        }

        internal void UpdateKeys()
        {
            Metin2Key m2 = new Metin2Key(Metin2Process);
            byte[][] keys = m2.Read();
            XTEAKey1 = keys[0];
            XTEAKey2 = keys[1];
        }

        internal string GetRouteString(TcpPacket p, ushort authPort, ushort gamePort)
        {
            string src, dest;
            if (p.SourcePort == authPort)
            {
                src = "Auth";
            }
            else if (p.SourcePort == gamePort)
            {
                src = "Game";
            }
            else
            {
                src = "Client";
            }

            if (p.DestinationPort == authPort)
            {
                dest = "Auth";
            }
            else if (p.DestinationPort == gamePort)
            {
                dest = "Game";
            }
            else
            {
                dest = "Client";
            }
            if (dest != src)
                return string.Format("{0} -> {1}", src, dest);
            return ("Client -> Self");
        }

        public override string ToString()
        {
            return PacketString;
        }

        public void MergePacket(MetinPacket toMerge)
        {
            Merged = true;
            List<byte> merged = new List<byte>();
            merged.AddRange(Data);
            merged.AddRange(toMerge.Data);
            Data = merged.ToArray();
            Length = Data.Length;
            DecryptPacket();
            PacketString = string.Format("{0} | ID: {1} - {3} | Len: {2} | GuessedType : {4}", Route, Id.ToString("X2"),
                                         Length.ToString("D4"), Id.ToString("D3"), Type);
        }

        #region Nested type: PacketList

        internal enum PacketList
        {
            TextPacket = 1,
            HandshakeMidPacket = 0xFF,
            HandshakeOpenPacket = 0xFD,
            HandshakeClosePacket = 0xFD,
            TimeSyncPacket = 252,
            MarkLoginPacket = 100,
            MarkIndexListPacket = 104,
            MarkCrcListPacket = 101,
            MarkUploadPacket = 102,
            SymbolUploadPacket = 112,
            SymbolCrcPacket = 113,
            LoginPacket = 49,
            LoginPacket2 = 52,
            LoginPacket3 = 65,
            AttackPacket = 2,
            ChatPacket = 3,
            WhisperPacket = 19,
            SelectPacket = 6,
            CreatePacket = 4,
            DeletePacket = 5,
            EnterGamePacket = 10,
            ItemUsePacket = 11,
            ItemDropPacket = 12,
            ItemDropPacket2 = 20,
            ItemMovePacket = 13,
            ItemPickupPacket = 15,
            QuickSlotAddPacket = 16,
            QuickSlotDelPacket = 17,
            QuickSlotSwapPacket = 18,
            ShopPacket = 50,
            OnClickPacket = 26,
            ExchangePacket = 27,
            PositionPacket = 28,
            ScriptAnswerPacket = 29,
            ScriptButtonPacket = 66,
            QuestInputString = 30,
            QuestConfirm = 31,
            MovePacket = 7,
            SyncPositionPacket = 8,
            FlyTargetPacket = 51,
            AddFlyTargetPacket = 53,
            ShootPacket = 54,
            UseSkillPacket = 52,
            UseItemToItemPacket = 60,
            TargetPacket = 61,
            WarpPacket = 65,
            MessengerPacket = 67,
            PartyRemovePacket = 74,
            PartyInvitePacket = 72,
            PartyIntiteAnswerPacket = 73,
            PartySetStatePacket = 75,
            PartyUseSkillPacket = 76,
            PartyParamPacket = 78,
            EmpirePacket = 90,
            SafeboxCheckoutPacket = 71,
            SafeboxCheckinPacket = 70,
            SafeboxItemMovePacket = 77,
            GuildPacket = 80,
            AnswerMakeGuildPacket = 81,
            FishingPacket = 82,
            ItemGivePacket = 83,
            HackPacket = 105,
            MyShopPacket = 55,
            RefirePacket = 96,
            ChangeNamePacket = 106,
            VersionPacket1 = 253,
            VersionPacket2 = 241,
            PongPacket = 254,
            MallCheckoutPacket = 69,
            ScriptSelectItemPacket = 114,
            PasspodAnswerPacket = 202,
            HackShieldResponsePacket = 203,
        }

        #endregion
    }
}