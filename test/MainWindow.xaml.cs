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
            await this.Animate().FadeIn();//.Duration(1000).Times(1);
        }
    }
}
