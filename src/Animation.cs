namespace Nine.Animation
{
    using System;
    using System.Runtime.CompilerServices;
    
    public abstract class Animation : IAnimation, INotifyCompletion
    {
        private Action continuation;

        /// <summary>
        /// Gets a value indicating whether this animation is playing.
        /// </summary>
        public bool IsPlaying { get; private set; } = true;

        /// <summary>
        /// Occurs when this animation has played to the end.
        /// </summary>
        public event Action Completed;

        public abstract bool Update(double elapsedTime);

        public void Complete()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                continuation?.Invoke();
                Completed?.Invoke();
            }
        }

        public bool IsCompleted => !IsPlaying;
        public void GetResult() { }
        public Animation GetAwaiter() => this;
        public void OnCompleted(Action continuation) => this.continuation = continuation;
    }
}