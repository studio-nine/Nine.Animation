namespace Nine.Animation
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class PortableFrameTimer : IFrameTimer
    {
        public static readonly IFrameTimer Default = new PortableFrameTimer();

        private static readonly TimeSpan frameTime = TimeSpan.FromSeconds(1.0 / 60);

        private bool running;
        private Stopwatch watch = new Stopwatch();
        private Action<double> tick;

        public event Action<double> Tick
        {
            add
            {
                tick += value;
                if (!running) Loop();
            }
            remove
            {
                tick -= value;
                if (tick == null) running = false;
            }
        }

        private async void Loop()
        {
            running = true;
            watch.Restart();

            while (running)
            {
                if (tick != null) tick((double)watch.Elapsed.TotalMilliseconds);
                watch.Restart();
                await Task.Delay(frameTime);
            }
        }
    }
}