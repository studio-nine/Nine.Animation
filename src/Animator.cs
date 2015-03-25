namespace Nine.Animation
{
    using System;

    public static class Animator
    {
        public static TweenAnimation TweenTo(this IAnimatable animatable, Action<double> set, double from, double to)
        {
            var tween = new TweenAnimation(set) { From = from, To = to };
            animatable.FrameTimer.Tick += tween.Update;
            return tween;
        }

        public static TweenAnimation TweenBy(this IAnimatable animatable, Action<double> set, double from, double by)
        {
            var tween = new TweenAnimation(set) { From = from, To = by + from };
            animatable.FrameTimer.Tick += tween.Update;
            return tween;
        }
    }
}