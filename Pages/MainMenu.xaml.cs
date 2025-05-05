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
using Морской_бой.Pages.HelpAndRules;
using Морской_бой.Pages.PagesGame;


namespace Морской_бой.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        // Кнопка выхода 
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo,MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(); // Закрываем приложение, если нажали "Да"
            }
        }
        // Кнопка начала игры 
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeGame());
        }

        private void RuleGAmeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RulesGame());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Settings/SettingsPage.xaml", UriKind.Relative));
        }
    }
}
