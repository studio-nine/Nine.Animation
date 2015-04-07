namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TweenBuilder2DExtensions
    {
        public static TweenBuilder2D FadeTo(this TweenBuilder2D builder, double to)
            => builder.To(a => builder.Target.Alpha = a, builder.Target.Alpha, to);

        public static TweenBuilder2D FadeIn(this TweenBuilder2D builder)
            => builder.To(a => builder.Target.Alpha = a, 0.0, 1.0);

        public static TweenBuilder2D FadeOut(this TweenBuilder2D builder)
            => builder.To(a => builder.Target.Alpha = a, 1.0, 0.0);

        public static TweenBuilder2D MoveTo(this TweenBuilder2D builder, double x, double y)
            => builder.To(a => builder.Target.Position = a, builder.Target.Position, new Vector2(x, y));

        public static TweenBuilder2D MoveBy(this TweenBuilder2D builder, double x, double y)
            => builder.To(a => builder.Target.Position = a, builder.Target.Position, new Vector2(builder.Target.Position.X + x, builder.Target.Position.Y + y));

        public static TweenBuilder2D ScaleTo(this TweenBuilder2D builder, double scale)
            => ScaleTo(builder, scale, scale);

        public static TweenBuilder2D ScaleTo(this TweenBuilder2D builder, double x, double y)
            => builder.To(a => builder.Target.Scale = a, builder.Target.Scale, new Vector2(x, y));

        public static TweenBuilder2D ScaleBy(this TweenBuilder2D builder, double scale)
            => ScaleBy(builder, scale, scale);

        public static TweenBuilder2D ScaleBy(this TweenBuilder2D builder, double x, double y)
            => builder.To(a => builder.Target.Scale = a, builder.Target.Scale, new Vector2(builder.Target.Scale.X + x, builder.Target.Scale.Y + y));

        public static TweenBuilder2D RotateTo(this TweenBuilder2D builder, double angle)
            => builder.To(a => builder.Target.Rotation = a, builder.Target.Rotation, angle);

        public static TweenBuilder2D RotateBy(this TweenBuilder2D builder, double angle)
            => builder.To(a => builder.Target.Rotation = a, builder.Target.Rotation, builder.Target.Rotation + angle);

        public static TweenBuilder2D SpinOnce(this TweenBuilder2D builder)
            => RotateBy(builder, Math.PI * 2);

        public static TweenBuilder2D Spin(this TweenBuilder2D builder, double times = double.MaxValue)
            => RotateBy(builder, Math.PI * 2).Repeat(times);
    }
}