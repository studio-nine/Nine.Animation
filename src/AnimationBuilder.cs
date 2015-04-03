namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class AnimationBuilder : IAnimatable, IAwaitable, IAwaiter
    {
        private IAnimatable parent;
        private Action continuation;

        public AnimationBuilder(IAnimatable parent, IAnimation animation)
        {
            this.parent = parent;
            this.parent.FrameTimer.Tick(new Func<double, bool>(dt =>
            {
                var ended = animation.Update(dt);
                if (ended)
                {
                    IsCompleted = true;
                    continuation?.Invoke();
                }
                return ended;
            }));
        }

        public IFrameTimer FrameTimer => parent.FrameTimer;
        public IAwaiter GetAwaiter() => this;
        public bool IsCompleted { get; private set; }
        public void GetResult() { }
        public void OnCompleted(Action continuation) => this.continuation = continuation;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Animator
    {
        public static AnimationBuilder Tween<T>(this IAnimatable animatable, TweenAnimation<T> animation)
        {
            return new AnimationBuilder(animatable, animation);
        }
    }
}