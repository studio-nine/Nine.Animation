namespace Nine.Animation
{
#if !PCL
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

#if TWEENER_PUBLIC
    public
#endif
    static class Tweener
    {
        private static readonly object nullObject = new object();
        private static readonly DependencyProperty animatableProperty = DependencyProperty.RegisterAttached("Animatable", typeof(Dictionary<object, IAnimatable2D>), typeof(Tweener), new PropertyMetadata(null));

        public static IAnimatable2D GetAnimatable(this FrameworkElement element, object channel = null)
        {
            return ((SpringAnimatable2D)GetSpringAnimatable(element, channel)).Animatable;
        }

        public static IAnimatable2D GetSpringAnimatable(this FrameworkElement element, object channel = null)
        {
            channel = channel ?? nullObject;
            var result = (IAnimatable2D)null;
            var resultByChannel = (Dictionary<object, IAnimatable2D>)element.GetValue(animatableProperty);
            if (resultByChannel == null) element.SetValue(animatableProperty, resultByChannel = new Dictionary<object, IAnimatable2D>());
            if (!resultByChannel.TryGetValue(channel, out result)) resultByChannel[channel] = result = new SpringAnimatable2D(new FrameworkElementAnimatable(element));
            return result;
        }

        public static TweenBuilder2D Spring(this FrameworkElement element, double tension = -1, double friction = -1, object channel = null)
        {
            var spring = (SpringAnimatable2D)GetSpringAnimatable(element, channel);
            if (tension >= 0) spring.Tension = tension;
            if (friction >= 0) spring.Friction = friction;

            var anim = (FrameworkElementAnimatable)spring.Animatable;
            anim.FrameTimer.Clear();
            anim.FrameTimer.OnTick(spring.Update);
            return new TweenBuilder2D(spring);
        }

        public static TweenBuilder2D Tween(this FrameworkElement element, object channel = null)
        {
            var anim = (FrameworkElementAnimatable)GetAnimatable(element, channel);
            anim.FrameTimer.Clear();
            return new TweenBuilder2D(anim);
        }

        public static TweenBuilder2D SpringAll(this ItemsControl items, Func<TweenBuilder2D, TweenBuilder2D> builder, double stagger = 0, double tension = -1, double friction = -1, object channel = null)
        {
            return SpringAll(Enumerable.Range(0, items.Items.Count).Select(i => items.ItemContainerGenerator.ContainerFromIndex(i)).OfType<FrameworkElement>(), builder, stagger, tension, friction, channel);
        }

        public static TweenBuilder2D TweenAll(this ItemsControl items, Func<TweenBuilder2D, TweenBuilder2D> builder, double stagger = 0, object channel = null)
        {
            return TweenAll(Enumerable.Range(0, items.Items.Count).Select(i => items.ItemContainerGenerator.ContainerFromIndex(i)).OfType<FrameworkElement>(), builder, stagger, channel);
        }

        public static TweenBuilder2D SpringAll(this Panel panel, Func<TweenBuilder2D, TweenBuilder2D> builder, double stagger = 0, double tension = -1, double friction = -1, object channel = null)
        {
            return SpringAll(panel.Children.OfType<FrameworkElement>(), builder, stagger, tension, friction, channel);
        }

        public static TweenBuilder2D TweenAll(this Panel panel, Func<TweenBuilder2D, TweenBuilder2D> builder, double stagger = 0, object channel = null)
        {
            return TweenAll(panel.Children.OfType<FrameworkElement>(), builder, stagger, channel);
        }

        public static TweenBuilder2D TweenAll(this IEnumerable<FrameworkElement> items, Func<TweenBuilder2D, TweenBuilder2D> builder, double stagger = 0, object channel = null)
        {
            double delay = 0.0;
            TweenBuilder2D result = null;
            foreach (var item in items)
            {
                result = builder(Tween(item, channel));
                var anim = result.Animation as Timeline;
                if (anim != null) anim.Delay += delay;
                delay += stagger;
            }
            return result ?? new TweenBuilder2D();
        }

        public static TweenBuilder2D SpringAll(this IEnumerable<FrameworkElement> items, Func<TweenBuilder2D, TweenBuilder2D> builder, double stagger = 0, double tension = -1, double friction = -1, object channel = null)
        {
            double delay = 0.0;
            TweenBuilder2D result = null;
            foreach (var item in items)
            {
                result = builder(Spring(item, tension, friction, channel));
                var anim = result.Animation as Timeline;
                if (anim != null) anim.Delay += delay;
                delay += stagger;
            }
            return result ?? new TweenBuilder2D();
        }

        class DispatchFrameTimer : FrameTimer
        {
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
            private Vector2 basePosition;
            private bool layoutUpdated = true;

            public DispatchFrameTimer FrameTimer { get; } = new DispatchFrameTimer();
            IFrameTimer IAnimatable.FrameTimer => FrameTimer;

            public FrameworkElementAnimatable(FrameworkElement e)
            {
                var transform = new TransformGroup();
                transform.Children.Add(scale = new ScaleTransform());
                transform.Children.Add(rotate = new RotateTransform());
                transform.Children.Add(translate = new TranslateTransform());

                this.e = e;
                this.e.RenderTransform = transform;
                this.e.LayoutUpdated += (a, b) => layoutUpdated = true;
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
                get { UpdateBasePosition(); return new Vector2 { X = translate.X + basePosition.X, Y = translate.Y + basePosition.Y }; }
                set { UpdateBasePosition(); translate.X = value.X - basePosition.X; translate.Y = value.Y - basePosition.Y; }
            }

            public Vector2 Scale
            {
                get { return new Vector2 { X = scale.ScaleX, Y = scale.ScaleY }; }
                set { scale.ScaleX = value.X; scale.ScaleY = value.Y; }
            }

            private void UpdateBasePosition()
            {
                if (layoutUpdated)
                {
                    var point = e.TransformToVisual((UIElement)VisualTreeHelper.GetParent(e)).Transform(new Point(0, 0));
                    basePosition.X = point.X - translate.X;
                    basePosition.Y = point.Y - translate.Y;
                    layoutUpdated = false;
                }
            }
        }
    }
#endif
}