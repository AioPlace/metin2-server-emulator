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
using System.Data;
using MySql.Data.MySqlClient;

namespace Metin2ServerEmulatorCommon
{
    public class Database
    {
        private readonly string _database;
        private readonly string _host;
        private readonly Logger _log;
        private readonly string _password;
        private readonly string _username;
        private MySqlConnection _connection;

        public Database(string host, string username, string password, string database, Logger log)
        {
            _log = log;
            _host = host;
            _username = username;
            _password = password;
            _database = database;
        }

        public bool Connected
        {
            get { return _connection.State == ConnectionState.Open; }
        }

        public bool Connect()
        {
            _connection = new MySqlConnection();
            try
            {
                _connection.ConnectionString = ("Server=" + _host + "; Database=" + _database + "; Uid=" +
                                                _username + "; Pwd=" + _password + ";");
                _connection.Open();
                _log.Info("Connected to Database");
                return true;
            }
            catch (Exception ex)
            {
                _log.Critical("Error While connecting to database; ex:" + ex);
                _connection.Close();
                return false;
            }
        }

        public MySqlDataReader Query(string queryString)
        {
            if (!Connected)
                return null;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = queryString;
            return cmd.ExecuteReader();
        }

        public void Disonnect()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}