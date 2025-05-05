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

namespace Морской_бой.Pages.PagesGame.PagesPVP
{
    /// <summary>
    /// Логика взаимодействия для PVP_Page_Start.xaml
    /// </summary>
    public partial class PVP_Page_Start : Page
    {
        public PVP_Page_Start()
        {
            InitializeComponent();
            FirstFieldFull();
            SecondFieldFull();

        }
        public void FirstFieldFull()
        {
            for (int i = 0;i<100;i++)
            {
                Button button = new Button();
                button.Width = 20;
                button.Height = 20;
                button.BorderBrush = Brushes.Black;
                button.BorderThickness = new Thickness(5);
                button.Background = Brushes.White;

                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;

                FirstTable.Children.Add(button);
            }
        }
        // Убирание курсора
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            //Button button = (Button)sender;
            //button.BorderBrush = Brushes.Black;
        }
        // Становление курсора
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.BorderBrush = Brushes.Yellow;
        }

        public void SecondFieldFull()
        {
            for (int i = 0; i < 100; i++)
            {
                Button button = new Button();
                button.Width = 20;
                button.Height = 20;
                button.BorderBrush = Brushes.Black;
                button.Background = Brushes.White;


                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;

                SecondTable.Children.Add(button);
            }
        }
    }
}
