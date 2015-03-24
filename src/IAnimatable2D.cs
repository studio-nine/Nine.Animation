namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    public interface IAnimatable2D : IAnimatable
    {
        float X { get; set; }
        float Y { get; set; }
        float Alpha { get; set; }
        float Orientation { get; set; }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AnimatableAnimation : IAnimatable2D
    {
        internal IAnimatable2D parent;

        public IFrameTimer FrameTimer => parent.FrameTimer;
        public float Alpha { get { return parent.Alpha; } set { parent.Alpha = value; } }
        public float Orientation { get { return parent.Orientation; } set { parent.Orientation = value; } }
        public float X { get { return parent.X; } set { parent.X = value; } }
        public float Y { get { return parent.Y; } set { parent.Y = value; } }

        public AnimationAwaiter GetAwaiter()
        {
            return new AnimationAwaiter();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator2D
    {
        public static AnimatableAnimation FadeIn(this IAnimatable2D target)
        {
            var animation = target as AnimatableAnimation;
            if (animation != null) target = animation.parent;
            Animator.To(1.0f, () => target.Alpha, a => target.Alpha = a, 1000, null, EasingDirection.In);
            return new AnimatableAnimation { parent = target };
        }
    }
}