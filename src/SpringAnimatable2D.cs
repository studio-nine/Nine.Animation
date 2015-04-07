namespace Nine.Animation
{
    using System;

    public class SpringAnimatable2D : IAnimatable2D
    {
        private readonly IAnimatable2D anim;
        private readonly Spring alpha;
        private readonly Spring rotation;
        private readonly Spring2D position;
        private readonly Spring2D scale;

        public IAnimatable2D Animatable => anim;

        public double Tension
        {
            get { return alpha.Tension; }
            set { alpha.Tension = rotation.Tension = position.Tension = scale.Tension = value; }
        }

        public double Friction
        {
            get { return alpha.Friction; }
            set { alpha.Friction = rotation.Friction = position.Friction = scale.Friction = value; }
        }

        public SpringAnimatable2D(IAnimatable2D animatable)
        {
            if (animatable == null) throw new ArgumentNullException(nameof(animatable));

            this.anim = animatable;
            this.alpha = new Spring(anim.Alpha);
            this.rotation = new Spring(anim.Rotation);
            this.position = new Spring2D(anim.Position);
            this.scale = new Spring2D(anim.Scale);
        }

        public bool Update(double dt)
        {
            alpha.Update(dt);
            rotation.Update(dt);
            position.Update(dt);
            scale.Update(dt);

            anim.Alpha = alpha.Value;
            anim.Rotation = rotation.Value;
            anim.Position = position.Value;
            anim.Scale = scale.Value;

            return false;
        }

        public IFrameTimer FrameTimer => anim.FrameTimer;

        public double Alpha
        {
            get { return alpha.Value; }
            set { alpha.Target = value; }
        }

        public double Rotation
        {
            get { return rotation.Value; }
            set { rotation.Target = value; }
        }

        public Vector2 Position
        {
            get { return position.Value; }
            set { position.Target = value; }
        }

        public Vector2 Scale
        {
            get { return scale.Value; }
            set { scale.Target = value; }
        }
    }
}