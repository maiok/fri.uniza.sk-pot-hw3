using System.Reflection;
using System.Windows;
using System.Windows.Media;
using SimpleChatClient.Properties;
using SimpleChatLibrary;

namespace SimpleChatClient
{
    /// <summary>
    ///     Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private static IChatService _server;

        public SettingsWindow(IChatService server)
        {
            _server = server;
            InitializeComponent();

            // Nacitanie farieb do color pickerov
            // Zdroj youtube: https://www.youtube.com/watch?v=M4-ILr2LXhM
            ComboBoxFontColorPicker.ItemsSource = typeof(Colors).GetProperties();
            ComboBoxFontColorPicker.SelectedIndex = 0;

            ComboBoxBgColorPicker.ItemsSource = typeof(Colors).GetProperties();
            ComboBoxBgColorPicker.SelectedIndex = 0;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            var selectedFontColor = (Color) (ComboBoxFontColorPicker.SelectedItem as PropertyInfo).GetValue(1, null);
            var selectedBgColor = (Color) (ComboBoxBgColorPicker.SelectedItem as PropertyInfo).GetValue(1, null);

            Settings.Default.SettingBgPrivateMessage = selectedBgColor;
            Settings.Default.SettingFontColor = selectedFontColor;
        }
    }
}