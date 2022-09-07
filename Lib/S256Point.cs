using System.Numerics;
using System.Text;

namespace Lib;

public class S256Point : Point
{
    public S256Point(S256Field x, S256Field y)
        : base(x, y, new S256Field(Constants.A), new S256Field(Constants.B))
    {
    }

    public S256Point(BigInteger x, BigInteger y)
        : base(
                new S256Field(x),
                new S256Field(y),
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

    public static S256Point Parse(byte[] secKey)
    {

        if (secKey[0] == 0x04)
        {
            var xBytes = secKey[1..33];
            var yBytes = secKey[33..65];
            return new S256Point(xBytes.ToBigInteger(true), yBytes.ToBigInteger(true));
        }

        var x = new S256Field(secKey[1..33].ToBigInteger(true));
        var alpha = S256Field.Pow(x, 3) + new S256Field(Constants.B);
        var beta = alpha.Sqrt();

        S256Field even, odd;
        if (beta.Num % 2 == 0)
        {
            even = beta;
            odd = new S256Field(Constants.P - beta.Num);
        }
        else
        {
            even = new S256Field(Constants.P - beta.Num);
            odd = beta;
        }

        if (secKey[0] == 0x02)
        {
            return new S256Point(x, even);
        }
        else
        {
            return new S256Point(x, odd);
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
