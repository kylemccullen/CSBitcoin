using System.Numerics;

namespace Lib;

public static class Constants
{
    public static readonly BigInteger P = (BigInteger)Math.Pow(2, 256) - (BigInteger)Math.Pow(2, 32) - 977;
    public static readonly BigInteger A = 0;
    public static readonly BigInteger B = 7;
    public static readonly BigInteger N = "0xfffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141".ToBigInteger(true);
    public static readonly S256Point G = new(
            "0x79be667ef9dcbbac55a06295ce870b07029bfcdb2dce28d959f2815b16f81798".ToBigInteger(true),
            "0x483ada7726a3c4655da4fbfc0e1108a8fd17b448a68554199c47d08ffb10d4b8".ToBigInteger(true)
            );
}
