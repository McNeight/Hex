// <copyright file="McNeight.Hex.cs" company="Random Developers on the Internet">
// Copyright © 2009 Brian Lambert. Copyright © 2019-2020 Neil McNeight.
// All rights reserved. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;

namespace McNeight
{
    /// <summary>
    /// https://stackoverflow.com/a/624379
    /// </summary>
    public static partial class Hex
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] Bytes)
        {
            var Result = new StringBuilder(Bytes.Length * 2);
            var HexAlphabet = "0123456789ABCDEF";

            foreach (var B in Bytes)
            {
                Result.Append(HexAlphabet[B >> 4]);
                Result.Append(HexAlphabet[B & 0xF]);
            }

            return Result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string Hex)
        {
            var Bytes = new byte[Hex.Length / 2];
            var HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
                    0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)((HexValue[char.ToUpper(Hex[i + 0]) - '0'] << 4) |
                                  HexValue[char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }
    }
}
