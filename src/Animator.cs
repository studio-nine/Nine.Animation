namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator
    {
        public static TweenAnimation<T> Tween<T>(this IAnimatable animatable, Action<T> set, T from, T to, Func<T, T, double, T> interpolate = null)
        {
            var tween = new TweenAnimation<T>(set, interpolate) { From = from, To = to };
            var timer = animatable.FrameTimer;
            timer.Tick(tween.Update);
            return tween;
        }
    }
}