namespace Nine.Animation
{
    using System;

    public class TweenBuilder2D : TweenBuilder
    {
        public new IAnimatable2D Target => (IAnimatable2D)base.Target;

        public TweenBuilder2D() : base() { }
        public TweenBuilder2D(IAnimatable2D target) : base(target) { }
        public TweenBuilder2D(TweenBuilder2D builder, IAnimation animation) : base(builder, animation) { }

        public new TweenBuilder2D Inherit(bool value = true) => (TweenBuilder2D)base.Inherit(value);
        public new TweenBuilder2D Duration(double value) => (TweenBuilder2D)base.Duration(value);
        public new TweenBuilder2D Delay(double value) => (TweenBuilder2D)base.Delay(value);
        public new TweenBuilder2D Speed(double value) => (TweenBuilder2D)base.Speed(value);
        public new TweenBuilder2D Repeat(double value) => (TweenBuilder2D)base.Repeat(value);
        public new TweenBuilder2D RepeatForever() => (TweenBuilder2D)base.RepeatForever();
        public new TweenBuilder2D Yoyo(bool value = true) => (TweenBuilder2D)base.Yoyo(value);
        public new TweenBuilder2D Forward() => (TweenBuilder2D)base.Forward();
        public new TweenBuilder2D Backward() => (TweenBuilder2D)base.Backward();
        public new TweenBuilder2D Ease(Func<double, double> value) => (TweenBuilder2D)base.Ease(value);
        public new TweenBuilder2D In() => (TweenBuilder2D)base.In();
        public new TweenBuilder2D Out() => (TweenBuilder2D)base.Out();
        public new TweenBuilder2D InOut() => (TweenBuilder2D)base.InOut();

        public new TweenBuilder2D OnStart(Action value) => (TweenBuilder2D)base.OnStart(value);
        public new TweenBuilder2D OnComplete(Action value) => (TweenBuilder2D)base.OnComplete(value);
        public new TweenBuilder2D OnRepeat(Action value) => (TweenBuilder2D)base.OnRepeat(value);

        public new TweenBuilder2D To<T>(Tween<T> animation) => new TweenBuilder2D(this, animation);
        public new TweenBuilder2D To<T>(Action<T> set, T from, T to)
        {
            return new TweenBuilder2D(this, new Tween<T>(set) { From = from, To = to });
        }
        public new TweenBuilder2D To<T>(object target, string property, T to)
        {
            return new TweenBuilder2D(this, new Tween<T>(target, property) { To = to });
        }
    }
}