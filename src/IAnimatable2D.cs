namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    public interface IAnimatable2D : IAnimatable
    {
        double X { get; set; }
        double Y { get; set; }
        double Alpha { get; set; }
        double Orientation { get; set; }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator2D
    {
        struct Vector2 { public double X; public double Y; }
        static Animator2D()
        {
            Interpolate<Vector2>.Value = (Vector2 a, Vector2 b, double t) => new Vector2
            {
                X = a.X * (1 - t) + b.X * t,
                Y = a.Y * (1 - t) + b.Y * t,
            };
        }

        public static TweenAnimation FadeIn(this IAnimatable2D target)
        {
            if (target.Alpha >= 1.0) return target.Tween(a => target.Alpha = a, 0.0, 1.0).Out();
            return target.Tween(a => target.Alpha = a, target.Alpha, 1.0).Out();
        }

        public static TweenAnimation FadeOut(this IAnimatable2D target)
        {
            if (target.Alpha <= 0.0) return target.Tween(a => target.Alpha = a, 1.0, 0.0);
            return target.Tween(a => target.Alpha = a, target.Alpha, 0.0);
        }
        
        public static TweenAnimation MoveTo(this IAnimatable2D target, double x, double y)
        {
            return target.Tween(a => { target.X = a.X; target.Y = a.Y; }, new Vector2 { X = target.X, Y = target.Y }, new Vector2 { X = x, Y = y })
                         .SetEasing(Easing.Cubic)
                         .InOut();
        }

        public static TweenAnimation MoveBy(this IAnimatable2D target, double x, double y)
        {
            return target.Tween(a => { target.X = a.X; target.Y = a.Y; }, new Vector2 { X = target.X, Y = target.Y }, new Vector2 { X = target.X + x, Y = target.Y + y })
                         .SetEasing(Easing.Cubic)
                         .InOut();
        }
    }
}