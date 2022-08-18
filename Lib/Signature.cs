using System.Numerics;

namespace Lib;

public class Signature
{
    public BigInteger R { get; set; }
    public BigInteger S { get; set; }

    public Signature(BigInteger r, BigInteger s)
    {
        R = r;
        S = s;
    }

    public Signature(string r, string s)
    {
        R = MathHelper.Parse(r);
        S = MathHelper.Parse(s);
    }

    public override string ToString()
    {
        return $"Signature({R}, {S})";
    }
}
