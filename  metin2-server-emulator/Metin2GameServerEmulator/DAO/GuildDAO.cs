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

using Metin2GameServerEmulator.World;
using Metin2ServerEmulatorCommon;
using MySql.Data.MySqlClient;

namespace Metin2GameServerEmulator.DAO
{
    internal class GuildDAO
    {
        private readonly Database _conn;

        internal GuildDAO(Database connection)
        {
            _conn = connection;
        }

        internal Guild GetGuild(uint id)
        {
            Guild guild = null;
            string query = string.Format(
                "SELECT * FROM `guild` WHERE `id`='{0}'", id);
            MySqlDataReader reader = _conn.Query(query);
            if (reader.Read())
            {
                guild = new Guild
                            {
                                ID = reader.GetUInt32("id"),
                                Name = reader.GetString("name"),
                                Empire = reader.GetByte("empire"),
                                Master = reader.GetUInt32("master"),
                                Wins = reader.GetUInt32("wins"),
                                Loses = reader.GetUInt32("loses"),
                                Draws = reader.GetUInt32("draws"),
                                Level = reader.GetByte("level"),
                                Mana = reader.GetUInt32("mana"),
                                SkillPoints = reader.GetUInt16("skill_points")
                            };
            }
            reader.Close();

            return guild;
        }

        internal Guild GetGuildByMaster(uint id)
        {
            MySqlDataReader reader =
                _conn.Query(string.Format("SELECT `id` FROM `guild` WHERE `master`='{0}'", id));
            uint guildID = reader.GetUInt32("id");
            reader.Close();
            return GetGuild(guildID);
        }

        internal Guild GetGuild(string name)
        {
            MySqlDataReader reader =
                _conn.Query(string.Format("SELECT `id` FROM `guild` WHERE `name`='{0}'", name));
            uint guildID = reader.GetUInt32("id");
            reader.Close();
            return GetGuild(guildID);
        }

        internal string GetGuildName(uint id)
        {
            string guildName = string.Empty;
            MySqlDataReader reader =
                _conn.Query(string.Format("SELECT `name` FROM `guild` WHERE `id`='{0}'", id));
            if (reader.Read())
                guildName = reader.GetString("name");
            reader.Close();
            return guildName;
        }
    }
}