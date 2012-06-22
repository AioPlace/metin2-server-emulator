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

namespace Metin2KeyForcer.Metin2
{
    internal class Metin2Key
    {
        private readonly IntPtr _nextAddress;
        private readonly Process _proc;

        public Metin2Key(Process process, IntPtr baseAddress, int[] offsets = null)
        {
            _proc = process;
            _nextAddress = baseAddress;
            IntPtr hProcess = Win32.OpenProcess(Win32.ProcessAccessFlags.VMRead, false, process.Id);
            if ((offsets != null) && (offsets.Length > 0))
            {
                foreach (int num in offsets)
                {
                    byte[] buffer = new byte[4];
                    IntPtr ptr;
                    Win32.ReadProcessMemory(hProcess, _nextAddress, buffer, 4, out ptr);
                    _nextAddress = new IntPtr(BitConverter.ToInt32(buffer, 0));
                    _nextAddress += num;
                }
            }
        }

        public void WriteKey(byte[] key)
        {
            UIntPtr ptr;
            IntPtr hProcess = Win32.OpenProcess(Win32.ProcessAccessFlags.All, false, _proc.Id);
            Win32.WriteProcessMemory(hProcess, _nextAddress, key, 0x10, out ptr);
            Win32.WriteProcessMemory(hProcess, _nextAddress + 0x10, key, 0x10, out ptr);
        }

/*
        public int GetAddress()
        {
            return _nextAddress.ToInt32();
        }
*/
    }
}