namespace Nine.Animation
{
    class DelayAnimation : IAnimation
    {
        private readonly double delay;
        private double elapsedTime;

        public DelayAnimation(double delay)
        {
            this.delay = delay;
        }
        
        public bool Update(double deltaTime)
        {
            return (elapsedTime += deltaTime) >= delay;
        }
    }
}