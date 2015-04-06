namespace Nine.Animation
{
    using System;

    public class TweenBuilder2D : TweenBuilder
    {
        public new IAnimatable2D Target => (IAnimatable2D)base.Target;

        public TweenBuilder2D(IAnimatable2D target) : base(target) { }
        public TweenBuilder2D(TweenBuilder2D builder, IAnimation animation) : base(builder, animation) { }

        public new TweenBuilder2D Duration(double value) => (TweenBuilder2D)base.Duration(value);
        public new TweenBuilder2D Delay(double value) => (TweenBuilder2D)base.Delay(value);
        public new TweenBuilder2D Repeat(double value) => (TweenBuilder2D)base.Repeat(value);
        public new TweenBuilder2D RepeatForever() => (TweenBuilder2D)base.RepeatForever();
        public new TweenBuilder2D Yoyo() => (TweenBuilder2D)base.Yoyo();
        public new TweenBuilder2D Backward() => (TweenBuilder2D)base.Backward();
        public new TweenBuilder2D Ease(Func<double, double> value) => (TweenBuilder2D)base.Ease(value);

        public new TweenBuilder2D OnStart(Action value) => (TweenBuilder2D)base.OnStart(value);
        public new TweenBuilder2D OnComplete(Action value) => (TweenBuilder2D)base.OnComplete(value);
        public new TweenBuilder2D OnRepeat(Action value) => (TweenBuilder2D)base.OnRepeat(value);

        public new TweenBuilder2D Tween<T>(Tween<T> animation) => new TweenBuilder2D(this, animation);
        public new TweenBuilder2D Tween<T>(Action<T> set, T from, T to)
        {
            return new TweenBuilder2D(this, new Tween<T>(set) { From = from, To = to });
        }
        public new TweenBuilder2D Tween<T>(object target, string property, T to, double duration = -1, Func<double, double> ease = null, double delay = 0)
        {
            return new TweenBuilder2D(this, new Tween<T>(target, property) { To = to });
        }
    }
}