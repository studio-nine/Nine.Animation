namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

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
        public event Action Stopped;

        /// <summary>
        /// Stops this animation.
        /// </summary>
        public void Stop()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                continuation?.Invoke();
                Stopped?.Invoke();
            }
        }

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
                Started?.Invoke();
            }

            if (UpdateCore(deltaTime))
            {
                Stop();
                return true;
            }

            return false;
        }

        protected abstract bool UpdateCore(double deltaTime);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsCompleted => !IsPlaying;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void GetResult() { }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IAwaiter GetAwaiter() => this;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnCompleted(Action continuation) => this.continuation = continuation;

        public virtual void InheritFrom(IAnimation other)
        {
            var anim = other as Animation;
            if (anim != null)
            {
                Delay = anim.Delay;
            }
        }
    }
}