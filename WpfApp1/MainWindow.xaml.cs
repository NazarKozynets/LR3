using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButtons_Click(object sender, RoutedEventArgs e)
        {
            ButtonsPanel.Children.Clear();

            if (!int.TryParse(FromTextBox.Text, out int from) ||
                !int.TryParse(ToTextBox.Text, out int to) ||
                !int.TryParse(StepTextBox.Text, out int step) ||
                step <= 0 || from > to)
            {
                MessageBox.Show("Please check the input data.");
                return;
            }

            for (int i = from; i <= to; i += step)
            {
                var button = new Button
                {
                    Content = i.ToString(),
                    Margin = new Thickness(3),
                    Padding = new Thickness(5),
                    Tag = i
                };

                button.Click += NumberButton_Click;
                ButtonsPanel.Children.Add(button);
            }
        }

        private void RemoveMultiples_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(MultipleTextBox.Text, out int multiple) || multiple == 0)
            {
                MessageBox.Show("Please enter a valid number.");
                return;
            }

            int removedCount = 0;

            for (int i = ButtonsPanel.Children.Count - 1; i >= 0; i--)
            {
                if (ButtonsPanel.Children[i] is Button button &&
                    int.TryParse(button.Content.ToString(), out int value) &&
                    value % multiple == 0)
                {
                    ButtonsPanel.Children.RemoveAt(i);
                    removedCount++;
                }
            }

            if (removedCount == 0)
            {
                MessageBox.Show("No buttons were removed.");
            }
        }

        private bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0 || n % 3 == 0) return false;

            int sqrtN = (int)Math.Sqrt(n);
            for (int i = 5; i <= sqrtN; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
            }
            return true;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Content.ToString(), out int number))
            {
                MessageBox.Show(IsPrime(number) ? $"{number} is a prime number." : $"{number} is a composite number.");
            }
        }
    }
}