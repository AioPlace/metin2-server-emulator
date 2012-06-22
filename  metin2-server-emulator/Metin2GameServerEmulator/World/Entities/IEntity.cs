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
    /// <summary>
    ///   Game Entity (NPCs, Players, Monsters, Stones...)
    /// </summary>
    internal interface IEntity
    {
        #region "Main"

        UInt16 Vid { get; set; } // Visual ID
        string Name { get; set; }
        byte Level { get; set; }

        #endregion

        #region "Stats"

        /// <summary>
        ///   Entity Maximum Health Points.
        /// </summary>
        Int32 MaxHP { get; set; }

        /// <summary>
        ///   Entity Current Health Points.
        /// </summary>
        Int32 CurHP { get; set; }


// ReSharper disable InconsistentNaming
        uint Strong { get; set; }
        uint Vitality { get; set; }
        uint Dexterity { get; set; }
        uint Intelligence { get; set; }

// ReSharper restore InconsistentNaming

        #endregion

        #region "Position"

        /// <summary>
        ///   Entity position in current map.
        /// </summary>
        Position RelativePosition { get; set; }

        /// <summary>
        ///   Entity Global Position.
        /// </summary>
        Position AbsolutePosition { get; set; }

        /// <summary>
        ///   Entity current Map.
        /// </summary>
        Int32 MapIndex { get; set; }

        // byte?

        #endregion

        #region "Other"

        /// <summary>
        ///   Entity last attacker
        /// </summary>
        IEntity LastAttacker { get; set; }

        /// <summary>
        ///   Entity Type
        /// </summary>
        EntityType Type { get; set; }

        EmpireFlag Empire { get; set; }

        #endregion
    }
}