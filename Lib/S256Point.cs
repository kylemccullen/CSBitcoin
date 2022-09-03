using System.Numerics;

namespace Lib;

public class S256Point : Point
{
    public S256Point(S256Field x, S256Field y)
        : base(x, y, new S256Field(Constants.A), new S256Field(Constants.B))
    {
    }

    public S256Point(string x, string y)
        : base(
                new S256Field(x.ToBigInteger(true)),
                new S256Field(y.ToBigInteger(true)),
                new S256Field(Constants.A),
                new S256Field(Constants.B)
                )
    {
    }

    public override string ToString()
    {
        return $"S256Point({X.Num}, {Y.Num})";
    }

    public static S256Point operator *(S256Point p, BigInteger coefficient)
    {
        var point = Multiply(p, MathHelper.Mod(coefficient, Constants.N));

        var x = new S256Field(point.X.Num);
        var y = new S256Field(point.Y.Num);

        return new S256Point(x, y);
    }

    public bool Verify(BigInteger z, Signature signature)
    {
        var sInv = MathHelper.Pow(signature.S, Constants.N - 2, Constants.N);
        var u = z * MathHelper.Mod(sInv, Constants.N);
        var v = signature.R * MathHelper.Mod(sInv, Constants.N);
        var total = Constants.G * u + this * v;
        return total.X.Num == signature.R;
    }

    public byte[] SECKey(bool compressed = true)
    {
        if (compressed)
        {
            var prefixByte = (byte) ((Y.Num % 2 == 0) ? 0x02 : 0x03);
            return AddBytePrefix(prefixByte, X.Num!.Value);
        }
        else
        {
            var prefixByte = (byte) 0x04;
            return AddBytePrefix(prefixByte, X.Num!.Value, Y.Num!.Value);
        }
    }

    private static byte[] AddBytePrefix(byte prefixByte, params BigInteger[] values)
    {
        var data = new List<byte>()
        {
            prefixByte
        };
        foreach (var value in values)
        {
            data.AddRange(value.ToByteArray(32, true));
        }

        return data.ToArray();
    }
}
