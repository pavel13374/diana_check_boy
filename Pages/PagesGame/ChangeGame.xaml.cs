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
using Морской_бой.Pages.PagesGame.PagePrimaryGame;

namespace Морской_бой.Pages.PagesGame
{
    /// <summary>
    /// Логика взаимодействия для ChangeGame.xaml
    /// </summary>
    public partial class ChangeGame : Page
    {
        public ChangeGame()
        {
            InitializeComponent();
        }
        // Кнопка игры с другом 
        private void GameFriendButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameField());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
