namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    public class TweenBuilder : IAwaitable, IAwaiter
    {
        private readonly IAnimatable target;
        private readonly IAnimation animation;
        private Action continuation;
        private bool inherit = true;
        
        public IAnimatable Target => target;

        public TweenBuilder(IAnimatable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            this.target = target;
            this.animation = new Tween<double>();
        }

        public TweenBuilder(TweenBuilder builder, IAnimation animation, bool inherit = true)
        {
            if (builder.animation != null && builder.inherit && inherit)
            {
                animation.InheritFrom(builder.animation);
            }

            this.animation = animation;
            this.target = builder.target;
            this.target.FrameTimer.OnTick(new Func<double, bool>(dt =>
            {
                var ended = this.animation.Update(dt);
                if (ended)
                {
                    IsCompleted = true;
                    continuation?.Invoke();
                }
                return ended;
            }));
        }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IAwaiter GetAwaiter() => this;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsCompleted { get; private set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void GetResult() { }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnCompleted(Action continuation) => this.continuation = continuation;

        public TweenBuilder Inherit(bool value = true)
        {
            this.inherit = value;
            return this;
        }

        public TweenBuilder Duration(double value)
        {
            var anim = animation as Timeline;
            if (anim != null) anim.Duration = value;
            return this;
        }

        public TweenBuilder Delay(double value)
        {
            var anim = animation as Timeline;
            if (anim != null) anim.Delay = value;
            return this;
        }

        public TweenBuilder Speed(double value)
        {
            var anim = animation as Timeline;
            if (anim != null) anim.Speed = value;
            return this;
        }

        public TweenBuilder RepeatForever() => Repeat(double.MaxValue);
        public TweenBuilder Repeat(double value)
        {
            var anim = animation as Timeline;
            if (anim != null) anim.Repeat = value;
            return this;
        }

        public TweenBuilder Yoyo(bool value = true)
        {
            var anim = animation as Timeline;
            if (anim != null) anim.Yoyo = value;
            return this;
        }

        public TweenBuilder Forward()
        {
            var anim = animation as Timeline;
            if (anim != null) anim.Forward = true;
            return this;
        }

        public TweenBuilder Backward()
        {
            var anim = animation as Timeline;
            if (anim != null) anim.Forward = false;
            return this;
        }

        public TweenBuilder Ease(Func<double, double> value)
        {
            var anim = animation as Tween;
            if (anim != null) anim.Ease = value;
            return this;
        }

        public TweenBuilder In()
        {
            var anim = animation as Tween;
            if (anim != null) anim.InOut = EaseInOut.In;
            return this;
        }

        public TweenBuilder Out()
        {
            var anim = animation as Tween;
            if (anim != null) anim.InOut = EaseInOut.Out;
            return this;
        }

        public TweenBuilder InOut()
        {
            var anim = animation as Tween;
            if (anim != null) anim.InOut = EaseInOut.InOut;
            return this;
        }

        public TweenBuilder OnStart(Action value)
        {
            var anim = animation as Animation;
            if (anim != null) anim.Started += value;
            return this;
        }

        public TweenBuilder OnComplete(Action value)
        {
            var anim = animation as Animation;
            if (anim != null) anim.Completed += value;
            return this;
        }

        public TweenBuilder OnRepeat(Action value)
        {
            var anim = animation as Tween;
            if (anim != null) anim.Repeated += value;
            return this;
        }

        public TweenBuilder Tween<T>(Tween<T> animation) => new TweenBuilder(this, animation);
        public TweenBuilder Tween<T>(Action<T> set, T from, T to) => new TweenBuilder(this, new Tween<T>(set) { From = from, To = to });
        public TweenBuilder Tween<T>(object target, string property, T to) => new TweenBuilder(this, new Tween<T>(target, property) { To = to });
    }
}