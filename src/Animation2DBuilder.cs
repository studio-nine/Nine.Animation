namespace Nine.Animation
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Animation2DBuilder : AnimationBuilder, IAnimatable2D
    {
        private readonly IAnimatable2D parent;

        public Animation2DBuilder(IAnimatable2D parent, IAnimation animation)
            : base(parent, animation)
        {
            var builder = parent as Animation2DBuilder;
            if (builder != null)
            {
                this.parent = builder.parent;
            }
            else
            {
                this.parent = parent;
            }
        }

        public double Alpha
        {
            get { return parent.Alpha; }
            set { parent.Alpha = value; }
        }

        public Vector2 Position
        {
            get { return parent.Position; }
            set { parent.Position = value; }
        }

        public Vector2 Scale
        {
            get { return parent.Scale; }
            set { parent.Scale = value; }
        }

        public double Rotation
        {
            get { return parent.Rotation; }
            set { parent.Rotation = value; }
        }
    }
}