namespace Nine.Animation
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct AnimationAwaiter : INotifyCompletion
    {
        private Action continuation;

        public bool IsCompleted { get; }
        public void OnCompleted(Action continuation) => this.continuation = continuation;
        public void GetResult() { }
    }
}