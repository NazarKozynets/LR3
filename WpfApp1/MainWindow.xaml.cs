using System;
using System.Diagnostics;
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
                MessageBox.Show("Перевірте правильність введення даних.");
                return;
            }

            for (int i = from; i <= to; i += step)
            {
                var btn = new Button
                {
                    Content = i.ToString(),
                    Margin = new Thickness(3),
                    Padding = new Thickness(5),
                    Tag = i
                };

                btn.Click += NumberButton_Click;

                ButtonsPanel.Children.Add(btn);
            }
        }

        private void RemoveMultiples_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(MultipleTextBox.Text, out int multiple) || multiple == 0)
            {
                MessageBox.Show("Введіть коректне число.");
                return;
            }

            int removedCount = 0;

            for (int i = ButtonsPanel.Children.Count - 1; i >= 0; i--)
            {
                if (ButtonsPanel.Children[i] is Button btn &&
                    int.TryParse(btn.Content.ToString(), out int value) &&
                    value % multiple == 0)
                {
                    ButtonsPanel.Children.RemoveAt(i);
                    removedCount++;
                }
            }

            if (removedCount == 0)
            {
                    MessageBox.Show("Жодної кннопок не було видалено");
            }
        }

        private bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Content.ToString(), out int number))
            {
                MessageBox.Show(IsPrime(number) ? $"{number} — це просте число." : $"{number} — це складене число.");
            }
        } 
    }
}
