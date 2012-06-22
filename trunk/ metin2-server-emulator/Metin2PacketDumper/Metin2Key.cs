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
using System.Diagnostics;

namespace Metin2PacketDumper
{
    public class Metin2Key
    {
        private const int KeyPointerHigherLevel = 0x3d070c;
        private const int Offset = 0x42;
        private readonly IntPtr _nextAddress;
        private readonly Process _proc;
        private readonly byte[][] _value;

        public Metin2Key(string process)
        {
            IntPtr ptr;
            _proc = Process.GetProcessesByName(process)[0];
            _value = new byte[2][];
            _value[0] = new byte[0x10];
            _value[1] = new byte[0x10];
            _nextAddress = (IntPtr) GetBase(process) + KeyPointerHigherLevel;
            int[] offsets = new int[] {Offset};
            IntPtr hProcess = Win32.OpenProcess(Win32.ProcessAccessFlags.Read, false, _proc.Id);

            foreach (int num in offsets)
            {
                byte[] buffer = new byte[4];
                Win32.ReadProcessMemory(hProcess, _nextAddress, buffer, 4, out ptr);
                _nextAddress = new IntPtr(BitConverter.ToInt32(buffer, 0));
                _nextAddress += num;
            }
            Win32.ReadProcessMemory(hProcess, _nextAddress, _value[0], 0x10, out ptr);
            Win32.ReadProcessMemory(hProcess, _nextAddress + 0x10, _value[1], 0x10, out ptr);
        }


        private static long GetBase(string process)
        {
            Process proc = Process.GetProcessesByName(process)[0];
            return proc.MainModule.BaseAddress.ToInt64();
        }

        /// <summary>
        ///   Write Metin2 Key
        /// </summary>
        /// <param name="key"> KEY to write (16byte) </param>
        /// <param name="keynum"> 1 - Key1, 2 - Key2, 0 - All </param>
        public void Write(byte[] key, byte keynum = 0)
        {
            UIntPtr ptr;
            IntPtr hProcess = Win32.OpenProcess(Win32.ProcessAccessFlags.All, false, _proc.Id);
            Win32.WriteProcessMemory(hProcess, _nextAddress, key, 0x10, out ptr);
            Win32.WriteProcessMemory(hProcess, _nextAddress + 0x10, key, 0x10, out ptr);
        }

        /// <summary>
        ///   Read Metin2 Key
        /// </summary>
        /// <returns> </returns>
        public byte[][] Read()
        {
            return _value;
        }
    }
}