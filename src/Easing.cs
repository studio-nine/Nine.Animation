namespace Nine.Animation
{
    using System;

    public static class Easing
    {
        public static double Linear(double t) => t;
        public static double Quad(double t) => t * t;
        public static double Cubic(double t) => t * t * t;
        public static double Quart(double t) => t * t * t * t;
        public static double Quint(double t) => t * t * t * t * t;

        //public static double Exp(double t, double b, double c, double d) => (t == 0) ? b : c * (double)Math.Pow(2, 10 * (t / d - 1)) + b;
        public static double Sin(double t) => Math.Sin(t * Math.PI / 2);
        //public static double Sqrt(double t, double b, double c, double d) => -c * ((double)Math.Sqrt(1 - (t /= d) * t) - 1) + b;

        //public static double Elastic(double t, double b, double c, double d)
        //{
        //    if (t == 0) return b; if ((t /= d) == 1) return b + c;
        //    double p = d * .3f;
        //    double a = c;
        //    double s = p / 4;
        //    return -(a * (double)Math.Pow(2, 10 * (t -= 1)) * (double)Math.Sin((t * d - s) * (2 * (double)Math.PI) / p)) + b;
        //}

        //public static double Back(double t, double b, double c, double d) => c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
        //public static Func<double, double, double, double, double> Back(double back) => (double t, double b, double c, double d) => c * (t /= d) * t * ((back + 1) * t - back) + b;

        //public static double Bounce(double t, double b, double c, double d)
        //{
        //    if ((t /= d) < (1 / 2.75f)) return c * (7.5625f * t * t) + b;
        //    if (t < (2 / 2.75f)) return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
        //    if (t < (2.5 / 2.75)) return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
        //    return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        //}
    }
}