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

namespace Metin2GameServerEmulator.Network.Packets
{
    internal enum InPackets
    {
        CharacterCreatePacket = 0x04,
        CharacterChoosePacket = 0x06,
        MovementPacket = 0x07, //NOTE Not sure
        GameAuthorizationPacket = 0x6D,
        UnknownPacket1 = 0x0A,
        LauncherNamePacket = 0xF1,
        HandshakePacket = 0xFF
    }

    internal enum InPacketsNew
    {
        TextPacket = 0x01,
        HandshakeMidPacket = 0xFF,
        HandshakeOpenPacket = 0xFD,
        HandshakeClosePacket = 0xFD,
        TimeSyncPacket = 0xFC,
        MarkLoginPacket = 0x64,
        MarkIndexListPacket = 0x68,
        MarkCrcListPacket = 0x65,
        MarkUploadPacket = 0x66,
        SymbolUploadPacket = 0x70,
        SymbolCrcPacket = 0x71,
        LoginPacket = 0x31,
        LoginPacket2 = 0x34,
        LoginPacket3 = 0x41,
        AttackPacket = 0x02,
        ChatPacket = 0x03,
        WhisperPacket = 0x13,
        SelectPacket = 0x06,
        CreatePacket = 0x04,
        DeletePacket = 0x05,
        EnterGamePacket = 0x0A,
        ItemUsePacket = 0x0B,
        ItemDropPacket = 0x0C,
        ItemDropPacket2 = 0x14,
        ItemMovePacket = 0x0D,
        ItemPickupPacket = 0x0F,
        QuickSlotAddPacket = 0x10,
        QuickSlotDelPacket = 0x11,
        QuickSlotSwapPacket = 0x12,
        ShopPacket = 0x32,
        OnClickPacket = 0x1A,
        ExchangePacket = 0x1B,
        PositionPacket = 0x1C,
        ScriptAnswerPacket = 0x1D,
        ScriptButtonPacket = 0x42,
        QuestInputString = 0x1E,
        QuestConfirm = 0x1F,
        MovePacket = 0x07,
        SyncPositionPacket = 0x08,
        FlyTargetPacket = 0x33,
        AddFlyTargetPacket = 0x35,
        ShootPacket = 0x36,
        UseSkillPacket = 0x34,
        UseItemToItemPacket = 0x3C,
        TargetPacket = 0x3D,
        WarpPacket = 0x41,
        MessengerPacket = 0x43,
        PartyRemovePacket = 0x4A,
        PartyInvitePacket = 0x48,
        PartyIntiteAnswerPacket = 0x49,
        PartySetStatePacket = 0x4B,
        PartyUseSkillPacket = 0x4C,
        PartyParamPacket = 0x4E,
        EmpirePacket = 0x5A,
        SafeboxCheckoutPacket = 0x47,
        SafeboxCheckinPacket = 0x46,
        SafeboxItemMovePacket = 0x4D,
        GuildPacket = 0x50,
        AnswerMakeGuildPacket = 0x51,
        FishingPacket = 0x52,
        ItemGivePacket = 0x53,
        HackPacket = 0x69,
        MyShopPacket = 0x37,
        RefirePacket = 0x60,
        ChangeNamePacket = 0x6A,
        VersionPacket1 = 0xFD,
        VersionPacket2 = 0xF1,
        PongPacket = 0xFE,
        MallCheckoutPacket = 0x45,
        ScriptSelectItemPacket = 0x72,
        PasspodAnswerPacket = 0xCA,
        HackShieldResponsePacket = 0xCB,
    }

    internal enum OutPackets
    {
        InventoryItemsPacket = 0x00, //NOTE Probably Wrong
        EntitySpawnPacket = 0x01,
        CharacterStatsPacket = 0x10,
        CharacterInfoPacket = 0x11,
        ItemCreatePacket = 0x15,
        UnknownPacket = 0x1C,
        GuildPacket = 0x4B,
        CharacterListPacket = 0x5A,
        HandshakeOpenPacket = 0xFD,
        HandshakeClosePacket = 0xFD, //NOTE Seems also action packet
        EnterGamePacket = 0xFD, //NOTE Wrong name, read above
        HandshakeMidPacket = 0xFF
    }
}