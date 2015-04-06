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

                // TODO: animation multiple targets
                // TODO: stagger ???
                // TODO: attribte override
                // TODO: AnimateSmooth
                await Ball.Tween(/* TODO: channel name to override animation */)
                          .MoveBy(e.GetPosition(Ball).X, e.GetPosition(Ball).Y).Delay(1000)
                          .FadeIn();

                // Ball.Tween().RotateBy(Math.PI);
                // Ball.Tween().SpinOnce();
                // Ball.Tween().Spin();
                // Ball.Tween().ScaleBy(1.5, 2.0);
            };

            var spring = new Spring();

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

            await target.Tween().Tween(
                new Tween<double>(x => target.Tween().Target.Position = new Vector2(x, 0))
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
