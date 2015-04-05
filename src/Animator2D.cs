namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator2D
    {
        public static Animation2DBuilder Tween<T>(this IAnimatable2D animatable, Tween<T> animation)
        {
            return new Animation2DBuilder(animatable, animation);
        }

        public static Animation2DBuilder FadeTo(this IAnimatable2D target, double to,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<double>(a => target.Alpha = a)
            {
                From = target.Alpha, To = to,
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder FadeIn(this IAnimatable2D target,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<double>(a => target.Alpha = a)
            {
                From = 0.0, To = 1.0,
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder FadeOut(this IAnimatable2D target,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<double>(a => target.Alpha = a)
            {
                From = 1.0, To = 0.0,
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }
        
        public static Animation2DBuilder MoveTo(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<Vector2>(a => target.Position = a)
            {
                From = target.Position, To = new Vector2(x, y),
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder MoveBy(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<Vector2>(a => target.Position = a)
            {
                From = target.Position,
                To = new Vector2(target.Position.X + x, target.Position.Y + y),
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder ScaleTo(this IAnimatable2D target, double scale,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return ScaleTo(target, scale, scale, duration, easing, inout);
        }

        public static Animation2DBuilder ScaleTo(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<Vector2>(a => target.Position = a)
            {
                From = target.Scale, To = new Vector2(x, y),
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder ScaleBy(this IAnimatable2D target, double scale,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return ScaleBy(target, scale, scale, duration, easing, inout);
        }

        public static Animation2DBuilder ScaleBy(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<Vector2>(a => target.Position = a)
            {
                From = target.Scale,
                To = new Vector2(target.Scale.X * x, target.Scale.Y * y),
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder RotateTo(this IAnimatable2D target, double angle,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<double>(a => target.Rotation = a)
            {
                From = target.Rotation, To = angle,
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder RotateBy(this IAnimatable2D target, double angle,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<double>(a => target.Rotation = a)
            {
                From = target.Rotation,
                To = target.Rotation + angle,
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder SpinOnce(this IAnimatable2D target,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null)
        {
            return new Animation2DBuilder(target, new Tween<double>(a => target.Rotation = a)
            {
                From = target.Rotation,
                To = target.Rotation + Math.PI * 2,
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
            });
        }

        public static Animation2DBuilder Spin(this IAnimatable2D target, double times,
            double? duration = null, Func<double, double> easing = null, EaseInOut? inout = null, bool yoyo = false)
        {
            return new Animation2DBuilder(target, new Tween<double>(a => target.Rotation = a)
            {
                From = target.Rotation,
                To = target.Rotation + Math.PI * 2,
                Duration = duration ?? Timeline.DefaultDuration,
                Ease = easing ?? Nine.Animation.Tween.DefaultEasing,
                InOut = inout ?? Nine.Animation.Tween.DefaultInOut,
                Repeat = times,
                Yoyo = yoyo,
            });
        }
    }
}