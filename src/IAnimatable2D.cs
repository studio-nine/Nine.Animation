namespace Nine.Animation
{
    public interface IAnimatable2D : IAnimatable
    {
        double X { get; set; }
        double Y { get; set; }

        double ScaleX { get; set; }
        double ScaleY { get; set; }

        double Alpha { get; set; }

        /// <summary>
        /// Gets or sets the rotation in radius.
        /// </summary>
        double Rotation { get; set; }
    }
}