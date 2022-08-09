using System.Numerics;

namespace Lib;

public class S256Field : FieldElement
{
    public S256Field(BigInteger? num) : base(num, Constants.P)
    {
    }
}
