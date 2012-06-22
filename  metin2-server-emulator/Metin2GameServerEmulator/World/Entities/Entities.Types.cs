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

namespace Metin2GameServerEmulator.World.Entities
{
    internal enum EmpireFlag
    {
        // TODO Check if numbers are correct
        // I think that last two should be inverted.
        NoEmpire = 0,
        Shinsoo = 1, //A
        Chunjoo = 2, //B
        Jinno = 3, //C
        BlackEmpire = 4,
        Monsters = 5,
        NPC = 6
    }

    internal enum SexRace
    {
        WarriorMan = 0,
        NinjaWoman = 1,
        SuraMan = 2,
        ShamanWoman = 3,
        WarriorWoman = 4,
        NinjaMan = 5,
        SuraWoman = 6,
        ShamanMan = 7
    }

    internal enum EntityType
    {
        NPC = 0,
        Monster = 1,
        Player = 2,
        Other // Boh.
    }

    internal struct Position
    {
        internal Int32 X;
        internal Int32 Y;

        internal Position(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }
    }
}