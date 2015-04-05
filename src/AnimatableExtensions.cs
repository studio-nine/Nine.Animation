namespace Nine.Animation
{
    using System;
    using System.ComponentModel;
#if WPF
    using System.Windows;
    using System.Windows.Media;
#endif

    [EditorBrowsable(EditorBrowsableState.Never)]
    static class AnimatableExtensions
    {
#if WPF
        private static readonly DependencyProperty animatableProperty = DependencyProperty.RegisterAttached("Animatable", typeof(IAnimatable2D), typeof(AnimatableExtensions));

        public static IAnimatable2D Animate(this FrameworkElement element, object channel = null)
        {
            var result = (IAnimatable2D)element.GetValue(animatableProperty);
            if (result != null) return result;
            element.SetValue(animatableProperty, result = new FrameworkElementAnimatable(element));
            return result;
        }

        class DispatchFrameTimer : FrameTimer
        {
            public static readonly DispatchFrameTimer Default = new DispatchFrameTimer();

            public DispatchFrameTimer()
            {
                CompositionTarget.Rendering += (sender, e) => UpdateFrame();
            }
        }

        class FrameworkElementAnimatable : IAnimatable2D
        {
            private FrameworkElement e;
            private ScaleTransform scale;
            private RotateTransform rotate;
            private TranslateTransform translate;

            public IFrameTimer FrameTimer
            {
                get { return DispatchFrameTimer.Default; }
            }

            public FrameworkElementAnimatable(FrameworkElement e)
            {
                var transform = new TransformGroup();
                transform.Children.Add(scale = new ScaleTransform());
                transform.Children.Add(rotate = new RotateTransform());
                transform.Children.Add(translate = new TranslateTransform());

                this.e = e;
                this.e.RenderTransform = transform;
            }

            public double Alpha
            {
                get { return (double)e.Opacity; }
                set { e.Opacity = value; }
            }

            public double Rotation
            {
                get { return Math.PI * rotate.Angle / 180; }
                set { rotate.Angle = value / Math.PI * 180; }
            }

            public Vector2 Position
            {
                get { return new Vector2 { X = translate.X, Y = translate.Y }; }
                set { translate.X = value.X; translate.Y = value.Y; }
            }

            public Vector2 Scale
            {
                get { return new Vector2 { X = scale.ScaleX, Y = scale.ScaleY }; }
                set { scale.ScaleX = value.X; scale.ScaleY = value.Y; }
            }
        }
#endif
    }
}