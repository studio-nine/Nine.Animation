namespace Nine.Animation
{
    using System;

    public struct Vector2
    {
        public double X;
        public double Y;

        public static readonly Vector2 Zero = new Vector2();
        public static readonly Vector2 One = new Vector2(1.0, 1.0);

        public Vector2(double x, double y) { X = x; Y = y; }

        public double Length => Math.Sqrt(X * X + Y * Y);

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 a, double b) => new Vector2(a.X * b, a.Y * b);
        public static Vector2 operator *(double b, Vector2 a) => new Vector2(a.X * b, a.Y * b);
    }
}