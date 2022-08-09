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
}
