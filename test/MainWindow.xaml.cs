namespace Nine.Animation.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Nine.Animation;

    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, Func<double, double>> easings = new Dictionary<string, Func<double, double>>
        {
            { nameof(Ease.Linear), Ease.Linear },
            { nameof(Ease.Quad), Ease.Quad },
            { nameof(Ease.Cubic), Ease.Cubic },
            { nameof(Ease.Quart), Ease.Quart },
            { nameof(Ease.Quint), Ease.Quint },
            { nameof(Ease.Sin), Ease.Sin },
            { nameof(Ease.Circular), Ease.Circular },
            { nameof(Ease.Exp), Ease.Exp },
            { nameof(Ease.Back), Ease.Back },
            { nameof(Ease.Bounce), Ease.Bounce },
            { nameof(Ease.Elastic), Ease.Elastic },
        };

        public MainWindow()
        {
            InitializeComponent();

            EasingList.ItemsSource = easings.Keys;
            EasingList.SelectionChanged += (sender, e) => Animate(Ball);

            MouseLeftButtonDown += async (sender, e) =>
            {
                // Ball.Tween().FadeTo(0.5);
                // Ball.Tween().FadeOut();
                
                // TODO: smooth tween using Spring
                await Ball.Tween().Duration(1000).Delay(200)
                          .MoveBy(e.GetPosition(Ball).X, e.GetPosition(Ball).Y)
                          .FadeIn().OnStart(() => Title += "+");

                await EasingList.TweenAll(t => t.FadeIn(), 50);

                // Ball.Tween().RotateBy(Math.PI);
                // Ball.Tween().SpinOnce();
                // Ball.Tween().Spin();
                // Ball.Tween().ScaleBy(1.5, 2.0);
            };

            var spring = new Spring();
            Ball.GetAnimatable().FrameTimer.OnTick(dt =>
            {
                spring.Update(dt);
                // Ball.GetAnimatable().Position = new Vector2(spring.Value, 0);
                return false;
            });

            MouseMove += (sender, e) =>
            {
                // spring.Target = e.GetPosition(this).X;
            };
        }

        private async void Animate(FrameworkElement target)
        {
            var easing = easings[EasingList.SelectedItem.ToString()];
            var repeat = Repeat.IsChecked.HasValue && Repeat.IsChecked.Value ? double.MaxValue : 1;
            var yoyo = Yoyo.IsChecked ?? false;

            await target.Tween().To(
                new Tween<double>(x => target.GetAnimatable().Position = new Vector2(x, 0))
                {
                    Ease = easing,
                    Repeat = repeat,
                    Yoyo = yoyo,
                    From = -300,
                    To = 300,
                });
        }
    }
}
