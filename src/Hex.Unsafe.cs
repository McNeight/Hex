// <copyright file="Hex.Unsafe.cs" company="Random Developers on the Internet">
// Copyright © 2009 Brian Lambert. Copyright © 2019-2020 Neil McNeight.
// All rights reserved. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace McNeight
{
    /// <summary>
    /// https://stackoverflow.com/a/24343727/48700
    /// </summary>
    public static partial class Hex
    {
        private static readonly uint[] Lookup32Unsafe = CreateLookup32Unsafe();
        private static readonly unsafe uint* Lookup32UnsafeP = (uint*)GCHandle.Alloc(Lookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();

        private static uint[] CreateLookup32Unsafe()
        {
            var result = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var s = i.ToString("X2", CultureInfo.InvariantCulture);
                if (BitConverter.IsLittleEndian)
                {
                    result[i] = s[0] + ((uint)s[1] << 16);
                }
                else
                {
                    result[i] = s[1] + ((uint)s[0] << 16);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a hex string representation of an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes.</param>
        /// <returns>A hex string representation of the array of bytes.</returns>
        public static unsafe string ByteArrayToHexViaLookup32Unsafe(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var lookupP = Lookup32UnsafeP;
            var result = new char[bytes.Length * 2];
            fixed (byte* bytesP = bytes)
            fixed (char* resultP = result)
            {
                var resultP2 = (uint*)resultP;
                for (var i = 0; i < bytes.Length; i++)
                {
                    resultP2[i] = lookupP[bytesP[i]];
                }
            }

            return new string(result);
        }
    }
}
