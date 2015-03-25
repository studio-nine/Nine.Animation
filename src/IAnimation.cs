namespace Nine.Animation
{
    using System.Runtime.CompilerServices;

    public interface IAnimation : INotifyCompletion
    {
        void Update(double elapsedTime);
    }
}