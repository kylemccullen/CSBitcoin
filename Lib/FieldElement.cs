namespace Lib;

public class FieldElement
{
    public int Num { get; set; }
    public int Prime { get; set; }

    public FieldElement(int num, int prime)
    {
        if (num < 0 || num > prime)
        {
            throw new Exception($"{num} not in field range 0 to {prime}");
        }

        Num = num;
        Prime = prime;
    }

    public override string ToString()
    {
        return $"FieldElement_{Prime}({Num})";
    }

    public override bool Equals(object? obj) => this.Equals(obj as FieldElement);

    public bool Equals(FieldElement? f)
    {
        if (f is null)
            return false;

        if (Object.ReferenceEquals(this, f))
            return true;

        if (this.GetType() != f.GetType())
            return false;

        return Num == f.Num && Prime == f.Prime;
    }

    public static bool operator ==(FieldElement a, FieldElement b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(FieldElement a, FieldElement b)
    {
        return !a.Equals(b);
    }

    public static FieldElement operator +(FieldElement a, FieldElement b)
    {
        if (a.Prime != b.Prime)
            throw new Exception("Cannot add numbers of different Fields!");

        var num = MathHelper.Mod(a.Num + b.Num, a.Prime);
        return new FieldElement(num, a.Prime);
    }

    public static FieldElement operator -(FieldElement a, FieldElement b)
    {
        if (a.Prime != b.Prime)
            throw new Exception("Cannot subtract numbers of different Fields!");

        var num = MathHelper.Mod(a.Num - b.Num, a.Prime);
        return new FieldElement(num, a.Prime);
    }

    public static FieldElement operator *(FieldElement a, FieldElement b)
    {
        if (a.Prime != b.Prime)
            throw new Exception("Cannot multiply numbers of different Fields!");
        var num = MathHelper.Mod(a.Num * b.Num, a.Prime);
        return new FieldElement(num, a.Prime);
    }

    public static FieldElement operator *(FieldElement a, int b)
    {
        var num = MathHelper.Mod(a.Num * b, a.Prime);
        return new FieldElement(num, a.Prime);
    }

    public static FieldElement operator /(FieldElement a, FieldElement b)
    {
        if (a.Prime != b.Prime)
            throw new Exception("Cannot divide numbers of different Fields!");
        var num = MathHelper.Mod(a.Num * MathHelper.Pow(b.Num, a.Prime - 2, a.Prime), a.Prime);
        return new FieldElement(num, a.Prime);
    }

    public static FieldElement Pow(FieldElement a, int element)
    {
        var n = element;
        while (n < 0)
            n += a.Prime - 1;
        var num = MathHelper.Pow(a.Num, n, a.Prime);
        return new FieldElement(num, a.Prime);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}
