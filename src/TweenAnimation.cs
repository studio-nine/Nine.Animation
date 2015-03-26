namespace Nine.Animation
{
    using System;

    /// <summary>
    /// Defines in which direction will the transition be eased.
    /// </summary>
    public enum EaseDirection
    {
        /// <summary>
        /// Eased when transiting in.
        /// </summary>
        In,

        /// <summary>
        /// Eased when transiting out.
        /// </summary>
        Out,

        /// <summary>
        /// Eased when both transiting in and out.
        /// </summary>
        InOut,
    }

    /// <summary>
    /// Implements a basic primitive animation that changes its value over time.
    /// Can also update the value of a named target property on an target object.
    /// </summary>
    public abstract class TweenAnimation : TimelineAnimation
    {
        public static Func<double, double> DefaultEasing { get; set; } = Nine.Animation.Easing.Sin;
        public static EaseDirection DefaultEaseDirection { get; set; } = EaseDirection.InOut;

        static TweenAnimation() { Interpolate.Initialize(); }

        public EaseDirection EaseDirection { get; set; } = DefaultEaseDirection;
        public Func<double, double> Easing { get; set; } = DefaultEasing;

        public TweenAnimation In() { this.EaseDirection = EaseDirection.In; return this; }
        public TweenAnimation Out() { this.EaseDirection = EaseDirection.Out; return this; }
        public TweenAnimation InOut() { this.EaseDirection = EaseDirection.InOut; return this; }
        public TweenAnimation SetEasing(Func<double, double> value) { this.Easing = value; return this; }
    }

    /// <summary>
    /// Implements a basic primitive animation that changes its value over time.
    /// Can also update the value of a named target property on an target object.
    /// </summary>
    public class TweenAnimation<T> : TweenAnimation
    {
        public T From { get; set; }
        public T To { get; set; }

        public Action<T> Set { get; set; }
        public Func<T, T, double, T> Interpolate { get; set; }

        public TweenAnimation<T> SetInterpolate(Func<T, T, double, T> value) { this.Interpolate = value; return this; }

        public TweenAnimation() { }
        public TweenAnimation(Action<T> set, Func<T, T, double, T> interpolate = null)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            
            this.Set = set;
            this.Interpolate = interpolate ?? Interpolate<T>.Value;

            if (this.Interpolate == null) throw new ArgumentNullException($"Interpolator not found for type: { typeof(T) }");
        }

        protected override void Seek(double percentage, double previousPercentage)
        {
            if (Interpolate == null && Set == null) return;
            if (percentage <= 0) { Set(From); return; }
            if (percentage >= 1) { Set(To); return; }

            switch (EaseDirection)
            {
                case EaseDirection.In:
                    percentage = Easing(percentage);
                    break;
                case EaseDirection.Out:
                    percentage = 1.0 - Easing(1.0 - percentage);
                    break;
                case EaseDirection.InOut:
                    percentage = percentage < 0.5 ?
                        Easing(percentage * 2) * 0.5 :
                        0.5 + (1.0 - Easing(1.0 - (percentage - 0.5) * 2)) * 0.5;
                    break;
            }

            Set.Invoke(Interpolate(From, To, percentage));
        }
    }
}