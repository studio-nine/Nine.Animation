namespace Nine.Animation
{
    using System;
    using Xunit;

    public class AnimationTest : FrameTimer, IAnimatable
    {
        private Random random = new Random();
        private int iterations = 10;

        public IFrameTimer FrameTimer => this;

        [Theory]
        [InlineData(10)]
        [InlineData(500)]
        [InlineData(10000)]
        public void target_is_place_at_the_exactly_end_position_when_tween_ends(double step)
        {
            for (var i = 0; i < iterations; i++)
            {
                var repeat = random.Next(10) + 1;
                var repeatCount = 0;
                var value = random.NextDouble();
                var to = random.NextDouble() * 100;
                var anim = this.Tween(x => value = x, 0, to).SetRepeat(repeat).SetDuration(random.NextDouble() + 0.8);
                anim.Repeated += () => repeatCount++;

                while (anim.IsPlaying) UpdateFrame((random.NextDouble() * 0.5 + 0.5) * step);

                Assert.True(anim.IsCompleted);
                Assert.Equal(to, value);
                Assert.Equal(repeat - 1, repeatCount);
            }
        }

        [Theory]
        [InlineData(20)]
        [InlineData(1000)]
        [InlineData(20000)]
        public void target_is_place_at_the_exactly_begin_position_when_tween_ends(double step)
        {
            for (var i = 0; i < iterations; i++)
            {
                var repeat = random.Next(10) + 1;
                var repeatCount = 0;
                var value = random.NextDouble();
                var from = random.NextDouble() * 100;
                var anim = this.Tween(x => value = x, from, 0).SetRepeat(repeat).SetDirection(AnimationDirection.Backward);
                anim.Repeated += () => repeatCount++;

                while (anim.IsPlaying) UpdateFrame((random.NextDouble() * 0.5 + 0.5) * step);

                Assert.True(anim.IsCompleted);
                Assert.Equal(from, value);
                Assert.Equal(repeat - 1, repeatCount);
            }
        }

        [Fact]
        public void aimation_is_not_played_until_delay_time_is_reached()
        {
            var initial = random.NextDouble();
            var value = initial;
            var delay = random.NextDouble() * 100;
            this.Tween(x => value = x, random.NextDouble(), random.NextDouble()).SetDelay(delay);

            UpdateFrame(delay - 0.0001);
            Assert.Equal(initial, value);
            UpdateFrame(delay);
            Assert.NotEqual(initial, delay);
        }
    }
}
}