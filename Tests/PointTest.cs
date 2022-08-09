using Lib;

public class PointTest
{
    [Theory]
    [InlineData(192, 105)]
    [InlineData(17, 56)]
    [InlineData(1, 193)]
    public void Point_IsOnCurve(int xNum, int yNum)
    {
        var a = new FieldElement(0, 223);
        var b = new FieldElement(7, 223);
        var x = new FieldElement(xNum, 223);
        var y = new FieldElement(yNum, 223);

        var ex = Record.Exception(() => new Point(x, y, a, b));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData(200, 119)]
    [InlineData(42, 99)]
    public void Point_IsNotOnCurve(int x1, int y1)
    {
        var a = new FieldElement(0, 223);
        var b = new FieldElement(7, 223);
        var x = new FieldElement(x1, 223);
        var y = new FieldElement(y1, 223);

        Assert.Throws<Exception>(() => new Point(x, y, a, b));
    }

    [Theory]
    [InlineData(192, 105, 17, 56, 170, 142)]
    [InlineData(47, 71, 117, 141, 60, 139)]
    [InlineData(143, 98, 76, 66, 47, 71)]
    [InlineData(47, 71, 47, 152, null, null)]
    public void Add(int x1, int y1, int x2, int y2, int? x3, int? y3)
    {
        var prime = 223;
        var a = new FieldElement(0, prime);
        var b = new FieldElement(7, prime);

        var p1 = new Point(new FieldElement(x1, prime), new FieldElement(y1, prime), a, b);
        var p2 = new Point(new FieldElement(x2, prime), new FieldElement(y2, prime), a, b);

        var expected = new Point(new FieldElement(x3, prime), new FieldElement(y3, prime), a, b);

        Assert.Equal(expected, p1 + p2);
    }

    [Theory]
    [InlineData(2, 192, 105, 49, 71)]
    [InlineData(2, 143, 98, 64, 168)]
    [InlineData(2, 47, 71, 36, 111)]
    [InlineData(4, 47, 71, 194, 51)]
    [InlineData(8, 47, 71, 116, 55)]
    [InlineData(20, 47, 71, 47, 152)]
    [InlineData(21, 47, 71, null, null)]
    public void Mult(int coefficient, int x1, int y1, int? x2, int? y2)
    {
        var prime = 223;
        var a = new FieldElement(0, prime);
        var b = new FieldElement(7, prime);

        var p = new Point(new FieldElement(x1, prime), new FieldElement(y1, prime), a, b);
        var expected = new Point(new FieldElement(x2, prime), new FieldElement(y2, prime), a, b);

        Assert.Equal(expected, p * coefficient);
    }
}

