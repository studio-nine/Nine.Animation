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
            return null;
        }
    }
}