namespace Nine.Animation
{
    public interface IAnimation
    {
        /// <summary>
        /// Updates the internal state of this animation with the delta time in milliseconds.
        /// </summary>
        bool Update(double deltaTime);
    }
}