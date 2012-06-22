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

namespace Metin2ServerEmulatorCommon.Util
{
    public static class XTEA
    {
        #region "Encrypt"

        /// <summary>
        ///   Encrypts a byte[]
        /// </summary>
        /// <param name="input"> Data do encrypt </param>
        /// <param name="key"> Encryption key </param>
        /// <param name="numRounds"> Rounds count </param>
        /// <returns> Encrypted data </returns>
        public static byte[] Encrypt(byte[] input, byte[] key, uint numRounds = 32)
        {
            int size = input.Length;
            int rToDo = size/8;
            int rCounter = 0; // round counter
            uint[] result = new uint[size/4];

            uint[] k = ByteSupport.ByteArrayToUintArray(key);
            uint[] inp = ByteSupport.ByteArrayToUintArray(input);

            uint c = 0, c1 = 1;

            while (rCounter != rToDo)
            {
                uint[] buf = {inp[c], inp[c1]};

                EncryptStep(numRounds, ref buf, k);

                result[c] = buf[0];
                result[c1] = buf[1];

                c += 2;
                c1 += 2;

                rCounter++;
            }

            return ByteSupport.UintArrayToByteArray(result);
        }

        private static void EncryptStep(uint numRounds, ref uint[] v, uint[] key)
        {
            uint v0 = v[0], v1 = v[1], sum = 0;
            const uint delta = 0x9E3779B9;
            for (uint i = 0; i < numRounds; i++)
            {
                v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
                sum += delta;
                v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
            }
            v[0] = v0;
            v[1] = v1;
        }

        #endregion //Encrypt

        #region "Decrypt"

        /// <summary>
        ///   Decrypts a byte[]
        /// </summary>
        /// <param name="input"> Data to decrypt </param>
        /// <param name="key"> Encryption key </param>
        /// <param name="numRounds"> Rounds count </param>
        /// <returns> Dectypted data </returns>
        public static byte[] Decrypt(byte[] input, byte[] key, uint numRounds = 32)
        {
            int size = input.Length;
            int rToDo = size/8;
            int rCounter = 0; // round counter
            uint[] result = new uint[size/4];

            uint[] k = ByteSupport.ByteArrayToUintArray(key);
            uint[] inp = ByteSupport.ByteArrayToUintArray(input);

            uint c = 0, c1 = 1;

            while (rCounter != rToDo)
            {
                uint[] buf = {inp[c], inp[c1]};

                DecryptStep(numRounds, ref buf, k);

                result[c] = buf[0];
                result[c1] = buf[1];

                c += 2;
                c1 += 2;

                rCounter++;
            }

            return ByteSupport.UintArrayToByteArray(result);
        }


        private static void DecryptStep(uint numRounds, ref uint[] v, uint[] key)
        {
            uint v0 = v[0], v1 = v[1];
            const uint delta = 0x9E3779B9;
            uint sum = delta*numRounds;
            for (uint i = 0; i < numRounds; i++)
            {
                v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
                sum -= delta;
                v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
            }
            v[0] = v0;
            v[1] = v1;
        }

        #endregion //Decrypt
    }
}