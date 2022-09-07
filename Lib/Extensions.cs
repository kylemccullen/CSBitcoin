using System.Globalization;
using System.Numerics;

namespace Lib;

public static class Extensions
{
    public static byte[] ToByteArray(this BigInteger value, int size, bool isBigEndian = false)
    {
        var byteArray = value.ToByteArray().Take(size).ToArray();
        if (isBigEndian)
        {
            Array.Reverse(byteArray, 0, byteArray.Length);
        }

        return byteArray;
    }

    public static BigInteger ToBigInteger(this string value, bool isBigEndian = false)
    {
        if (value[0..2] == "0x")
        {
            value = value.Replace("x", "");
        }
        else
        {
            value = "0" + value;
        }

        if (!isBigEndian)
        {
            value = ReverseBytes(value);
        }

        return BigInteger.Parse(value, NumberStyles.HexNumber);
    }

    public static BigInteger ToBigInteger(this byte[] value, bool isBigEndian = false)
    {
        if (value[0] != 0x00)
        {
            value = new byte[] { 0x00 }.Concat(value).ToArray();
        }

        if (isBigEndian)
        {
            Array.Reverse(value, 0, value.Length);
        }

        return new BigInteger(value);
    }

    public static byte[] ToByteArray(this string value, bool isBigEndian)
    {
        var bigInteger = value.ToBigInteger(isBigEndian);
        var size = value.Replace("0x", "").Length / 2;
        return bigInteger.ToByteArray(size, isBigEndian);
    }

    public static string ReverseBytes(this string value)
    {
        if (value.Length % 2 == 1)
        {
            value = value[1..value.Length];
        }
        var result = new List<string>();
        for (var i = 0; i < value.Length; i += 2)
        {
            result.Insert(0, value[i..(i+2)]);
        }

        return string.Join("", result);
    }
}
