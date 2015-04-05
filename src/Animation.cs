namespace Nine.Animation
{
    using System;
    
    public abstract class Animation : IAnimation, IAwaitable, IAwaiter
    {
        private Action continuation;
        private double elapsedTime;

        /// <summary>
        /// Gets a value indicating whether this animation is playing.
        /// </summary>
        public bool IsPlaying { get; private set; } = true;

        /// <summary>
        /// Gets or sets the delay before the animation has started.
        /// </summary>
        public double Delay { get; set; }

        /// <summary>
        /// Occurs when this animation has played to the end.
        /// </summary>
        public event Action Completed;

        /// <summary>
        /// Occurs when this animation is about to playing.
        /// </summary>
        public event Action Started;

        /// <summary>
        /// Update the animation by a specified amount of elapsed time.
        /// Handle playing either forwards or backwards.
        /// Determines whether animation should terminate or continue.
        /// Signals related events.
        /// </summary>
        public bool Update(double deltaTime)
        {
            if (!IsPlaying) return true;

            if (elapsedTime < Delay)
            {
                elapsedTime += deltaTime;
                if (elapsedTime < Delay) return false;
                deltaTime = (elapsedTime - Delay);
            }

            if (UpdateCore(deltaTime))
            {
                IsPlaying = false;
                continuation?.Invoke();
                Completed?.Invoke();
                return true;
            }

            return false;
        }

        protected abstract bool UpdateCore(double deltaTime);

        public bool IsCompleted => !IsPlaying;
        public void GetResult() { }
        public IAwaiter GetAwaiter() => this;
        public void OnCompleted(Action continuation) => this.continuation = continuation;
    }
}