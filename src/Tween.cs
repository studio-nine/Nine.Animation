namespace Nine.Animation
{
    using System;

    /// <summary>
    /// Defines in which direction will the transition be eased.
    /// </summary>
    public enum EaseInOut
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
    public abstract class Tween : Timeline
    {
        public Func<double, double> Ease { get; set; } = DefaultEase;
        public static Func<double, double> DefaultEase { get; set; } = Nine.Animation.Ease.Sin;

        public EaseInOut InOut { get; set; } = DefaultInOut;
        public static EaseInOut DefaultInOut { get; set; } = EaseInOut.InOut;

        public override void InheritFrom(IAnimation other)
        {
            base.InheritFrom(other);

            var anim = other as Tween;
            if (anim != null)
            {
                InOut = anim.InOut;
                Ease = anim.Ease;
            }
        }
    }

    /// <summary>
    /// Implements a basic primitive animation that changes its value over time.
    /// Can also update the value of a named target property on an target object.
    /// </summary>
    public class Tween<T> : Tween
    {
        public T From { get; set; }
        public T To { get; set; }

        public Action<T> Set { get; set; }
        public Func<T, T, double, T> Interpolate { get; set; }
        
        public Tween() { }
        public Tween(object target, string property) : this(PropertyAccessor.Setter<T>(target, property))
        {
            From = PropertyAccessor.Getter<T>(target, property)();
        }
        public Tween(Action<T> set)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            
            this.Set = set;
            this.Interpolate = Interpolate<T>.Value;
        }

        protected override void Seek(double percentage, double previousPercentage)
        {
            if (Interpolate == null && Set == null) return;
            if (percentage <= 0) { Set(From); return; }
            if (percentage >= 1) { Set(To); return; }

            switch (InOut)
            {
                case EaseInOut.In:
                    percentage = Ease(percentage);
                    break;
                case EaseInOut.Out:
                    percentage = 1.0 - Ease(1.0 - percentage);
                    break;
                case EaseInOut.InOut:
                    percentage = percentage < 0.5 ?
                        Ease(percentage * 2) * 0.5 :
                        0.5 + (1.0 - Ease(1.0 - (percentage - 0.5) * 2)) * 0.5;
                    break;
            }

            Set.Invoke(Interpolate(From, To, percentage));
        }
    }
}