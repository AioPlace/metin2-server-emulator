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

namespace Metin2GameServerEmulator.World.Entities
{
    internal class NPC : IEntity
    {
        #region "Eredited From IEntity"

        public ushort Vid { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public int MaxHP { get; set; }
        public int CurHP { get; set; }
        public uint Strong { get; set; }
        public uint Vitality { get; set; }
        public uint Dexterity { get; set; }
        public uint Intelligence { get; set; }
        public Position RelativePosition { get; set; }
        public Position AbsolutePosition { get; set; }
        public int MapIndex { get; set; }
        public IEntity LastAttacker { get; set; }
        public EntityType Type { get; set; }
        public EmpireFlag Empire { get; set; }

        #endregion

        internal int Vnum { get; set; }
    }
}