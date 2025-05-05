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
using Морской_бой.Classes;

namespace Морской_бой.Pages.PagesGame.PagePrimaryGame
{
   

    public partial class GameField : Page
    {
        public int selectedship_length = 0; 
        public List<Ship> list_placedships = new List<Ship>(); 
        public bool isHorizontal = false; 
        Dictionary<int, int> count_ships = new Dictionary<int, int>() 
        {
            { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 }
        };

        public int selectedship_length2 = 0;
        public List<Ship> list_placedships2 = new List<Ship>();
        public bool isHorizontal2 = false;
        Dictionary<int, int> count_ships2 = new Dictionary<int, int>()
        {
            { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 }
        };

        private bool isFirstPlayerTurn = true; 
        private List<Ship> shipsToHit;
        private UniformGrid activeHitField;
        private List<Ship> enemyShips;
        private bool isGameOver = false;
        private int firstPlayerScore = 0;
        private int secondPlayerScore = 0;
        private bool hasMadeShot = false;

        public GameField()
        {
            InitializeComponent();
            CreateField(ShipsPlayer, list_placedships, 1);
            CreateField(ShipsSecondPlayer, list_placedships2, 2);

            SecondPlayerItem.Visibility = Visibility.Collapsed;
        }

        public void CreateField(UniformGrid uniformGrid, List<Ship> list_placedships, int player_id) 
        {
            for (int i = 0; i < 100; i++)
            {
                Button button = new Button();
                button.Width = 20;
                button.Height = 20;
                button.BorderThickness = new Thickness(1);
                button.Background = Brushes.White; 

                int x = i % 10; 
                int y = i / 10;

                Point coordinates = new Point(x, y); 
                button.Tag = coordinates; 

                if (player_id == 1) button.Click += Button_ClickFirst; 
                else if (player_id == 2) button.Click += Button_ClickSecond;

                uniformGrid.Children.Add(button);
            }
        }

        private void Button_ClickFirst(object sender, RoutedEventArgs e)
        {
            if (FirstReadyButton.Visibility == Visibility.Collapsed)
            {
                MessageBox.Show("Ничего поменять нельзя, все корабли расставлены!");
            }
            else
            {
                ButtonPlacement(sender, ShipsPlayer, list_placedships, count_ships,
                (bool)OneChoise.IsChecked, (bool)SecondChoise.IsChecked, (bool)ThreeChoise.IsChecked, (bool)FourChoise.IsChecked,
                (bool)HorizontalChoise.IsChecked, selectedship_length, 1);
            }
        }

        private void Button_ClickSecond(object sender, RoutedEventArgs e)
        {
            if (FirstReadyButton.Visibility == Visibility.Collapsed)
            {
                MessageBox.Show("Ничего поменять нельзя, все корабли расставлены!");
            }
            else
            {
                ButtonPlacement(sender, ShipsSecondPlayer, list_placedships2, count_ships2,
                (bool)OneChoised.IsChecked, (bool)SecondChoised.IsChecked, (bool)ThreeChoised.IsChecked, (bool)FourChoised.IsChecked,
                (bool)HorizontalChoise2.IsChecked, selectedship_length2, 2);
            }
        }

        private void ButtonPlacement(object sender, UniformGrid shipsPlayer, List<Ship> list_placedships, Dictionary<int, int> count_ships,
           bool oneChoiceIsChecked, bool secondChoiceIsChecked, bool threeChoiceIsChecked, bool fourChoiceIsChecked,
           bool horizontalChoiceIsChecked, int selectedship_length, int playerId) 
        {
            Button button = (Button)sender;
            Point coordinates = (Point)button.Tag;
            int x = (int)coordinates.X;
            int y = (int)coordinates.Y;

            if (oneChoiceIsChecked) selectedship_length = 1;
            else if (secondChoiceIsChecked) selectedship_length = 2;
            else if (threeChoiceIsChecked) selectedship_length = 3;
            else if (fourChoiceIsChecked) selectedship_length = 4;

            if (selectedship_length == 0) 
            {
                MessageBox.Show("Выберите размер корабля!");
                return;
            }

            if (count_ships[selectedship_length] <= 0)
            {
                MessageBox.Show($"{selectedship_length} - палубные корабли закончились(");
                return;
            }

            if (button.Background == Brushes.Blue)
            {
                MessageBox.Show("Эта кнопка уже выбрана!");
                return;
            }

            bool isHorizontal = horizontalChoiceIsChecked;

            if (!CanPlaceShip(x, y, selectedship_length, isHorizontal, list_placedships))
            {
                MessageBox.Show("Невозможно разместить корабль!");
                return;
            }

            for (int i = 0; i < selectedship_length; i++)
            {
                int currentX = isHorizontal ? x + i : x; 
                int currentY = isHorizontal ? y : y + i;

                Button cellButton = FindButtonByCoordinates(currentX, currentY, shipsPlayer);
                if (cellButton != null) 
                {
                    cellButton.Background = Brushes.Blue;

                    Ship ship = new Ship();
                    ship.coordinateX = currentX;
                    ship.coordinateY = currentY;
                    ship.ship_length = selectedship_length;
                    list_placedships.Add(ship);
                }
            }
            count_ships[selectedship_length]--;
        }

        private Button FindButtonByCoordinates(int x, int y, UniformGrid Ships) 
        {
            foreach (var item in Ships.Children)
            {
                Button button = item as Button;
                if (button != null)
                {
                    Point coordinates = (Point)button.Tag;
                    if ((int)coordinates.X == x && (int)coordinates.Y == y)
                    {
                        return button;
                    }
                }
            }
            return null;
        }

        private bool IsMinDistance(int x, int y, List<Ship> list_placed) 
        {
            for (int spectre_x = -1; spectre_x <= 1; spectre_x++)
            {
                for (int spectre_y = -1; spectre_y <= 1; spectre_y++)
                {
                    if (spectre_x == 0 && spectre_y == 0)
                    {
                        continue;
                    }

                    int now_button_x = x + spectre_x;
                    int now_button_y = y + spectre_y;

                    if (now_button_x >= 0 && now_button_x < 10 && now_button_y >= 0 && now_button_y < 10)
                    {
                        if (list_placed.Any(c => c.coordinateX == now_button_x && c.coordinateY == now_button_y))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool CanPlaceShip(int startX, int startY, int shipLength, bool isHorizontal, List<Ship> list_placed) 
        {
            if (isHorizontal && startX + shipLength > 10) return false; 
            if (!isHorizontal && startY + shipLength > 10) return false;

            for (int i = 0; i < shipLength; i++)
            {
                int currentX = isHorizontal ? startX + i : startX;  
                int currentY = isHorizontal ? startY : startY + i;

                if (IsMinDistance(currentX, currentY, list_placed)) 
                {
                    return false;
                }
            }
            return true;
        }
        private void FirstReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (list_placedships.Count < 20)
            {
                MessageBox.Show("Заполните ваше поле!");
            }
            else
            {
                PlayerItem.Visibility = Visibility.Collapsed;
                SecondPlayerItem.Visibility = Visibility.Visible; 
            }
        }

        public void CreateHitField(UniformGrid uniformGrid) 
        {
            for (int i = 0; i < 100; i++)
            {
                Button button = new Button();
                button.Width = 20;
                button.Height = 20;
                button.BorderThickness = new Thickness(1);
                button.Background = Brushes.White;

                int x = i % 10; 
                int y = i / 10;

                Point coordinates = new Point(x, y);
                button.Tag = coordinates; 

                button.Click += HitButton_Click; 

                uniformGrid.Children.Add(button);
            }
        }

        private void SecondReadyButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (list_placedships2.Count < 20)
            {
                MessageBox.Show("Заполните ваше поле!");
            }
            else
            {
                PlayerItem.Visibility = Visibility.Visible;
                CreateHitField(PlayerHitsField); 
                CreateHitField(PlayerHitsSecondField);
                this.Title = "Игра началась!"; 
                ChangeTypeShips.Visibility = Visibility.Collapsed;
                ChangeTypeShips2.Visibility = Visibility.Collapsed;
                FirstReadyButton.Visibility = Visibility.Collapsed;
                SecondReadyButton.Visibility = Visibility.Collapsed;
                PlayerHitsField.IsEnabled = true;
                PlayerHitsSecondField.IsEnabled = false;

                isFirstPlayerTurn = true; 
                activeHitField = PlayerHitsField; 
                shipsToHit = list_placedships2; 
                enemyShips = list_placedships2;  
                hasMadeShot = false; 
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); 
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            if (isGameOver)
            {
                return;
            }

            Button button = (Button)sender;

            if (button.Background != Brushes.White)
            {
                MessageBox.Show("Сюда ты уже стрелял!");
                return;
            }

            Point coordinates = (Point)button.Tag;
            int x = (int)coordinates.X;
            int y = (int)coordinates.Y;

            bool hit = false;
            Ship hitShip = null;
            foreach (Ship ship in shipsToHit.ToList()) 
            {
                if (ship.coordinateX == x && ship.coordinateY == y)
                {
                    hit = true;
                    hitShip = ship;
                    break;
                }
            }

            if (hit)
            {
                button.Background = Brushes.Green; 
                MessageBox.Show("Попал!"); 
                shipsToHit.Remove(hitShip); 
                hasMadeShot = true;
                UniformGrid enemy_field = null;

                if (isFirstPlayerTurn)
                {
                    firstPlayerScore++;
                    enemy_field = ShipsSecondPlayer;
                }
                else
                {
                    secondPlayerScore++;
                    enemy_field = ShipsPlayer;
                }

                Button enemy_button = FindButtonByCoordinates(x, y, enemy_field);
                enemy_button.Background = Brushes.Red;

                if (shipsToHit.Count == 0)
                {
                    string winner = null;
                    if(isFirstPlayerTurn==true)
                    {
                        winner = "Первый игрок";
                    }
                    else
                    {
                        winner = "Второй игрок";
                    }
                    MessageBox.Show($"{winner} победил! Счет: Первый игрок - {firstPlayerScore}, Второй игрок - {secondPlayerScore}");
                    isGameOver = true;
                    return;
                }

                return;
            }
            else
            {
                button.Background = Brushes.LightBlue; 
                MessageBox.Show("Мимо!");
                hasMadeShot = true; 
            }

            if (hasMadeShot)
            {
                SwitchTurns();
            }
        }

        private void SwitchTurns()
        {
            isFirstPlayerTurn = !isFirstPlayerTurn;

            activeHitField.IsEnabled = false;

            if (isFirstPlayerTurn) 
            {
                activeHitField = PlayerHitsField; 
                shipsToHit = list_placedships2;  
                enemyShips = list_placedships2;    
            }
            else 
            {
                activeHitField = PlayerHitsSecondField; 
                shipsToHit = list_placedships;  
                enemyShips = list_placedships;    
            }

            activeHitField.IsEnabled = true; 
            hasMadeShot = false; 
        }
    }
}
