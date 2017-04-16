using UnityEngine;

public struct Point2
{
    public static readonly Point2 zero = new Point2(0, 0);
    public static readonly Point2 one = new Point2(1, 1);
    public static readonly Point2 right = new Point2(1, 0);
    public static readonly Point2 up = new Point2(0, 1);
    public static readonly Point2 left = new Point2(-1, 0);
    public static readonly Point2 down = new Point2(0, -1);

    public int x;
    public int y;

    public Point2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Point2(Point2 point2) : this(point2.x, point2.y) { }

    public bool Within(int x, int y, int width, int height)
    {
        return x <= this.x && this.x < width && y <= this.y && this.y < height;
    }

    public Point2 RotateLeft()
    {
        return new Point2(-y, x);
    }

    public Point2 RotateRight()
    {
        return new Point2(y, -x);
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }

    override public string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    public static Point2 operator -(Point2 a)
    {
        return new Point2(-a.x, -a.y);
    }

    public static Point2 operator +(Point2 a, Point2 b)
    {
        return new Point2(a.x + b.x, a.y + b.y);
    }

    public static Point2 operator -(Point2 a, Point2 b)
    {
        return new Point2(a.x - b.x, a.y - b.y);
    }

    public static Point2 operator *(Point2 a, Point2 b)
    {
        return new Point2(a.x * b.x, a.y * b.y);
    }

    public static Point2 operator *(int n, Point2 p)
    {
        return new Point2(n * p.x, n * p.y);
    }

    public static Point2 operator *(Point2 p, int n)
    {
        return n * p;
    }
}

public struct Point3
{
    public static readonly Point3 zero = new Point3(0, 0, 0);
    public static readonly Point3 one = new Point3(1, 1, 1);
    public static readonly Point3 right = new Point3(1, 0, 0);
    public static readonly Point3 up = new Point3(0, 1, 0);
    public static readonly Point3 forward = new Point3(0, 0, 1);
    public static readonly Point3 left = new Point3(-1, 0, 0);
    public static readonly Point3 down = new Point3(0, -1, 0);
    public static readonly Point3 back = new Point3(0, 0, -1);

    public int x;
    public int y;
    public int z;

    public Point3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Point3(Point3 point3) : this(point3.x, point3.y, point3.z) { }
}