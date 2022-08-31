using Lib;
using System.Numerics;

public class S256PointTest
{
    [Theory]
    [InlineData("7", "0x5cbdf0646e5db4eaa398f365f2ea7a0e3d419b7e0330e39ce92bddedcac4f9bc", "0x6aebca40ba255960a3178d6d861a54dba813d0b813fde7b5a5082628087264da")]
    [InlineData("1485", "0xc982196a7466fbbbb0e27a940b6af926c1a74d5ad07128c82824a11b5398afda", "0x7a91f9eae64438afb9ce6448a1c133db2d8fb9254e4546b6f001637d50901f55")]
    [InlineData("340282366920938463463374607431768211456", "0x8f68b9d2f63b5f339239c1ad981f162ee88c5678723ea3351b7b444c9ec4c0da", "0x662a9f2dba063986de1d90c2b6be215dbbea2cfe95510bfdf23cbf79501fff82")]
    [InlineData("1766847064778384329583297500742918515827483896875618958121606203440103424", "0x9577ff57c8234558f293df502ca4f09cbc65a6572c842b39b366f21717945116", "0x10b49c67fa9365ad7b90dab070be339a1daf9052373ec30ffae4f72d5e66d053")]
    public void PublicPoint(string secret, string x, string y)
    {
        var p = new S256Point(x, y);

        Assert.Equal(p, Constants.G * BigInteger.Parse(secret));
    }

    [Theory]
    [InlineData(
            "0x887387e452b8eacc4acfde10d9aaf7f6d9a0f975aabb10d006e4da568744d06c",
            "0x61de6d95231cd89026e286df3b6ae4a894a3378e393e93a0f45b666329a0ae34",
            "0xec208baa0fc1c19f708a9ca96fdeff3ac3f230bb4a7ba4aede4942ad003c0f60",
            "0xac8d1c87e51d0d441be8b3dd5b05c8795b48875dffe00b7ffcfac23010d3a395",
            "0x68342ceff8935ededd102dd876ffd6ba72d6a427a3edb13d26eb0781cb423c4")]
    public void Verify(string x, string y, string z, string r, string s)
    {
        var p = new S256Point(x, y);
        Assert.True(p.Verify(MathHelper.Parse(z), new Signature(r, s)));
    }

    [Theory]
    [InlineData(
            "997002999",
            "049d5ca49670cbe4c3bfa84c96a8c87df086c6ea6a24ba6b809c9de234496808d56fa15cc7f3d38cda98dee2419f415b7513dde1301f8643cd9245aea7f3f911f9",
            "039d5ca49670cbe4c3bfa84c96a8c87df086c6ea6a24ba6b809c9de234496808d5")]
    [InlineData(
            "123",
            "04a598a8030da6d86c6bc7f2f5144ea549d28211ea58faa70ebf4c1e665c1fe9b5204b5d6f84822c307e4b4a7140737aec23fc63b65b35f86a10026dbd2d864e6b",
            "03a598a8030da6d86c6bc7f2f5144ea549d28211ea58faa70ebf4c1e665c1fe9b5"
            )]
    public void SECKey(string coefficient, string uncompressed, string compressed)
    {
        var p = Constants.G * BigInteger.Parse(coefficient);

        Assert.Equal(uncompressed, ByteHelper.ToHexString(p.SECKey(false)));
        Assert.Equal(compressed, ByteHelper.ToHexString(p.SECKey()));
    }
}
