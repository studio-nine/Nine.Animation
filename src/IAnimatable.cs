namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    public interface IAnimatable
    {
        IFrameTimer FrameTimer { get; }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Animatable : IAnimatable
    {
        internal IAnimatable parent;
        public IFrameTimer FrameTimer => parent.FrameTimer;
        public AnimationAwaiter GetAwaiter()
        {
            return new AnimationAwaiter();
        }
    }

    public enum EasingDirection { In, Out, InOut }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator
    {
        private static readonly IFrameTimer defaultTimer = new PortableFrameTimer();
        public static IFrameTimer Timer { get; set; }

        public static Animatable To(
            float to,
            Func<float> get,
            Action<float> set,
            float duration,
            Func<float, float, float, float, float> easing,
            EasingDirection direction)
        {
            return null;
        }

        public static Animatable By(
            Func<float> by,
            Func<float> get,
            Action<float> set,
            float duration,
            Func<float, float, float, float, float> easing,
            EasingDirection direction)
        {
            return null;
        }
    }
}