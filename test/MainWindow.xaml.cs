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
            { nameof(Easing.Linear), new Func<double, double>(Easing.Linear) },
            { nameof(Easing.Quad), new Func<double, double>(Easing.Quad) },
            { nameof(Easing.Cubic), new Func<double, double>(Easing.Cubic) },
            { nameof(Easing.Quart), new Func<double, double>(Easing.Quart) },
            { nameof(Easing.Quint), new Func<double, double>(Easing.Quint) },
            { nameof(Easing.Sin), new Func<double, double>(Easing.Sin) },
            { nameof(Easing.Circular), new Func<double, double>(Easing.Circular) },
            { nameof(Easing.Exp), new Func<double, double>(Easing.Exp) },
            { nameof(Easing.Back), new Func<double, double>(Easing.Back) },
            { nameof(Easing.Bounce), new Func<double, double>(Easing.Bounce) },
            { nameof(Easing.Elastic), new Func<double, double>(Easing.Elastic) },
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
                await Ball.Animate(/* TODO: channel name to override animation */)
                          .MoveBy(e.GetPosition(Ball).X, e.GetPosition(Ball).Y)
                          .FadeIn();
                // Ball.Animate().RotateBy(Math.PI);
                // Ball.Animate().SpinOnce();
                // Ball.Animate().Spin();
                // Ball.Animate().ScaleBy(1.5, 2.0);
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
                    Easing = easing,
                    Repeat = repeat,
                    Yoyo = yoyo,
                    InOut = EaseInOut.InOut,
                    From = -300,
                    To = 300,
                });
        }
    }
}
