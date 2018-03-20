using System;
using System.Collections;
using System.Collections.Generic;

namespace Test.Org.Interledger
{
    public static class TestUtils
    {
        public static bool IsListEqual<T>(IList<T> a, IList<T> b)
        {
            if (a != null && b != null && a == b)
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }
            if (a.Count != b.Count)
            {
                return false;
            }
            for (int index = 0; index < a.Count; index++)
            {
                if (a[index].Equals(b[index]) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] GetBytesFromHexString(string str, byte fromBase)
        {
            byte[] bytes = new byte[str.Length / 2];
            for (int index = 0; index < str.Length - 1; index += 2)
            {
                bytes[index / 2] = Convert.ToByte(str.Substring(index, 2), fromBase);
            }
            return bytes;
        }
    }
}
