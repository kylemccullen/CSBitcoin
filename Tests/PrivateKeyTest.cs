using Lib;

public class PrivateKeyTest
{
    [Fact]
    public void Sign()
    {
        var pk = new PrivateKey(156);
        var z = 167;
        var sig = pk.Sign(z);
        Assert.True(pk.Point.Verify(z, sig));
    }
}
