using System.Windows;
using SimpleChatLibrary;

namespace SimpleChatClient
{
    /// <summary>
    ///     Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        private static IChatService _server;

        public UserRegistration(IChatService server)
        {
            _server = server;
            InitializeComponent();
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            var newUserName = TextBoxNewNickName.Text;
            // Najprv zistim, ci uz taky uzivatel existuje
            if (_server.GetUserByNickName(newUserName) != null)
            {
                MessageBox.Show("Takyto uzivatel uz existuje.");
            }
            // Ked nie, zaregistrujem ho
            else
            {
                _server.RegisterUser(newUserName);
                Close();
            }
        }

        private void ButtonCancelRegistration_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}