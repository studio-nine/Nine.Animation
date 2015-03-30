namespace Nine.Animation
{
    using System;

    public static class Interpolate<T>
    {
        public static Func<T, T, double, T> Value { get; set; }

        static Interpolate() { Interpolate.EnsureInitialize(); }
    }

    public static class Interpolate
    {
        public static byte Lerp(byte x, byte y, double amount)
        {
            return (byte)(x * (1 - amount) + y * amount);
        }

        public static sbyte Lerp(sbyte x, sbyte y, double amount)
        {
            return (sbyte)(x * (1 - amount) + y * amount);
        }

        public static char Lerp(char x, char y, double amount)
        {
            return (char)(x * (1 - amount) + y * amount);
        }

        public static int Lerp(int x, int y, double amount)
        {
            return (int)(x * (1 - amount) + y * amount);
        }

        public static uint Lerp(uint x, uint y, double amount)
        {
            return (uint)(x * (1 - amount) + y * amount);
        }

        public static short Lerp(short x, short y, double amount)
        {
            return (short)(x * (1 - amount) + y * amount);
        }

        public static ushort Lerp(ushort x, ushort y, double amount)
        {
            return (ushort)(x * (1 - amount) + y * amount);
        }

        public static long Lerp(long x, long y, double amount)
        {
            return (long)(x * (1 - amount) + y * amount);
        }

        public static ulong Lerp(ulong x, ulong y, double amount)
        {
            return (ulong)(x * (1 - amount) + y * amount);
        }

        public static float Lerp(float x, float y, double amount)
        {
            return (float)(x * (1 - amount) + y * amount);
        }

        public static double Lerp(double x, double y, double amount)
        {
            return x * (1 - amount) + y * amount;
        }

        public static decimal Lerp(decimal x, decimal y, double amount)
        {
            return x * (decimal)(1 - amount) + y * (decimal)amount;
        }

        public static bool Lerp(bool x, bool y, double amount)
        {
            return amount < 0.5f ? x : y;
        }

        static bool initialized = false;

        internal static void EnsureInitialize()
        {
            if (!initialized) initialized = true;

            Interpolate<byte>.Value = new Func<byte, byte, double, byte>(Lerp);
            Interpolate<sbyte>.Value = new Func<sbyte, sbyte, double, sbyte>(Lerp);
            Interpolate<double>.Value = new Func<double, double, double, double>(Lerp);
            Interpolate<float>.Value = new Func<float, float, double, float>(Lerp);
            Interpolate<bool>.Value = new Func<bool, bool, double, bool>(Lerp);
            Interpolate<decimal>.Value = new Func<decimal, decimal, double, decimal>(Lerp);
            Interpolate<short>.Value = new Func<short, short, double, short>(Lerp);
            Interpolate<ushort>.Value = new Func<ushort, ushort, double, ushort>(Lerp);
            Interpolate<int>.Value = new Func<int, int, double, int>(Lerp);
            Interpolate<uint>.Value = new Func<uint, uint, double, uint>(Lerp);
            Interpolate<long>.Value = new Func<long, long, double, long>(Lerp);
            Interpolate<ulong>.Value = new Func<ulong, ulong, double, ulong>(Lerp);
            Interpolate<char>.Value = new Func<char, char, double, char>(Lerp);
        }
    }
}