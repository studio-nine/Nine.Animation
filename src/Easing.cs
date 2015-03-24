namespace Nine.Animation
{
    using System;

    public static class Easing
    {
        public static float Linear(float t, float b, float c, float d) => c * t / d + b;
        public static float Quad(float t, float b, float c, float d) => c * (t /= d) * t + b;
        public static float Cubic(float t, float b, float c, float d) => c * (t /= d) * t * t + b;
        public static float Quart(float t, float b, float c, float d) => c * (t /= d) * t * t * t + b;
        public static float Quint(float t, float b, float c, float d) => c * (t /= d) * t * t * t * t + b;

        public static float Exp(float t, float b, float c, float d) => (t == 0) ? b : c * (float)Math.Pow(2, 10 * (t / d - 1)) + b;
        public static float Sin(float t, float b, float c, float d) => -c * (float)Math.Cos(t / d * (Math.PI / 2)) + c + b;
        public static float Sqrt(float t, float b, float c, float d) => -c * ((float)Math.Sqrt(1 - (t /= d) * t) - 1) + b;

        public static float Elastic(float t, float b, float c, float d)
        {
            if (t == 0) return b; if ((t /= d) == 1) return b + c;
            float p = d * .3f;
            float a = c;
            float s = p / 4;
            return -(a * (float)Math.Pow(2, 10 * (t -= 1)) * (float)Math.Sin((t * d - s) * (2 * (float)Math.PI) / p)) + b;
        }

        public static float Back(float t, float b, float c, float d) => c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
        public static Func<float, float, float, float, float> Back(float back) => (float t, float b, float c, float d) => c * (t /= d) * t * ((back + 1) * t - back) + b;

        public static float Bounce(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75f)) return c * (7.5625f * t * t) + b;
            if (t < (2 / 2.75f)) return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            if (t < (2.5 / 2.75)) return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }
    }
}