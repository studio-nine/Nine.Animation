namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Animation2DBuilder : AnimationBuilder, IAnimatable2D
    {
        private readonly IAnimatable2D parent;

        public Animation2DBuilder(IAnimatable2D parent, IAnimation animation)
            : base(parent, animation)
        {
            var builder = parent as Animation2DBuilder;
            if (builder != null)
            {
                this.parent = builder.parent;
            }
            else
            {
                this.parent = parent;
            }
        }

        public double Alpha
        {
            get { return parent.Alpha; }
            set { parent.Alpha = value; }
        }

        public Vector2 Position
        {
            get { return parent.Position; }
            set { parent.Position = value; }
        }

        public Vector2 Scale
        {
            get { return parent.Scale; }
            set { parent.Scale = value; }
        }

        public double Rotation
        {
            get { return parent.Rotation; }
            set { parent.Rotation = value; }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator2D
    {
        public static Animation2DBuilder FadeTo(this IAnimatable2D target, double to,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<double>(a => target.Alpha = a)
            {
                From = target.Alpha, To = to,
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder FadeIn(this IAnimatable2D target,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<double>(a => target.Alpha = a)
            {
                From = 0.0, To = 1.0,
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder FadeOut(this IAnimatable2D target,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<double>(a => target.Alpha = a)
            {
                From = 1.0, To = 0.0,
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }
        
        public static Animation2DBuilder MoveTo(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<Vector2>(a => target.Position = a)
            {
                From = target.Position, To = new Vector2(x, y),
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder MoveBy(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<Vector2>(a => target.Position = a)
            {
                From = target.Position,
                To = new Vector2(target.Position.X + x, target.Position.Y + y),
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder ScaleTo(this IAnimatable2D target, double scale,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return ScaleTo(target, scale, scale, duration, easing, direction);
        }

        public static Animation2DBuilder ScaleTo(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<Vector2>(a => target.Position = a)
            {
                From = target.Scale, To = new Vector2(x, y),
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder ScaleBy(this IAnimatable2D target, double scale,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return ScaleBy(target, scale, scale, duration, easing, direction);
        }

        public static Animation2DBuilder ScaleBy(this IAnimatable2D target, double x, double y,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<Vector2>(a => target.Position = a)
            {
                From = target.Scale,
                To = new Vector2(target.Scale.X * x, target.Scale.Y * y),
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder RotateTo(this IAnimatable2D target, double angle,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<double>(a => target.Rotation = a)
            {
                From = target.Rotation, To = angle,
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder RotateBy(this IAnimatable2D target, double angle,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<double>(a => target.Rotation = a)
            {
                From = target.Rotation,
                To = target.Rotation + angle,
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder SpinOnce(this IAnimatable2D target,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null)
        {
            return new Animation2DBuilder(target, new TweenAnimation<double>(a => target.Rotation = a)
            {
                From = target.Rotation,
                To = target.Rotation + Math.PI * 2,
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
            });
        }

        public static Animation2DBuilder Spin(this IAnimatable2D target, double times,
            double? duration = null, Func<double, double> easing = null, EaseDirection? direction = null, bool yoyo = false)
        {
            return new Animation2DBuilder(target, new TweenAnimation<double>(a => target.Rotation = a)
            {
                From = target.Rotation,
                To = target.Rotation + Math.PI * 2,
                Duration = duration ?? TimelineAnimation.DefaultDuration,
                Easing = easing ?? TweenAnimation.DefaultEasing,
                EaseDirection = direction ?? TweenAnimation.DefaultEaseDirection,
                Repeat = times,
                Yoyo = yoyo,
            });
        }
    }
}