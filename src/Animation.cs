namespace Nine.Animation
{
    using System;
    using System.Runtime.CompilerServices;
    
    public abstract class Animation : IAnimation, INotifyCompletion
    {
        private Action continuation;
        private double elapsed;

        /// <summary>
        /// Gets a value indicating whether this animation is playing.
        /// </summary>
        public bool IsPlaying { get; private set; } = true;

        /// <summary>
        /// Gets or sets the delay before this animation is played.
        /// </summary>
        public TimeSpan Delay
        {
            get { return TimeSpan.FromMilliseconds(delay); }
            set { delay = value.TotalMilliseconds; }
        }
        private double delay;

        public Animation SetDelay(double value) { this.delay = value; return this; }
        public Animation SetDelay(TimeSpan value) { this.delay = value.TotalMilliseconds; return this; }

        /// <summary>
        /// Occurs when this animation has played to the end.
        /// </summary>
        public event Action Completed;

        /// <summary>
        /// Update the animation by a specified amount of elapsed time.
        /// Handle playing either forwards or backwards.
        /// Determines whether animation should terminate or continue.
        /// Signals related events.
        /// </summary>
        public bool Update(double elapsedTime)
        {
            if (!IsPlaying) return true;

            elapsed += elapsedTime;
            elapsedTime = (elapsed - delay);

            if (elapsedTime < 0) return false;

            if (UpdateCore(elapsedTime))
            {
                IsPlaying = false;
                continuation?.Invoke();
                Completed?.Invoke();
                return true;
            }

            return false;
        }

        protected abstract bool UpdateCore(double elapsedTime);

        public bool IsCompleted => !IsPlaying;
        public void GetResult() { }
        public Animation GetAwaiter() => this;
        public void OnCompleted(Action continuation) => this.continuation = continuation;
    }
}