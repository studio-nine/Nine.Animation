namespace Nine.Animation
{
    using System.Runtime.CompilerServices;

    public interface IAwaiter : INotifyCompletion
    {
        bool IsCompleted { get; }
        void GetResult();
    }

    public interface IAwaitable
    {
        IAwaiter GetAwaiter();
    }
}