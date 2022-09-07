using System.Numerics;

namespace Lib;

public class S256Field : FieldElement
{
    public S256Field(BigInteger? num) : base(num, Constants.P)
    {
    }

    public S256Field Sqrt()
    {
        return S256Field.Pow(this, (Constants.P + 1) / new BigInteger(4));
    }

    public static S256Field Pow(S256Field a, BigInteger element)
    {
        var fieldElement = FieldElement.Pow(a, element);

        return new S256Field(fieldElement.Num);
    }

    public static S256Field operator +(S256Field a, S256Field b)
    {
        var fieldElement = Add(a, b);

        return new S256Field(fieldElement.Num);
    }
}
