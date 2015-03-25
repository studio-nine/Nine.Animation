namespace Nine.Animation
{
    using System;

    public static class Animator
    {
        public static TweenAnimation TweenTo(this IAnimatable animatable, Func<double> get, Action<double> set, double to)
        {
            var tween = new TweenAnimation(set) { From = get(), To = to };
            animatable.FrameTimer.Tick += tween.Update;
            return tween;
        }

        public static TweenAnimation TweenBy(this IAnimatable animatable, Func<double> get, Action<double> set, double by)
        {
            var from = get();
            var tween = new TweenAnimation(set) { From = from, To = by + from };
            animatable.FrameTimer.Tick += tween.Update;
            return tween;
        }
    }
}