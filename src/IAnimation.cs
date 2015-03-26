namespace Nine.Animation
{
    using System;

    public interface IAnimation
    {
        event Action Completed;

        void Update(double elapsedTime);
    }
}