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
    public sealed class AnimatableAnimation : IAnimatable2D
    {
        internal IAnimatable2D parent;

        public IFrameTimer FrameTimer => parent.FrameTimer;
        public double Alpha { get { return parent.Alpha; } set { parent.Alpha = value; } }
        public double Orientation { get { return parent.Orientation; } set { parent.Orientation = value; } }
        public double X { get { return parent.X; } set { parent.X = value; } }
        public double Y { get { return parent.Y; } set { parent.Y = value; } }

        public AnimationAwaiter GetAwaiter()
        {
            return new AnimationAwaiter();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator2D
    {
        //public static AnimatableAnimation FadeIn(this IAnimatable2D target)
        //{
        //    var animation = target as AnimatableAnimation;
        //    if (animation != null) target = animation.parent;
        //    Animator.To(1.0f, () => target.Alpha, a => target.Alpha = a, 1000, null, EasingDirection.In);
        //    return new AnimatableAnimation { parent = target };
        //}
    }
}