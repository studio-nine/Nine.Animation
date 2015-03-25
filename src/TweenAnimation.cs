namespace Nine.Animation
{
    using System;

    /// <summary>
    /// Implements a basic primitive animation that changes its value over time.
    /// Can also update the value of a named target property on an target object.
    /// </summary>
    public class TweenAnimation : TimelineAnimation
    {
        public double From { get; set; }
        public double To { get; set; }

        public Action<double> Set { get; set; }
        public Func<double, double> Easing { get; set; }

        public TweenAnimation SetEasing(Func<double, double> value) { this.Easing = value; return this; }

        public TweenAnimation() { }
        public TweenAnimation(Action<double> set) { this.Set = set; }

        protected override void Seek(double percentage, double previousPercentage)
        {
            if (Set == null) return;
            if (Easing == null) Easing = Nine.Animation.Easing.Sin;
            
            Set.Invoke(From + Easing(percentage) * (To - From));
        }
    }

    /// <summary>
    /// Implements a basic primitive animation that changes its value over time.
    /// Can also update the value of a named target property on an target object.
    /// </summary>
    public class TweenAnimation<T> : TimelineAnimation
    {
        public T From { get; set; }
        public T To { get; set; }

        public Action<T> Set { get; set; }

        public Func<double, double> Easing { get; set; }
        public Func<T, T, double, T> Interpolate { get; set; }

        public TweenAnimation<T> SetEasing(Func<double, double> value) { this.Easing = value; return this; }
        public TweenAnimation<T> SetInterpolate(Func<T, T, double, T> value) { this.Interpolate = value; return this; }

        public TweenAnimation() { }
        public TweenAnimation(Action<T> set, Func<T, T, double, T> interpolate) { this.Set = set; this.Interpolate = interpolate; }

        protected override void Seek(double percentage, double previousPercentage)
        {
            if (Interpolate == null && Set == null) return;
            if (Easing == null) Easing = Nine.Animation.Easing.Sin;
            
            Set.Invoke(Interpolate(From, To, Easing(percentage)));
        }
    }
}