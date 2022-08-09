namespace Lib;

public class Point
{
    public FieldElement X { get; set; }
    public FieldElement Y { get; set; }
    public FieldElement A { get; set; }
    public FieldElement B { get; set; }

    public Point(FieldElement x, FieldElement y, FieldElement a, FieldElement b)
    {
        X = x;
        Y = y;
        A = a;
        B = b;

        if (x.Num is null && y.Num is null)
            return;

        if (FieldElement.Pow(y, 2) != FieldElement.Pow(x, 3) + (a * x) + b)
            throw new Exception($"({x}, {y}) is not on the curve");
    }

    public override string ToString()
    {
        return $"Point_{A}_{B}({X}, {Y})";
    }

    public override bool Equals(object? obj) => this.Equals(obj as Point);

    public bool Equals(Point? p)
    {
        if (p is null)
            return false;

        if (Object.ReferenceEquals(this, p))
            return true;

        if (this.GetType() != p.GetType())
            return false;

        return X == p.X && Y == p.Y && A == p.A && B == p.B;
    }

    public static bool operator ==(Point p1, Point p2)
    {
        return p1.Equals(p2);
    }

    public static bool operator !=(Point p1, Point p2)
    {
        return !p1.Equals(p2);
    }

    public static Point operator +(Point p1, Point p2)
    {
        if (p1.A != p2.A || p1.B != p2.B)
            throw new Exception($"Points ({p1}, {p2}) are not on the same curve.");

        if (p1.X.Num is null)
            return p2;

        if (p2.X.Num is null)
            return p1;

        if (p1.X == p2.X && p1.Y != p2.Y)
            return NewNullPoint(p1);

        if (p1 != p2)
        {
            var slope = (p2.Y - p1.Y) / (p2.X - p1.X);
            var x = FieldElement.Pow(slope, 2) - p1.X - p2.X;
            var y = slope * (p1.X - x) - p1.Y;
            return new Point(x, y, p1.A, p1.B);
        }

        if (p1 == p2 && p1.Y == p1.X * 0)
            return NewNullPoint(p1);

        if (p1 == p2)
        {
            var slope = (FieldElement.Pow(p1.X, 2) * 3 + p1.A) / (p1.Y * 2);
            var x = FieldElement.Pow(slope, 2) - p1.X - p2.X;
            var y = slope * (p1.X - x) - p1.Y;
            return new Point(x, y, p1.A, p1.B);
        }

        throw new Exception("Error");
    }

    public static Point operator *(Point p, int coefficient)
    {
        var current = p;
        var result = NewNullPoint(p);
        while (coefficient != 0)
        {
            if ((coefficient & 1) != 0)
            {
                result += current;
            }
            current += current;
            coefficient >>= 1;
        }
        return result;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    private static Point NewNullPoint(Point p)
    {
        var prime = p.A.Prime;
        return new Point(new FieldElement(null, prime), new FieldElement(null, prime), p.A, p.B);
    }
}
