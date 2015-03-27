namespace Nine.Animation
{
    using System;
    using System.Runtime.CompilerServices;
    using static System.Math;

    /// <summary>
    /// Defines whether the animation is playing forward or backward.
    /// </summary>
    public enum AnimationDirection
    {
        /// <summary>
        /// The animation is playing forward.
        /// </summary>
        Forward,

        /// <summary>
        /// The animation is playing backward.
        /// </summary>
        Backward,
    }

    /// <summary>
    /// Basic class for all timeline based animations.
    /// </summary>
    public abstract class TimelineAnimation : Animation
    {
        private double elapsedTime;
        private Action continuation;

        /// <summary>
        /// Gets or set the duration for this animation. This value is not affected by BeginTime, EndTime and Repeat.
        /// </summary>
        public TimeSpan Duration
        {
            get { return TimeSpan.FromMilliseconds(duration); }
            set { duration = value.TotalMilliseconds; }
        }
        private double duration = DefaultDuration.TotalMilliseconds;

        public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(1);

        public TimelineAnimation SetDuration(double duration) { this.duration = duration; return this; }
        public TimelineAnimation SetDuration(TimeSpan duration) { this.duration = duration.TotalMilliseconds; return this; }

        /// <summary>
        /// Gets or sets the time at which this <see cref="TimelineAnimation"/> should begin.
        /// </summary>
        public TimeSpan? BeginTime
        {
            get { return beginTime != null ? TimeSpan.FromMilliseconds(beginTime.Value) : (TimeSpan?)null; }
            set { beginTime = value?.TotalMilliseconds; }
        }
        private double? beginTime;

        /// <summary>
        /// Gets or sets the time at which this <see cref="TimelineAnimation"/> should end.
        /// </summary>
        public TimeSpan? EndTime
        {
            get { return endTime != null ? TimeSpan.FromMilliseconds(endTime.Value) : (TimeSpan?)null; }
            set { endTime = value?.TotalMilliseconds; }
        }
        private double? endTime;

        /// <summary>
        /// Gets the position of the animation as an elapsed time since the begin point.
        /// Counts up if the direction is Forward, down if Backward.
        /// </summary>
        public TimeSpan Position => TimeSpan.FromMilliseconds(position);
        private double position;

        /// <summary>
        /// Gets or sets the playing speed of this animation.
        /// </summary>
        public double Speed { get; set; } = DefaultSpeed;

        public static double DefaultSpeed { get; set; } = 1.0;

        /// <summary>
        /// Gets whether this animation should play backwards after it reaches the end.
        /// Takes effect when an animation would otherwise complete.
        /// </summary>
        public bool AutoReverse { get; set; }

        public TimelineAnimation SetAutoReverse(bool value) { AutoReverse = value; return this; }

        /// <summary>
        /// Gets or set whether the animation is currently playing forward or backward.
        /// Takes effect on an animation that is playing or paused.
        /// </summary>
        public AnimationDirection Direction
        {
            get { return reverse ? AnimationDirection.Backward : AnimationDirection.Forward; }
            set { reverse = (value == AnimationDirection.Backward); }
        }
        private bool reverse;

        public TimelineAnimation SetDirection(AnimationDirection value) { Direction = value; return this; }

        /// <summary>
        /// Gets or sets the number of times this animation will be played.
        /// When set to a fractional value, the animation will be stopped and completed part way.
        /// Float.MaxValue means forever. The default value is 1.
        /// </summary>
        public double Repeat { get; set; } = 1;

        public TimelineAnimation SetRepeat(double value) { Repeat = value; return this; }
        public TimelineAnimation RepeatForever() { Repeat = double.MaxValue; return this; }
        public TimelineAnimation Yoyo() { Repeat = double.MaxValue; AutoReverse = true; return this; }

        /// <summary>
        /// Occurs when this animation has just repeated.
        /// </summary>
        public event Action Repeated;

        /// <summary>
        /// Positions the animation at the specified time value between 0 and Duration.
        /// Takes effect on an animation that is playing or paused.
        /// Adjusts elapsed time, so that animation will stop on time.
        /// </summary>
        public void Seek(TimeSpan position)
        {
            elapsedTime = position.TotalMilliseconds;
            Update(0);
        }

        /// <summary>
        /// When overridden, positions the animation at the specified percentage.
        /// </summary>
        protected abstract void Seek(double percentage, double previousPercentage);

        /// <summary>
        /// Update the animation by a specified amount of elapsed time.
        /// Handle playing either forwards or backwards.
        /// Determines whether animation should terminate or continue.
        /// Signals related events.
        /// </summary>
        public override bool Update(double dt)
        {
            if (!IsPlaying) return true;

            var ended = false;

            var increment = dt * Speed;

            var beginPosition = Max(0, beginTime ?? 0);

            var endPosition = Min(endTime ?? duration, duration);

            var trimmedDuration = endPosition - beginPosition;

            if (trimmedDuration <= 0)
            {
                Complete();
                return true;
            }

            var totalDuration = Repeat * trimmedDuration;

            var previousRepeat = Floor(elapsedTime / trimmedDuration);

            elapsedTime += increment;

            if (elapsedTime > totalDuration)
            {
                ended = true;
                elapsedTime = totalDuration;
            }

            var nextRepeat = Floor(elapsedTime / trimmedDuration);

            if (ended)
            {
                nextRepeat--; // Do not raise repeat event when the end is reached.
            }

            var nextPosition = elapsedTime - nextRepeat * trimmedDuration;

            var isReversed = (AutoReverse && nextRepeat % 2 == 1 ? !reverse : reverse);

            var previousPosition = position;

            position = isReversed ? endPosition - nextPosition : beginPosition + nextPosition;

            if (ended && Floor(Repeat) == Repeat && (isReversed && beginTime == null || !isReversed && endTime == null))
            {
                Seek(isReversed ? 0.0 : 1.0, previousPosition / duration);
            }
            else
            {
                Seek(position / duration, previousPosition / duration);
            }

            var repeated = Repeated;
            if (repeated != null)
            {
                var repeatCount = nextRepeat - previousRepeat;
                for (var i = 0; i < repeatCount; i++)
                {
                    repeated();
                }
            }

            if (ended)
            {
                Complete();
                return true;
            }

            return false;
        }
    }
}