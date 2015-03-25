namespace Nine.Animation.Test
{
    using System;
    using System.Windows;
    using Nine.Animation;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private async void Load()
        {
            await this.Animate().TweenBy(() => Ball.Animate().X, x => Ball.Animate().X = x, 100);

            // await this.Animate().FadeIn();//.Duration(1000).Times(1);
            // await this.Animate().FadeIn().Duration(1).MoveTo(100, 100).Easing(Easing.Cubic); // Concurrent
            // await this.Animate().TweenTo(100, (a, v) => a.X = v)
        }
    }
}
