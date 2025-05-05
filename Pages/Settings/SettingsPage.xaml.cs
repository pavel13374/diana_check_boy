using System.Windows;
using System.Windows.Controls;
using Морской_бой.Properties;

namespace Морской_бой.Pages.Settings
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            // Инициализация настроек перед загрузкой
            if (string.IsNullOrEmpty(Properties.Settings.Default.Theme))
            {
                Properties.Settings.Default.Theme = "Dark";
            }

            if (Properties.Settings.Default.FontSize <= 0)
            {
                Properties.Settings.Default.FontSize = 14;
            }

            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            var app = (App)Application.Current;

            // Загрузка темы
            DarkThemeRadio.IsChecked = app.CurrentTheme == "Dark";
            LightThemeRadio.IsChecked = app.CurrentTheme == "Light";

            // Загрузка размера шрифта
            foreach (ComboBoxItem item in FontSizeComboBox.Items)
            {
                if (item.Tag != null && item.Tag.ToString() == app.CurrentFontSize.ToString())
                {
                    FontSizeComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void ApplySettings_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что выбран элемент в ComboBox
            if (!(FontSizeComboBox.SelectedItem is ComboBoxItem selectedItem) || selectedItem.Tag == null)
            {
                MessageBox.Show("Пожалуйста, выберите размер шрифта");
                return;
            }

            // Применение новой темы
            string newTheme = DarkThemeRadio.IsChecked == true ? "Dark" : "Light";

            // Парсим размер шрифта
            if (!int.TryParse(selectedItem.Tag.ToString(), out int newFontSize))
            {
                MessageBox.Show("Некорректный размер шрифта");
                return;
            }

            // Применяем и сохраняем настройки
            var app = (App)Application.Current;
            app.ApplyTheme(newTheme);
            app.ApplyFontSize(newFontSize);

            // Сохранение в настройки приложения
            Properties.Settings.Default.Theme = newTheme;
            Properties.Settings.Default.FontSize = newFontSize;
            Properties.Settings.Default.Save();

            // Возврат на предыдущую страницу
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}