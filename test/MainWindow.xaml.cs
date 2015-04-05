namespace Nine.Animation.Test
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Nine.Animation;

    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, Func<double, double>> easings = new Dictionary<string, Func<double, double>>
        {
            { nameof(Ease.Linear), new Func<double, double>(Ease.Linear) },
            { nameof(Ease.Quad), new Func<double, double>(Ease.Quad) },
            { nameof(Ease.Cubic), new Func<double, double>(Ease.Cubic) },
            { nameof(Ease.Quart), new Func<double, double>(Ease.Quart) },
            { nameof(Ease.Quint), new Func<double, double>(Ease.Quint) },
            { nameof(Ease.Sin), new Func<double, double>(Ease.Sin) },
            { nameof(Ease.Circular), new Func<double, double>(Ease.Circular) },
            { nameof(Ease.Exp), new Func<double, double>(Ease.Exp) },
            { nameof(Ease.Back), new Func<double, double>(Ease.Back) },
            { nameof(Ease.Bounce), new Func<double, double>(Ease.Bounce) },
            { nameof(Ease.Elastic), new Func<double, double>(Ease.Elastic) },
        };

        public MainWindow()
        {
            InitializeComponent();

            EasingList.ItemsSource = easings.Keys;
            EasingList.SelectionChanged += (sender, e) => Animate(Ball);

            MouseLeftButtonDown += async (sender, e) =>
            {
                // Ball.Animate().FadeTo(0.5);
                // Ball.Animate().FadeOut();

                // TODO: attribte override
                // TODO: AnimateSmooth
                await Ball.Animate(/* TODO: channel name to override animation */)
                          .MoveBy(e.GetPosition(Ball).X, e.GetPosition(Ball).Y) // TODO: Delay
                          .FadeIn();

                // Ball.Animate().RotateBy(Math.PI);
                // Ball.Animate().SpinOnce();
                // Ball.Animate().Spin();
                // Ball.Animate().ScaleBy(1.5, 2.0);
            };

            var spring = new SpringAnimation();

            MouseMove += (sender, e) =>
            {
                // spring
            };
        }

        private async void Animate(FrameworkElement target)
        {
            var easing = easings[EasingList.SelectedItem.ToString()];
            var repeat = Repeat.IsChecked.HasValue && Repeat.IsChecked.Value ? double.MaxValue : 1;
            var yoyo = Yoyo.IsChecked ?? false;

            await target.Animate().Tween(
                new TweenAnimation<double>(x => target.Animate().Position = new Vector2(x, 0))
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
