using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Морской_бой.Properties;

namespace Морской_бой
{
    public partial class App : Application
    {
        public string CurrentTheme { get; private set; } = "Dark";
        public int CurrentFontSize { get; private set; } = 14;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Загружаем сохранённые настройки или используем значения по умолчанию
            CurrentTheme = Settings.Default.Theme ?? "Dark";
            CurrentFontSize = Settings.Default.FontSize > 0 ?
                Settings.Default.FontSize : 14;

            // Применение настроек
            ApplyTheme(CurrentTheme);
            ApplyFontSize(CurrentFontSize);
        }

        public void ApplyTheme(string theme)
        {
            CurrentTheme = theme;

            if (theme == "Light")
            {
                // Светлая тема
                Resources["DarkGrayColor"] = Color.FromRgb(240, 240, 240);
                Resources["MediumGrayColor"] = Color.FromRgb(220, 220, 220);
                Resources["LightGrayColor"] = Color.FromRgb(200, 200, 200);
                Resources["TextColor"] = Color.FromRgb(51, 51, 51);
                Resources["AccentColor"] = Color.FromRgb(100, 149, 237);
            }
            else
            {
                // Темная тема (по умолчанию)
                Resources["DarkGrayColor"] = Color.FromRgb(51, 51, 51);
                Resources["MediumGrayColor"] = Color.FromRgb(85, 85, 85);
                Resources["LightGrayColor"] = Color.FromRgb(119, 119, 119);
                Resources["TextColor"] = Color.FromRgb(238, 238, 238);
                Resources["AccentColor"] = Color.FromRgb(136, 136, 136);
            }

            // Обновление всех кистей
            Resources["DarkGrayBrush"] = new SolidColorBrush((Color)Resources["DarkGrayColor"]);
            Resources["MediumGrayBrush"] = new SolidColorBrush((Color)Resources["MediumGrayColor"]);
            Resources["LightGrayBrush"] = new SolidColorBrush((Color)Resources["LightGrayColor"]);
            Resources["TextBrush"] = new SolidColorBrush((Color)Resources["TextColor"]);
            Resources["AccentBrush"] = new SolidColorBrush((Color)Resources["AccentColor"]);
        }

        public void ApplyFontSize(int fontSize)
        {
            CurrentFontSize = fontSize;

            // Обновляем стили
            UpdateStyleFontSize("MenuButton", fontSize);
            UpdateStyleFontSize("GameButton", fontSize);
            UpdateStyleFontSize("Text", fontSize - 1);
        }

        private void UpdateStyleFontSize(string styleKey, double fontSize)
        {
            var baseStyle = (Style)Resources[styleKey];
            var newStyle = new Style(baseStyle.TargetType, baseStyle);

            // Удаляем старый сеттер размера шрифта, если он есть
            newStyle.Setters.Add(new Setter(Control.FontSizeProperty, fontSize));

            Resources[styleKey] = newStyle;
        }
    }
}