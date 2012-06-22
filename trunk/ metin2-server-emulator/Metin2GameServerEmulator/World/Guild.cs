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

namespace Metin2GameServerEmulator.World
{
    internal class Guild
    {
        internal uint ID { get; set; }
        internal string Name { get; set; }
        internal byte Empire { get; set; }
        internal uint Master { get; set; }
        internal uint Wins { get; set; }
        internal uint Loses { get; set; }
        internal uint Draws { get; set; }
        internal byte Level { get; set; }
        internal uint Mana { get; set; }
        internal ushort SkillPoints { get; set; }
    }
}