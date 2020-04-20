// <copyright file="McNeight.Hex.Safe.cs" company="Random Developers on the Internet">
// Copyright © 2009 Brian Lambert. Copyright © 2019-2020 Neil McNeight.
// All rights reserved. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Globalization;

namespace McNeight
{
    /// <summary>
    /// https://stackoverflow.com/a/24343727/48700
    /// </summary>
    public static partial class Hex
    {
        private static readonly uint[] Lookup32 = CreateLookup32();

        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var s = i.ToString("X2", CultureInfo.InvariantCulture);
                result[i] = s[0] + ((uint)s[1] << 16);
            }

            return result;
        }

        /// <summary>
        /// Returns a hex string representation of an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes.</param>
        /// <returns>A hex string representation of the array of bytes.</returns>
        public static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var lookup32 = Hex.Lookup32;
            var result = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[(2 * i) + 1] = (char)(val >> 16);
            }

            return new string(result);
        }
    }
}
