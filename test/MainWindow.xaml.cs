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
            MouseLeftButtonDown += (sender, e) =>
            {
                // Ball.Animate().FadeOut();
                // Ball.Animate().MoveBy(e.GetPosition(Ball).X, e.GetPosition(Ball).Y);
                // Ball.Animate().RotateBy(180);
            };
        }

        private async void Animate(FrameworkElement target)
        {
            var easing = easings[EasingList.SelectedItem.ToString()];
            var repeat = Repeat.IsChecked.HasValue && Repeat.IsChecked.Value ? double.MaxValue : 1;
            var autoReverse = AutoReverse.IsChecked ?? false;

            await target.Animate()
                        .Tween(x => target.Animate().X = x, -300, 300)
                        .InOut().SetEasing(easing).SetRepeat(repeat).SetAutoReverse(autoReverse);
        }
    }
}
