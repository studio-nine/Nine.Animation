namespace Nine.Animation
{
    using System;

    public interface IFrameTimer
    {
        event Action<float> Tick;
    }
}