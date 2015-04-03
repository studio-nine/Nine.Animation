namespace Nine.Animation
{
    using System;
    
    public abstract class Animation : IAnimation
    {
        /// <summary>
        /// Gets a value indicating whether this animation is playing.
        /// </summary>
        public bool IsPlaying { get; private set; } = true;

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
        public bool Update(double deltaTime)
        {
            if (!IsPlaying) return true;
            if (deltaTime < 0) return false;

            if (UpdateCore(deltaTime))
            {
                IsPlaying = false;
                Completed?.Invoke();
                return true;
            }

            return false;
        }

        protected abstract bool UpdateCore(double elapsedTime);
    }
}