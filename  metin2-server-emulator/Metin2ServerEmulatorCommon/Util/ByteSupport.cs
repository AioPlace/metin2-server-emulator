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

namespace Metin2ServerEmulatorCommon.Util
{
    public static class ByteSupport
    {        
        /// <summary>
        ///   Converts a Byte Array to an Unsigned Int32 Array.
        /// </summary>
        /// <param name="input"> Byte array to convert. Its length should be a multiple of 4. </param>
        /// <returns> </returns>
        public static uint[] ByteArrayToUintArray(byte[] input)
        {
            int i = 0, c = 0;
            byte[] buffer = new byte[4];
            uint[] ret = new uint[input.Length/4];
            foreach (byte b in input)
            {
                buffer[i] = b;
                i++;
                if (i >= 4)
                {
                    i = 0;
                    ret[c] = BitConverter.ToUInt32(buffer, 0);
                    buffer = new byte[4];
                    c++;
                }
            }
            return ret;
        }

        /// <summary>
        ///   Converts an Unsigned Int32 array to a Byte Array.
        /// </summary>
        /// <param name="input"> Unsigned Int32 array </param>
        /// <returns> Returns a byte[] which elements are 4 times more than the uint[]. </returns>
        public static byte[] UintArrayToByteArray(uint[] input)
        {
            byte[] ret = new byte[input.Length*4];
            uint c = 0;
            foreach (uint b in input)
            {
                foreach (byte bytese in BitConverter.GetBytes(b))
                {
                    ret[c] = bytese;
                    c++;
                }
            }
            return ret;
        }

        /// <summary>
        ///   Converts a byte array to the relative HEX string.
        /// </summary>
        /// <param name="buffer"> Byte array to convert. </param>
        /// <param name="spaces"> If `true`, code will add spaces between bytes. </param>
        /// <returns> </returns>
        public static string ByteArrayToHexString(byte[] buffer, bool spaces = false)
        {
            if (buffer == null)
                return "";
            return BitConverter.ToString(buffer).Replace("-", spaces ? " " : "");
        }


        /// <summary>
        ///   Converts a HEX String to Byte Array
        /// </summary>
        /// <param name="hexString"> HEX String without spaces. His length must be a multiple of two. </param>
        /// <returns> </returns>
        public static byte[] HexStringToByteArray(string hexString)
        {
            if (hexString.Length%2 != 0)
                throw new Exception("INVALID_HEX_STRING");

            return Enumerable.Range(0, hexString.Length)
                .Where(x => x%2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }

        /// <summary>
        ///   Converts a ASCII String to a Byte array.
        /// </summary>
        /// <param name="asciiString"> String with ASCII encoding. </param>
        /// <returns> </returns>
        public static byte[] AsciiStringToByteArray(string asciiString)
        {
            return
                Enumerable.Range(0, asciiString.Length).Select(x => Convert.ToByte(asciiString.ToCharArray()[x])).
                    ToArray();
        }

        /// <summary>
        ///   Converts a Byte Array to ASCII String.
        /// </summary>
        /// <param name="buffer"> Byte array to convert. </param>
        /// <returns> </returns>
        public static string ByteArrayToAsciiString(byte[] buffer)
        {
            if (buffer == null)
                return "";
            string b = "";
            foreach (byte t in buffer)
            {
                if (Convert.ToInt32(t) < 32)
                {
                    b += ".";
                    continue;
                }
                b += (char) t;
            }
            return b;
        }

        /// <summary>
        ///   (EXPERIMENTAL) Converts a bitmask to Boolean Array.
        /// </summary>
        /// <param name="bitMask"> Bitmask as int </param>
        /// <returns> BooleanArray </returns>
        public static bool[] BitMasktoBoolArray(byte bitMask)
        {
            string binary = Convert.ToString(bitMask, 2);
            do
            {
                binary = "0" + binary;
            } while (binary.Length < 8);
            bool[] ret = new bool[binary.Length];
            for (int i = 0; i < binary.Length; i++)
                ret[i] = (binary[i] == "1"[0]);
            return ret;
        }
    }
}