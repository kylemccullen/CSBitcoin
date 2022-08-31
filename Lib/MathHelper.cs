using System.Globalization;
using System.Numerics;

namespace Lib;

public static class MathHelper
{
    public static BigInteger Mod(BigInteger x, BigInteger y)
    {
        return (x % y + y) % y;
    }

    public static BigInteger Pow(BigInteger x, BigInteger y, BigInteger? modulus = null)
    {
        if (modulus == null)
        {
            return BigInteger.Pow(x, (int)y);
        }

        return BigInteger.ModPow(x, y, modulus.Value);
    }

    public static BigInteger Parse(string hex)
    {
        return BigInteger.Parse(hex.Replace("x", ""), NumberStyles.HexNumber);
    }

    public static byte[] ToByteArray(this BigInteger value, int size, bool isBigEndian = false)
    {
        var byteArray = value.ToByteArray().Take(size).ToArray();
        if (isBigEndian)
        {
            Array.Reverse(byteArray, 0, byteArray.Length);
        }

        return byteArray;
    }
}
