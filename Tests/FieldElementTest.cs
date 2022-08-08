using Lib;

public class FieldElementTest
{
    [Theory]
    [InlineData(10, 13)]
    [InlineData(15, 19)]
    public void FieldElement_ShouldBeCreated(int num, int prime)
    {
        var f = new FieldElement(num, prime);

        Assert.Equal(num, f.Num);
        Assert.Equal(prime, f.Prime);
    }

    [Theory]
    [InlineData(-1, 13)]
    [InlineData(15, 13)]
    public void FieldElement_ShouldThrowError(int num, int prime)
    {
        Assert.Throws<Exception>(() => new FieldElement(num, prime));
    }

    [Theory]
    [InlineData(7, 13, 7, 13)]
    public void Equals_ShouldBeTrue(int num1, int aPrime, int num2, int bPrime)
    {
        var f1 = new FieldElement(num1, aPrime);
        var f2 = new FieldElement(num2, bPrime);

        Assert.True(f1 == f2);
    }

    [Theory]
    [InlineData(7, 13, 7, 12)]
    [InlineData(7, 13, 8, 13)]
    public void Equals_ShouldBeFalse(int num1, int aPrime, int num2, int bPrime)
    {
        var f1 = new FieldElement(num1, aPrime);
        var f2 = new FieldElement(num2, bPrime);

        Assert.False(f1 == f2);
    }

    [Theory]
    [InlineData(7, 12, 6)]
    [InlineData(8, 3, 11)]
    public void Add(int num1, int num2, int expectedNum)
    {
        var f1 = new FieldElement(num1, 13);
        var f2 = new FieldElement(num2, 13);

        Assert.Equal(new FieldElement(expectedNum, 13), f1 + f2);
    }

    [Theory]
    [InlineData(7, 12, 8)]
    [InlineData(8, 3, 5)]
    public void Sub(int num1, int num2, int expectedNum)
    {
        var f1 = new FieldElement(num1, 13);
        var f2 = new FieldElement(num2, 13);

        Assert.Equal(new FieldElement(expectedNum, 13), f1 - f2);
    }

    [Theory]
    [InlineData(3, 12, 10)]
    [InlineData(6, 8, 9)]
    public void Mult(int num1, int num2, int expectedNum)
    {
        var f1 = new FieldElement(num1, 13);
        var f2 = new FieldElement(num2, 13);

        Assert.Equal(new FieldElement(expectedNum, 13), f1 * f2);
    }

    [Theory]
    [InlineData(2, 7, 3)]
    [InlineData(7, 5, 9)]
    public void Div(int num1, int num2, int expectedNum)
    {
        var f1 = new FieldElement(num1, 19);
        var f2 = new FieldElement(num2, 19);

        Assert.Equal(new FieldElement(expectedNum, 19), f1 / f2);
    }

    [Theory]
    [InlineData(3, 3, 1)]
    [InlineData(7, -3, 8)]
    public void Pow(int num, int element, int expectedNum)
    {
        var f = new FieldElement(num, 13);

        Assert.Equal(new FieldElement(expectedNum, 13), FieldElement.Pow(f, element));
    }
}
