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
        };

        public MainWindow()
        {
            InitializeComponent();

            EasingList.ItemsSource = easings.Keys;
            EasingList.SelectionChanged += (sender, e) => Animate(Ball);
            MouseLeftButtonDown += async (sender, e) => Animate(Ball);
        }

        private async void Animate(FrameworkElement target)
        {
            var easing = easings[EasingList.SelectedItem.ToString()];

            await target.Animate()
                        .TweenTo(x => target.Animate().X = x, -300, 300)
                        .InOut().SetEasing(easing).SetRepeat(5).SetAutoReverse(true);
        }
    }
}
