namespace Nine.Animation
{
    using System;
    using System.Threading.Tasks;

    public class PortableFrameTimer : FrameTimer
    {
        public static readonly PortableFrameTimer Default = new PortableFrameTimer();

        private static readonly TimeSpan frameTime = TimeSpan.FromSeconds(1.0 / 60);

        private bool running;

        public override void OnTick(Func<double, bool> listener)
        {
            base.OnTick(listener);
            if (!running) Loop();
        }

        private async void Loop()
        {
            running = true;

            while (running)
            {
                UpdateFrame();

                await Task.Delay(frameTime);
            }
        }
    }
}