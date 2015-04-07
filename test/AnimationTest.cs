namespace Nine.Animation
{
    using System;
    using Xunit;

    public class AnimationTest
    {
        private Random random = new Random();
        private int iterations = 10;

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
                var anim = new Tween<double>(x => value = x)
                {
                    From = 0,
                    To = to,
                    Repeat = repeat,
                    Duration = random.NextDouble() + 0.8
                };
                anim.Repeated += () => repeatCount++;
                
                while (anim.IsPlaying) anim.Update((random.NextDouble() * 0.5 + 0.5) * step);

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
                var anim = new Tween<double>(x => value = x)
                {
                    From = from,
                    To = 0,
                    Repeat = repeat,
                    Duration = random.NextDouble() + 0.8,
                    Forward = false,
                };
                anim.Repeated += () => repeatCount++;

                while (anim.IsPlaying) anim.Update((random.NextDouble() * 0.5 + 0.5) * step);
                
                Assert.Equal(from, value);
                Assert.Equal(repeat - 1, repeatCount);
            }
        }
    }
}