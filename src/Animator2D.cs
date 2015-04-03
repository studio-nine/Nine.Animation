namespace Nine.Animation
{
    using System;
    using System.ComponentModel;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator2D
    {
        static Animator2D()
        {
            Interpolate<Vector2>.Value = (Vector2 a, Vector2 b, double t) => new Vector2
            {
                X = a.X * (1 - t) + b.X * t,
                Y = a.Y * (1 - t) + b.Y * t,
            };
        }

        public static TweenAnimation FadeTo(this IAnimatable2D target, double alpha)
        {
            return target.Tween(a => target.Alpha = a, target.Alpha, alpha);
        }

        public static TweenAnimation FadeIn(this IAnimatable2D target)
        {
            if (target.Alpha >= 1.0) return target.Tween(a => target.Alpha = a, 0.0, 1.0).Out();
            return target.Tween(a => target.Alpha = a, target.Alpha, 1.0);
        }

        public static TweenAnimation FadeOut(this IAnimatable2D target)
        {
            if (target.Alpha <= 0.0) return target.Tween(a => target.Alpha = a, 1.0, 0.0);
            return target.Tween(a => target.Alpha = a, target.Alpha, 0.0);
        }
        
        public static TweenAnimation MoveTo(this IAnimatable2D target, double x, double y)
        {
            return target.Tween(a => target.Position = a, target.Position, new Vector2(x, y));
        }

        public static TweenAnimation MoveBy(this IAnimatable2D target, double x, double y)
        {
            return target.Tween(a => target.Position = a, target.Position, new Vector2(target.Position.X + x, target.Position.Y + y));
        }

        public static TweenAnimation ScaleTo(this IAnimatable2D target, double scale)
        {
            return ScaleTo(target, scale, scale);
        }

        public static TweenAnimation ScaleTo(this IAnimatable2D target, double x, double y)
        {
            return target.Tween(a => target.Scale = a, target.Scale, new Vector2(x, y));
        }

        public static TweenAnimation ScaleBy(this IAnimatable2D target, double scale)
        {
            return ScaleBy(target, scale, scale);
        }

        public static TweenAnimation ScaleBy(this IAnimatable2D target, double x, double y)
        {
            return target.Tween(a => target.Scale = a, target.Scale, new Vector2(target.Scale.X * x, target.Scale.Y * y));
        }

        public static TweenAnimation RotateTo(this IAnimatable2D target, double angle)
        {
            return target.Tween(a => target.Rotation = a, target.Rotation, angle);
        }

        public static TweenAnimation RotateBy(this IAnimatable2D target, double angle)
        {
            return target.Tween(a => target.Rotation = a, target.Rotation, target.Rotation + angle);
        }

        public static TweenAnimation SpinOnce(this IAnimatable2D target)
        {
            return target.Tween(a => target.Rotation = a, target.Rotation, target.Rotation + Math.PI * 2);
        }

        public static TweenAnimation Spin(this IAnimatable2D target)
        {
            return (TweenAnimation)
                target.Tween(a => target.Rotation = a, target.Rotation, target.Rotation + Math.PI * 2)
                      .SetEasing(Easing.Linear)
                      .RepeatForever();
        }
    }
}