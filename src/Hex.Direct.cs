// <copyright file="McNeight.Hex.Direct.cs" company="Random Developers on the Internet">
// Copyright © 2009 Brian Lambert. Copyright © 2019-2020 Neil McNeight.
// All rights reserved. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Runtime.InteropServices;

namespace McNeight
{
    /// <summary>
    /// https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa/24343727#24343727
    /// </summary>
    public static partial class Hex
    {
        private static readonly unsafe uint* Lookup32DirectP = (uint*)GCHandle.Alloc(Lookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();

        /// <summary>
        /// Returns a hex string representation of an array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes.</param>
        /// <returns>A hex string representation of the array of bytes.</returns>
        public static unsafe string ByteArrayToHexViaLookup32UnsafeDirect(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var lookupP = Lookup32DirectP;
            var result = new string((char)0, bytes.Length * 2);
            fixed (byte* bytesP = bytes)
            fixed (char* resultP = result)
            {
                var resultP2 = (uint*)resultP;
                for (var i = 0; i < bytes.Length; i++)
                {
                    resultP2[i] = lookupP[bytesP[i]];
                }
            }

            return result;
        }
    }
}
