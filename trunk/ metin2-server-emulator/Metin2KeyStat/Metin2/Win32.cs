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
using System.Linq;
using System.Runtime.InteropServices;

namespace Metin2KeyForcer.Metin2
{
    internal static class Win32
    {
        #region ProcessAccessFlags enum

        public enum ProcessAccessFlags : uint
        {
            VMRead = 0x10,
            VMWrite = 0x20,
            All = 0x001F0FFF
        }

        #endregion

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess,
                                                [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] buffer, uint size,
                                                   out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize,
                                                     out UIntPtr lpNumberOfBytesWritten);

        internal static string ByteArrayToHexString(byte[] buffer, bool spaces = false)
        {
            string buffer2 = "";

            foreach (byte b in buffer)
            {
                int c = Convert.ToInt32(b);
                if (c == 0)
                {
                    buffer2 += "00";
                    if (spaces)
                        buffer2 += " ";
                    continue;
                }
                string s = Convert.ToString(c, 16);

                if (c < 0x10)
                    s = "0" + s;
                buffer2 += s;
                if (spaces)
                    buffer2 += " ";
            }
            return buffer2.ToUpper();
        }

        public static byte[] HexStringToByteArray(string hexString)
        {
            if (hexString.Length%2 != 0)
                throw new Exception("INVALID_HEX_STRING");

            return Enumerable.Range(0, hexString.Length)
                .Where(x => x%2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }
    }
}