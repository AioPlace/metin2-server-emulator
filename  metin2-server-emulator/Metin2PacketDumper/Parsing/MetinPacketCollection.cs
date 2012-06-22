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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Metin2PacketDumper.Parsing
{
    public class MetinPacketCollection : ICollection, ISerializable
    {
        private readonly List<MetinPacket> _mPackets;

        public MetinPacketCollection()
        {
            _mPackets = new List<MetinPacket>();
        }

        #region ICollection Members

        public void CopyTo(Array array, int index = 0)
        {
            _mPackets.CopyTo((MetinPacket[]) array, index);
        }

        public int Count
        {
            get { return _mPackets.Count; }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator GetEnumerator()
        {
            return _mPackets.GetEnumerator();
        }

        #endregion

        #region ISerializable Members

        public void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void Add(MetinPacket mPacket)
        {
            _mPackets.Add(mPacket);
        }

        public void AddRange(MetinPacket[] mPackets)
        {
            foreach (MetinPacket mPacket in mPackets)
                Add(mPacket);
        }

        public MetinPacket[] ToArray()
        {
            return _mPackets.ToArray();
        }
    }
}