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

namespace Metin2ServerEmulatorCommon
{
    public class Logger
    {
        // File Name
        private const string Log = "log.txt";
        private const string SystemLog = "systemLog.txt";
        private const string PacketLog = "packetLog.txt";
        private const string WarnLog = "warnLog.txt";
        private const string ErrorLog = "errorLog.txt";
        private const string CritLog = "critLog.txt";

        private readonly StreamWriter _critLog;
        private readonly StreamWriter _errorLog;
        private readonly StreamWriter _log;
        private readonly string _logFolder;
        private readonly StreamWriter _packetLog;
        private readonly StreamWriter _systemLog;
        private readonly StreamWriter _warnLog;

        /// <summary>
        ///   Logger constructor
        /// </summary>
        /// <param name="logFolder"> Folder to save logs in </param>
        public Logger(string logFolder)
        {
            _logFolder = logFolder;
            CheckForFiles();

            _log = new StreamWriter(Path.Combine(_logFolder, Log));
            _systemLog = new StreamWriter(Path.Combine(_logFolder, SystemLog));
            _packetLog = new StreamWriter(Path.Combine(_logFolder, PacketLog));
            _warnLog = new StreamWriter(Path.Combine(_logFolder, WarnLog));
            _errorLog = new StreamWriter(Path.Combine(_logFolder, ErrorLog));
            _critLog = new StreamWriter(Path.Combine(_logFolder, CritLog));

            _log.AutoFlush = true;
            _systemLog.AutoFlush = true;
            _packetLog.AutoFlush = true;
            _warnLog.AutoFlush = true;
            _errorLog.AutoFlush = true;
            _critLog.AutoFlush = true;
        }

        public void Stop()
        {
            Info("Server Closing");
            _log.Close();
            _systemLog.Close();
            _packetLog.Close();
            _warnLog.Close();
            _errorLog.Close();
            _critLog.Close();
        }

        /// <summary>
        ///   Writes an info log
        /// </summary>
        /// <param name="message"> Message to log </param>
        public void Info(string message)
        {
            string text = message;
            text = "[" + DateTime.Now + "][INFO] " + text;
            _log.WriteLine(text);
            Console.WriteLine(text);
        }

        /// <summary>
        ///   Writes a packet log. No output on console
        /// </summary>
        /// <param name="message"> Message to log </param>
        public void Packet(string message)
        {
            string text = message;
            text = "[" + DateTime.Now + "][PACKET] " + text;
            _packetLog.WriteLine(text);
        }

        /// <summary>
        ///   Writes a packet log. Outputs to console
        /// </summary>
        /// <param name="message"> Message to log </param>
        public void PacketConsole(string message)
        {
            string text = message;
            text = "[" + DateTime.Now + "][PACKET] " + text;
            _packetLog.WriteLine(text);
            _log.WriteLine(text);
            Console.WriteLine(text);
        }

        /// <summary>
        ///   Writes a warning log
        /// </summary>
        /// <param name="message"> Message to log </param>
        public void Warn(string message)
        {
            string text = message;
            text = "[" + DateTime.Now + "][WARNING] " + text;
            _log.WriteLine(text);
            _warnLog.WriteLine(text);
            Console.WriteLine(text);
        }

        /// <summary>
        ///   Writes an error log
        /// </summary>
        /// <param name="message"> Message to log </param>
        public void Error(string message)
        {
            string text = message;
            text = "[" + DateTime.Now + "][ERROR] " + text;
            _log.WriteLine(text);
            _warnLog.WriteLine(text);
            _errorLog.WriteLine(text);
            Console.WriteLine(text);
        }

        /// <summary>
        ///   Writes a critical log
        /// </summary>
        /// <param name="message"> Message to log </param>
        public void Critical(string message)
        {
            string text = message;
            text = "[" + DateTime.Now + "][CRITICAL] " + text;
            _log.WriteLine(text);
            _warnLog.WriteLine(text);
            _errorLog.WriteLine(text);
            _critLog.WriteLine(text);
            Console.WriteLine(text);
        }

        /// <summary>
        ///   Writes a system (console message) log
        /// </summary>
        /// <param name="message"> Message to log </param>
        public void System(string message)
        {
            string text = message;
            text = "[" + DateTime.Now + "][SYSTEM] " + text;
            _systemLog.WriteLine(text);
            Console.WriteLine(text);
        }

        /// <summary>
        ///   Check for logs file, if exist delete
        /// </summary>
        private void CheckForFiles()
        {
            if (Directory.Exists(_logFolder))
            {
                if (File.Exists(Path.Combine(_logFolder, Log)))
                {
                    File.Delete(Path.Combine(_logFolder, Log));
                }

                if (File.Exists(Path.Combine(_logFolder, SystemLog)))
                {
                    File.Delete(Path.Combine(_logFolder, SystemLog));
                }

                if (File.Exists(Path.Combine(_logFolder, PacketLog)))
                {
                    File.Delete(Path.Combine(_logFolder, PacketLog));
                }

                if (File.Exists(Path.Combine(_logFolder, WarnLog)))
                {
                    File.Delete(Path.Combine(_logFolder, WarnLog));
                }

                if (File.Exists(Path.Combine(_logFolder, ErrorLog)))
                {
                    File.Delete(Path.Combine(_logFolder, ErrorLog));
                }

                if (File.Exists(Path.Combine(_logFolder, CritLog)))
                {
                    File.Delete(Path.Combine(_logFolder, CritLog));
                }
            }
            else
            {
                Directory.CreateDirectory(_logFolder);
            }
        }
    }
}