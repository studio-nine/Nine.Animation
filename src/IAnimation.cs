namespace Nine.Animation
{
    public interface IAnimation
    {
        /// <summary>
        /// Advances the clock of this animation, returns true if the animations should stop.
        /// </summary>
        bool Update(double deltaTime);

        /// <summary>
        /// Inherit properties from another animation.
        /// </summary>
        void InheritFrom(IAnimation other);
    }
}