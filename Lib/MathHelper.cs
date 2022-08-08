namespace Lib;

public static class MathHelper
{
    public static int Mod(int x, int y)
    {
        return (x % y + y) % y;
    }

    public static int Pow(int x, int y, int? modulus = null)
    {
        if (modulus is null)
        {
            return (int) Math.Pow(x, y);
        }

        var ans = x;
        for (var i = 0; i < y - 1; i++)
        {
            ans *= x;
            ans = Mod(ans, modulus.Value);
        }

        return ans;
    }
}
