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
                new S256Field(MathHelper.Parse(x)),
                new S256Field(MathHelper.Parse(y)),
                new S256Field(Constants.A),
                new S256Field(Constants.B)
                )
    {
    }

    public static S256Point operator *(S256Point p, BigInteger coefficient)
    {
        var point = Multiply(p, MathHelper.Mod(coefficient, Constants.N));

        var x = new S256Field(point.X.Num);
        var y = new S256Field(point.Y.Num);

        return new S256Point(x, y);
    }
}
