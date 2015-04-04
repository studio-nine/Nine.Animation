namespace Nine.Animation
{
    using System.ComponentModel;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator
    {
        public static AnimationBuilder Tween<T>(this IAnimatable animatable, TweenAnimation<T> animation)
        {
            return new AnimationBuilder(animatable, animation);
        }
    }
}