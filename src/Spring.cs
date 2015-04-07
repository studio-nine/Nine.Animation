namespace Nine.Animation
{
    public class Spring : IAnimation
    {
        struct State { public double x, v; }
        struct Derivative { public double dx, dv; }

        private const double solverTimeStep = 10.0 / 1000;

        private State state;

        // http://facebook.github.io/rebound-js/docs/rebound.html
        public static double DefaultTension { get; set; } = 40.0;
        public static double DefaultFriction { get; set; } = 7.0;

        public double Tension
        {
            get { return (tension - 194.0) / 3.62 + 30.0; }
            set { tension = (value - 30.0) * 3.62 + 194.0; }
        }
        private double tension;

        public double Friction
        {
            get { return (friction - 25.0) / 3.0 + 8.0; }
            set { friction = (value - 8.0) * 3.0 + 25.0; }
        }
        private double friction;

        public double Target { get; set; }

        public double Value
        {
            get { return state.x; }
            set { state.x = value; state.v = 0.0; IsActive = true; }
        }

        public bool IsActive { get; private set; } = true;

        public Spring() { Tension = DefaultTension; Friction = DefaultFriction; }
        public Spring(double value) : this() { Reset(value); }

        public void Reset(double value)
        {
            Target = Value = value;
        }

        public virtual bool Update(double deltaTime)
        {
            if (!IsActive) return false;

            deltaTime *= 0.001; // Convert to seconds.

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

        private double Acceleration(ref State state) => tension * (Target - state.x) - friction * state.v;

        public void InheritFrom(IAnimation other)
        {
            var spring = other as Spring;
            if (spring != null)
            {
                tension = spring.tension;
                friction = spring.friction;
            }
        }
    }
}