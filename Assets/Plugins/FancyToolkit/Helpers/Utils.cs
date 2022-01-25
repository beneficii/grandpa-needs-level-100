using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace FancyUtils
{
    public static class Interfaces
    {
        public static bool Exists(object obj) => obj as Object;
    }


    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static int GetLength<T>()
        {
            return System.Enum.GetNames(typeof(T)).Length;
        }

        public static TEnum Parse<TEnum>(string s) where TEnum : struct
        {
            System.Enum.TryParse(s, true, out TEnum result);

            return result;
        }
    }

    public static class BitUtil
    {
        public static int GetBit(int num) => (1 << num);

        public static int RotateLeft(int value) // 4 bits
        {
            value = (value << 1);
            if ((value & GetBit(4)) != 0) value |= 1;
            return value & 0b1111;
        }

        public static int RotateRight(int value) // 4 bits
        {
            if ((value & 1) != 0) value |= GetBit(4);
            value = (value >> 1);
            return value & 0b1111;
        }
    }

    public static class StringUtil
    {
        public static bool TryIdSplit(string input, out string id, out string other)
        {
            var firstSpaceIndex = input.IndexOf(" ");

            if (firstSpaceIndex > 0)
            {
                id = input.Substring(0, firstSpaceIndex);
                other = input.Substring(firstSpaceIndex + 1);
            }
            else
            {
                id = input;
                other = "";
            }

            return true;
        }
    }
}


