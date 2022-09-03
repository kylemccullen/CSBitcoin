using System.Numerics;
using Lib;

public class ExtensionsTest
{
    [Theory]
    [InlineData(256, false, new byte[] { 0, 1 })]
    [InlineData(256, true, new byte[] { 1, 0 })]
    public void BigInteger_ToByteArray(int value, bool isBigEndian, byte[] expected)
    {
        var bigInteger = new BigInteger(value);

        Assert.Equal(expected, bigInteger.ToByteArray(expected.Length, isBigEndian));
    }

    [Theory]
    [InlineData("0102", true, "258")]
    [InlineData("0201", false, "258")]
    [InlineData("0x887387e452b8eacc4acfde10d9aaf7f6d9a0f975aabb10d006e4da568744d06c", true, "61718672711110078285455301750480400966627255360668707636501858927943098880108")]
    [InlineData("887387e452b8eacc4acfde10d9aaf7f6d9a0f975aabb10d006e4da568744d06c", true, "61718672711110078285455301750480400966627255360668707636501858927943098880108")]
    public void Hex_ToBigInteger(string hex, bool isBigEndian, string expected)
    {
        Assert.Equal(BigInteger.Parse(expected), hex.ToBigInteger(isBigEndian));
    }
}
