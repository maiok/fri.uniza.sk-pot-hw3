using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Forms;
using SimpleChatLibrary;
using Message = SimpleChatLibrary.Message;

namespace SimpleChatClient
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static IChatService _server;

        public MainWindow()
        {
            InitializeComponent();

            var channelFactory = new DuplexChannelFactory<IChatService>(new ClientCallback(), "ChatServiceEndPoint");
            _server = channelFactory.CreateChannel();

            DataToUser = new List<string>();
        }

        private List<string> DataFromUser { get; set; }
        private List<string> DataToUser { get; set; }

        private List<Message> ListMessages { get; set; }
        private List<User> ListOnlineUsers { get; set; }

        // OBSLUHA GUI KOMPONENTOV *********************************************************

        // Metoda mi refreshne obe comboboxy zo zoznamom uzivatelov
        private void RefreshComboBoxUsers()
        {
            // Naplnim si pouzivatelov do comboboxu pre prihlasenie uzivatela
            DataFromUser = _server.GetUserNickNames();
            ComboBoxFromUser.ItemsSource = DataFromUser;
            // Naplnim si pouzivatelov do comboboxu pre cieloveho pouzivatela
            // Musel to urobit cez pomList, ako keby treba nastavit premennu cez set
            // aby ju mohol Binding vo WPF zachytit a tym refreshnut zoznam
            var usersPom = new List<string>();
            usersPom.Add("Všetci");
            usersPom.AddRange(_server.GetUserNickNames());
            DataToUser = usersPom;
            ComboBoxToUser.ItemsSource = DataToUser;
            ComboBoxToUser.SelectedIndex = 0; // Defaultne Vsetci
        }

        public void RefreshSettings()
        {
            //todo tu som chcel refreshnut nastavenia farieb a velkosti pisma
        }

        // OBSLUHA GUI KOMPONENTOV *********************************************************

        public void RefreshListMessages()
        {
            ListMessages = _server.GetMessages();
            ListViewMessages.ItemsSource = ListMessages;
        }

        public void RefreshListOnlineUsers()
        {
            ListOnlineUsers = _server.GetUsers();
            ListViewOnlineUsers.ItemsSource = ListOnlineUsers;
        }

        public void AppendMessage(Message message)
        {
        }

        private void ButtonSendText_Click(object sender, RoutedEventArgs e)
        {
            var toUser = ComboBoxToUser.Text.Trim() != "Všetci" ? ComboBoxToUser.Text : "";

            _server.SendMessage(toUser, TextBoxText.Text);
            TextBoxText.Clear();
            RefreshListMessages();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new UserRegistration(_server);
            dialog.ShowDialog();
            RefreshComboBoxUsers();
            RefreshListOnlineUsers();
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            var nickname = ComboBoxFromUser.Text;
            if (nickname.Trim() != "" && _server.LoginAs(nickname))
                TextBlockLogged.Text = nickname;
            else
                TextBlockLogged.Text = "";
        }

        private void ButtonSendImage_Click(object sender, RoutedEventArgs e)
        {
            var image = new OpenFileDialog();

            if (image.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var imagePath = image.FileName;
                var toUser = ComboBoxToUser.Text.Trim() != "Všetci" ? ComboBoxToUser.Text : "";

                // Povolene extensions
                switch (Path.GetExtension(imagePath))
                {
                    case ".jpg":
                        break;
                    case ".png":
                        break;
                    case ".jpeg":
                        break;
                    default:
                        imagePath = "";
                        break;
                }

                _server.SendImage(toUser, imagePath);
                RefreshListMessages();
            }
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SettingsWindow(_server);
            dialog.ShowDialog();
        }
    }
}