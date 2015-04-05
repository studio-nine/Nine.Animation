namespace Nine.Animation
{
    public interface IAnimatable2D : IAnimatable
    {
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }

        double Alpha { get; set; }

        /// <summary>
        /// Gets or sets the rotation in radius.
        /// </summary>
        double Rotation { get; set; }
    }
}