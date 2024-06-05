using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace hod_kostkami_Hrdy
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RollDice_Click(object sender, RoutedEventArgs e)
        {
            string selectedAnimation = ((ComboBoxItem)AnimationComboBox.SelectedItem).Content.ToString();

            AnimateDice(Die1, selectedAnimation);
            AnimateDice(Die2, selectedAnimation);
            AnimateDice(Die3, selectedAnimation);
            AnimateDice(Die4, selectedAnimation);
            AnimateDice(Die5, selectedAnimation);
            AnimateDice(Die6, selectedAnimation);
        }

        private void AnimateDice(Canvas canvas, string animationType)
        {
            Storyboard storyboard = null;

            switch (animationType)
            {
                case "Jednoduchá":
                    storyboard = (Storyboard)FindResource("SimpleAnimation");
                    break;
                case "Realistická":
                    storyboard = (Storyboard)FindResource("RealisticAnimation");
                    break;
                case "Šílená":
                    storyboard = (Storyboard)FindResource("CrazyAnimation");
                    break;
            }

            if (storyboard != null)
            {
                storyboard.Completed += (s, e) => DrawDie(canvas, random.Next(1, 7));
                storyboard.Begin(canvas);
            }
        }

        private void DrawDie(Canvas canvas, int value)
        {
            canvas.Children.Clear();

            // Common dot properties
            double dotRadius = 20;
            SolidColorBrush dotBrush = new SolidColorBrush(Colors.Black);

            // Helper function to create a dot
            Ellipse CreateDot(double x, double y) => new Ellipse
            {
                Width = dotRadius,
                Height = dotRadius,
                Fill = dotBrush,
                Margin = new Thickness(x - dotRadius / 2, y - dotRadius / 2, 0, 0)
            };

            double centerX = canvas.Width / 2;
            double centerY = canvas.Height / 2;
            double offset = canvas.Width / 4;

            // Draw dots based on die value
            if (value == 1 || value == 3 || value == 5)
            {
                canvas.Children.Add(CreateDot(centerX, centerY));
            }
            if (value >= 2)
            {
                canvas.Children.Add(CreateDot(centerX - offset, centerY - offset));
                canvas.Children.Add(CreateDot(centerX + offset, centerY + offset));
            }
            if (value >= 4)
            {
                canvas.Children.Add(CreateDot(centerX + offset, centerY - offset));
                canvas.Children.Add(CreateDot(centerX - offset, centerY + offset));
            }
            if (value == 6)
            {
                canvas.Children.Add(CreateDot(centerX - offset, centerY));
                canvas.Children.Add(CreateDot(centerX + offset, centerY));
            }
        }
    }
}


