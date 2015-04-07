namespace Nine.Animation
{
    using System;

    public static class Ease
    {
        public static double Linear(double x) => x;
        public static double Quad(double x) => x * x;
        public static double Cubic(double x) => x * x * x;
        public static double Quart(double x) => x * x * x * x;
        public static double Quint(double x) => x * x * x * x * x;
        public static double Exp(double x) => (x <= 0) ? 0 : Math.Pow(2, 10 * (x - 1));
        public static double Sin(double x) => 1 - Math.Cos(x * Math.PI / 2);
        public static double Circular(double x) => 1 - Math.Sqrt(1 - x * x);
        public static double Back(double x) => x * x * ((1.70158f + 1) * x - 1.70158f);
        public static double Bounce(double x)
        {
            if (x < (1 / 2.75f)) return 7.5625f * x * x;
            if (x < (2 / 2.75f)) return 7.5625f * (x -= (1.5f / 2.75f)) * x + .75f;
            if (x < (2.5 / 2.75)) return 7.5625f * (x -= (2.25f / 2.75f)) * x + .9375f;
            return 7.5625f * (x -= (2.625f / 2.75f)) * x + .984375f;
        }
        public static double Elastic(double x)
        {
            if (x <= 0) return 0; if (x >= 1) return 1;
            double p = .3f;
            double s = p / 4;
            return -(Math.Pow(2, 10 * (x -= 1)) * Math.Sin((x - s) * (2 * Math.PI) / p));
        }
    }
}