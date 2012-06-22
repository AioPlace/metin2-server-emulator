using System;
using System.Text;
using Metin2GameServerEmulator.World.Entities;

namespace Metin2GameServerEmulator.Network.Packets.Out
{
    internal class EnterGamePacket : IPacket
    {
        private const int Length = 104;

      
        public EnterGamePacket(Character character)
        {
          
            Data = new byte[Length];
            Data[0] = Id;
            Data[1] = 0x4;
            BitConverter.GetBytes(character.Vid ).CopyTo(Data,0x9);
            Data[8] = 0x71;
            Data[0xD] = character.Race;
            Encoding.ASCII.GetBytes(character.Name).CopyTo(Data,0xF);


        }

        #region IPacket Members

        public byte Id
        {
            get { return (byte)OutPackets.EnterGamePacket; }
        }

        public byte[] Data { get; private set; }

        #endregion
    }
}