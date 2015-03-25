namespace Nine.Animation
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class AnimationTest : IFrameTimer, IAnimatable
    {
        private Random random = new Random();

        public IFrameTimer FrameTimer => this;

        public event Action<double> Tick;

        public void Update(double dt) => Tick?.Invoke(dt);

        [Fact]
        public async Task unsubscribe_to_tick_when_animation_is_complete()
        {
            Assert.Null(Tick);

            var anim = this.TweenTo(x => { }, 0, 1);
            Assert.NotNull(Tick);
            Update(double.MaxValue);
            await anim;

            Assert.Null(Tick);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(500)]
        [InlineData(10000)]
        public void target_is_place_at_the_exactly_end_position_when_tween_ends(double step)
        {
            var repeat = random.Next(10) + 1;

            double value = random.NextDouble();

            var repeatCount = 0;
            var to = random.NextDouble() * 100;
            var anim = this.TweenTo(x => value = x, 0, to).SetRepeat(repeat).SetDuration(random.NextDouble() + 0.8);
            anim.Repeated += () => repeatCount++;

            while (anim.IsPlaying) Update((random.NextDouble() * 0.5 + 0.5) * step);

            Assert.True(anim.IsCompleted);
            Assert.Equal(to, value);
            Assert.Equal(repeat - 1, repeatCount);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(1000)]
        [InlineData(20000)]
        public void target_is_place_at_the_exactly_begin_position_when_tween_ends(double step)
        {
            var repeat = random.Next(10) + 1;

            double value = random.NextDouble();

            var repeatCount = 0;
            var from = random.NextDouble() * 100;
            var anim = this.TweenTo(x => value = x, from, 0).SetRepeat(repeat).SetDirection(AnimationDirection.Backward);
            anim.Repeated += () => repeatCount++;

            while (anim.IsPlaying) Update((random.NextDouble() * 0.5 + 0.5) * step);

            Assert.True(anim.IsCompleted);
            Assert.Equal(from, value);
            Assert.Equal(repeat - 1, repeatCount);
        }
    }
}