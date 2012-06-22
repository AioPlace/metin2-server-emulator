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
using System.IO;
using MySql.Data.MySqlClient;

namespace Metin2ServerEmulatorCommon.DAO
{
    public class AccountDAO
    {
        private readonly Database _conn;

        public AccountDAO(Database connection)
        {
            _conn = connection;
        }

        public byte GetAccountEmpire(int id)
        {
            MySqlDataReader reader =
                _conn.Query(
                    string.Format(
                        "SELECT `empire` FROM `account` WHERE `id`='{0}'",
                        id));
            byte ret = 0;
            if (reader.Read())
                ret = reader.GetByte("empire");


            reader.Close();

            return ret;
        }

        public int GetAccountID(string username)
        {
            MySqlDataReader reader =
                _conn.Query(
                    string.Format(
                        "SELECT `id` FROM `account` WHERE `username`='{0}'",
                        username));
            int ret = 0;
            if (reader.Read())
                ret = reader.GetInt32("id");


            reader.Close();

            return ret;
        }

        public string GetAccountLogin(int id)
        {
            MySqlDataReader reader =
                _conn.Query(
                    string.Format(
                        "SELECT `username` FROM `account` WHERE `id`='{0}'",
                        id));
            string ret = "";
            if (reader.Read())
                ret = reader.GetString("username");


            reader.Close();

            return ret;
        }

        public bool VerifyLogin(string username, string password)
        {
            MySqlDataReader reader =
                _conn.Query(
                    string.Format(
                        "SELECT `id`,`username`,`password` FROM `account` WHERE `username`='{0}' AND `password`=PASSWORD('{1}')",
                        username, password));
            bool result = reader.Read();

            reader.Close();

            return result;
        }

        public Tuple<bool, bool> BanStatus(string username)
        {
            string str = string.Empty;
            MySqlDataReader reader =
                _conn.Query(
                    string.Format(
                        "SELECT `banned` FROM `account` WHERE `username`='{0}'",
                        username));
            if (reader.Read())
                str = reader.GetString("banned");
            reader.Close();
            if (string.Compare(str, "OK", StringComparison.InvariantCultureIgnoreCase) == 0)
                return new Tuple<bool, bool>(false, false);
            if (string.Compare(str, "BLOCK", StringComparison.InvariantCultureIgnoreCase) == 0)
                return new Tuple<bool, bool>(true, true);
            if (string.Compare(str, "NOTAVAIL", StringComparison.InvariantCultureIgnoreCase) == 0)
                return new Tuple<bool, bool>(true, false);
            throw new IOException("Database: unexpected value.");
        }
    }
}