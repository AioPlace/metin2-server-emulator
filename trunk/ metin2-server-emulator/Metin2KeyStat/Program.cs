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
using Metin2KeyForcer.Metin2;

namespace Metin2KeyForcer
{
    internal static class Program
    {
        private static byte[] _key;

        private static void Main(string[] args)
        {
            Console.WriteLine("Metin2 KeyForcer 0.1");
            Console.WriteLine("Part of Metin2ServerEmulator project.");
            Console.WriteLine("=====================================");
            if (args.Length != 2)
                goto InvalidArgs;
            try
            {
                _key = Win32.HexStringToByteArray(args[1]);
            }
            catch (Exception)
            {
                goto InvalidArgs;
            }
            try
            {
                KeyWriter.SetMetin2Key(args[0], _key);
                WriteSuccess("Key Froze to : " + Win32.ByteArrayToHexString(_key, true));
                while (true)
                    KeyWriter.SetMetin2Key(args[0], _key);
            }
            catch (Exception ex)
            {
                WriteErr(ex.Message);
            }

            EndProgram:
            {
                Console.ReadKey();
                return;
            }
            InvalidArgs:
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("Metin2KeyForcer <Process> <AABBCCDDEEFFAABBCCDDEEFFAABBCCDD>");
                goto EndProgram;
            }
        }

        private static void WriteErr(string buffer)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[KO] ");
            Console.ResetColor();
            Console.Write(buffer + "\r\n");
        }

        private static void WriteSuccess(string buffer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[OK] ");
            Console.ResetColor();
            Console.Write(buffer + "\r\n");
        }
    }
}