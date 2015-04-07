namespace Nine.Animation
{
    public class Spring2D : IAnimation
    {
        struct State { public Vector2 x, v; }
        struct Derivative { public Vector2 dx, dv; }

        private const double solverTimeStep = 10;

        private State state;

        public static double DefaultTension { get; set; } = (40.0 - 30.0) * 3.62 + 194.0;
        public static double DefaultFriction { get; set; } = (7.0 - 8.0) * 3.0 + 25.0;

        public double Tension { get; set; } = DefaultTension;
        public double Friction { get; set; } = DefaultFriction;

        public Vector2 Target { get; set; }

        public Vector2 Value
        {
            get { return state.x; }
            set { state.x = value; state.v = Vector2.Zero; IsActive = true; }
        }

        public bool IsActive { get; private set; } = true;

        public virtual bool Update(double deltaTime)
        {
            if (!IsActive) return false;

            while (deltaTime > solverTimeStep)
            {
                deltaTime -= solverTimeStep;
                Integrate(ref state, solverTimeStep);
            }

            Integrate(ref state, deltaTime);

            return false;
        }

        // http://gafferongames.com/game-physics/integration-basics/
        private void Integrate(ref State state, double dt)
        {
            var initial = new Derivative();

            var a = Evaluate(ref state, ref initial, 0.0);
            var b = Evaluate(ref state, ref a, dt * 0.5);
            var c = Evaluate(ref state, ref b, dt * 0.5);
            var d = Evaluate(ref state, ref c, dt);

            var dxdt = 1.0 / 6.0 * (a.dx + 2.0 * (b.dx + c.dx) + d.dx);
            var dvdt = 1.0 / 6.0 * (a.dv + 2.0 * (b.dv + c.dv) + d.dv);

            state.x += dxdt * dt;
            state.v += dvdt * dt;
        }

        private Derivative Evaluate(ref State initial, ref Derivative d, double dt)
        {
            var state = new State { x = initial.x + d.dx * dt, v = initial.v + d.dv * dt };
            return new Derivative { dx = initial.v + d.dv * dt, dv = Acceleration(ref state) };
        }

        private Vector2 Acceleration(ref State state) => Tension * (Target - state.x) - Friction * state.v;

        public void InheritFrom(IAnimation other)
        {
            var spring = other as Spring;
            if (spring != null)
            {
                Tension = spring.Tension;
                Friction = spring.Friction;
            }
        }
    }
}