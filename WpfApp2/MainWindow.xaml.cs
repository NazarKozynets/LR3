using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Window messageWindow = new Window
            {
                Title = "Повідомлення",
                Width = 300,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            StackPanel panel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            TextBlock message = new TextBlock
            {
                Text = "Ясна річ, ніхто й не сумнівався",
                Margin = new Thickness(10)
            };

            Button okButton = new Button
            {
                Content = "OK",
                Width = 80,
                Margin = new Thickness(10)
            };
            okButton.Click += (s, args) => Application.Current.Shutdown();

            panel.Children.Add(message);
            panel.Children.Add(okButton);
            messageWindow.Content = panel;
            messageWindow.ShowDialog();
        }

        private void NoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (double.IsNaN(Canvas.GetLeft(NoButton)))
                Canvas.SetLeft(NoButton, 0);
            if (double.IsNaN(Canvas.GetTop(NoButton)))
                Canvas.SetTop(NoButton, 0);

            double currentX = Canvas.GetLeft(NoButton);
            double currentY = Canvas.GetTop(NoButton);

            double margin = 10; 
            double maxX = MainCanvas.ActualWidth - NoButton.ActualWidth - margin;
            double maxY = MainCanvas.ActualHeight - NoButton.ActualHeight - margin;

            double newX, newY;

            do
            {
                newX = random.NextDouble() * maxX;
                newY = random.NextDouble() * maxY;
            }
            while (Distance(currentX, currentY, newX, newY) < 50); 

            var duration = TimeSpan.FromSeconds(0.4);

            DoubleAnimation animX = new DoubleAnimation
            {
                To = newX,
                Duration = duration,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation animY = new DoubleAnimation
            {
                To = newY,
                Duration = duration,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };

            NoButton.BeginAnimation(Canvas.LeftProperty, animX);
            NoButton.BeginAnimation(Canvas.TopProperty, animY);
        }

        private double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}