using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Lib;

public class PrivateKey
{
    public BigInteger Secret { get; set; }
    public S256Point Point { get; set; }

    public PrivateKey(BigInteger secret)
    {
        Secret = secret;
        Point = Constants.G * secret;
    }

    public Signature Sign(BigInteger z)
    {
        var k = DeterministicK(z);
        var r = (Constants.G * k).X.Num;
        var kInv = MathHelper.Pow(k, Constants.N - 2, Constants.N);
        var s = MathHelper.Mod((z + r!.Value * Secret) * kInv, Constants.N);
        
        if (s > Constants.N / 2)
        {
            s -= Constants.N;
        }

        return new Signature(r.Value, s);
    }

    private BigInteger DeterministicK(BigInteger z)
    {
        var k = GetBytes(32, 0x00);
        var v = GetBytes(32, 0x01);
        if (z > Constants.N)
        {
            z -= Constants.N;
        }
        var z_bytes = z.ToByteArray(32, true);

        var secret_bytes = Secret.ToByteArray(32, true);
        k = HmacSHA256_(k, v, GetBytes(1, 0x00), secret_bytes, z_bytes);
        v = HmacSHA256_(k, v);
        k = HmacSHA256_(k, v, GetBytes(1, 0x01), secret_bytes, z_bytes);
        v = HmacSHA256_(k, v);

        while (true)
        {
            v = HmacSHA256_(k, v);
            var canidate = new BigInteger(v);
            if (canidate >= 1 && canidate < Constants.N)
            {
                return canidate;
            }

            k = HmacSHA256_(k, v, GetBytes(1, 0x00));
            v = HmacSHA256_(k, v);
        }
    }

    private static byte[] HmacSHA256_(byte[] key, params byte[][] data)
    {
        var dataConcat = new List<byte>();
        foreach (var item in data)
        {
            dataConcat.Concat(item);
        }

        return HmacSHA256(key, dataConcat.ToArray());
    }

    private static byte[] HmacSHA256(byte[] key, byte[] data)
    {
        var keyString = new BigInteger(key).ToString("X");
        var dataString = new BigInteger(data).ToString("X");
        return HmacSHA256(keyString, dataString);
    }

    private static byte[] HmacSHA256(string key, string data)
    {
        Byte[] hmBytes;
        ASCIIEncoding encoder = new ASCIIEncoding();
        Byte[] code = encoder.GetBytes(key);
        using (HMACSHA256 hmac = new HMACSHA256(code))
        {
            hmBytes = hmac.ComputeHash(encoder.GetBytes(data));
        }
        return hmBytes;
    }

    private static byte[] GetBytes(int size, byte value)
    {
        var bytes = new byte[size];
        for (var i = 0; i < size; i++)
        {
            bytes[i] = value;
        }

        return bytes;
    }
}
