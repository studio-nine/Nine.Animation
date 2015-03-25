namespace Nine.Animation
{
    using System;

    public interface IFrameTimer
    {
        event Action<double> Tick;
    }
}