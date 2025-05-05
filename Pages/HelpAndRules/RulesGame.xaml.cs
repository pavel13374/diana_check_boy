using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Морской_бой.Pages.HelpAndRules
{
    /// <summary>
    /// Логика взаимодействия для RulesGame.xaml
    /// </summary>
    public partial class RulesGame : Page
    {
        public RulesGame()
        {
            InitializeComponent();
        }

        private void ExampleButton_Click(object sender, RoutedEventArgs e)
        {
            TextRules.Visibility = Visibility.Collapsed;
            Example.Visibility = Visibility.Visible;
            CreateField(ShipsPlayer);
            CreateField(PlayerHitsField);
        }

        
        // Для примера 
        public void CreateField(UniformGrid uniformGrid)
        {
            for (int i = 0; i<100; i++)
            {
                Button button = new Button();
                button.Width = 15;
                button.Height = 15;
                button.Background = Brushes.White;

                uniformGrid.Children.Add(button);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TextRules.Visibility = Visibility.Visible;
            Example.Visibility = Visibility.Collapsed;
            ShipsPlayer.Children.Clear();
            PlayerHitsField.Children.Clear();
        }

        private void BackMenuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
