namespace Nine.Animation
{
    using System;
    using System.ComponentModel;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator
    {
        public static AnimationBuilder Delay(this IAnimatable animatable, double delay)
        {
            return new AnimationBuilder(animatable, new DelayAnimation(delay));
        }

        public static AnimationBuilder Tween<T>(this IAnimatable animatable, TweenAnimation<T> animation)
        {
            return new AnimationBuilder(animatable, animation);
        }
    }
}