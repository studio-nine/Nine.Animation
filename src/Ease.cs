namespace Nine.Animation
{
    using System;

    public static class Ease
    {
        public static Func<double, double> Out(Func<double, double> ease) => new Func<double, double>(t => 1.0 - ease(1.0 - t));
        public static Func<double, double> InOut(Func<double, double> ease) => new Func<double, double>(t => t < 0.5 ? ease(t * 2) * 0.5 : 0.5 + (1.0 - ease(1.0 - (t - 0.5) * 2)) * 0.5);

        public static double Linear(double x) => x;

        public static double Quad(double x) => x * x;
        public static readonly Func<double, double> QuadOut = Out(Quad);
        public static readonly Func<double, double> QuadInOut = InOut(Quad);

        public static double Cubic(double x) => x * x * x;
        public static readonly Func<double, double> CubicOut = Out(Cubic);
        public static readonly Func<double, double> CubicInOut = InOut(Cubic);

        public static double Quart(double x) => x * x * x * x;
        public static readonly Func<double, double> QuartOut = Out(Quart);
        public static readonly Func<double, double> QuartInOut = InOut(Quart);

        public static double Quint(double x) => x * x * x * x * x;
        public static readonly Func<double, double> QuintOut = Out(Quint);
        public static readonly Func<double, double> QuintInOut = InOut(Quint);

        public static double Exp(double x) => (x <= 0) ? 0 : Math.Pow(2, 10 * (x - 1));
        public static readonly Func<double, double> ExpOut = Out(Exp);
        public static readonly Func<double, double> ExpInOut = InOut(Exp);

        public static double Sin(double x) => 1 - Math.Cos(x * Math.PI / 2);
        public static readonly Func<double, double> SinOut = Out(Sin);
        public static readonly Func<double, double> SinInOut = InOut(Sin);

        public static double Circular(double x) => 1 - Math.Sqrt(1 - x * x);
        public static readonly Func<double, double> CircularOut = Out(Circular);
        public static readonly Func<double, double> CircularInOut = InOut(Circular);

        public static double Back(double x) => x * x * ((1.70158f + 1) * x - 1.70158f);
        public static readonly Func<double, double> BackOut = Out(Back);
        public static readonly Func<double, double> BackInOut = InOut(Back);
        
        public static double Bounce(double x)
        {
            if (x < (1 / 2.75f)) return 7.5625f * x * x;
            if (x < (2 / 2.75f)) return 7.5625f * (x -= (1.5f / 2.75f)) * x + .75f;
            if (x < (2.5 / 2.75)) return 7.5625f * (x -= (2.25f / 2.75f)) * x + .9375f;
            return 7.5625f * (x -= (2.625f / 2.75f)) * x + .984375f;
        }
        public static readonly Func<double, double> BounceOut = Out(Bounce);
        public static readonly Func<double, double> BounceInOut = InOut(Bounce);

        public static double Elastic(double x)
        {
            if (x <= 0) return 0; if (x >= 1) return 1;
            double p = .3f;
            double s = p / 4;
            return -(Math.Pow(2, 10 * (x -= 1)) * Math.Sin((x - s) * (2 * Math.PI) / p));
        }
        public static readonly Func<double, double> ElasticOut = Out(Elastic);
        public static readonly Func<double, double> ElasticInOut = InOut(Elastic);
    }
}