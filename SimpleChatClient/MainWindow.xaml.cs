using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using SimpleChatLibrary;

namespace SimpleChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static IChatService _server;
        private List<String> DataFromUser { get; set; }
        private List<String> DataToUser { get; set; }

        private List<Message> ListMessages { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var channelFactory = new DuplexChannelFactory<IChatService>(new ClientCallback(), "ChatServiceEndPoint");
            _server = channelFactory.CreateChannel();

            DataToUser = new List<string>();
        }

        // OBSLUHA GUI KOMPONENTOV *********************************************************

        // Metoda mi refreshne obe comboboxy zo zoznamom uzivatelov
        private void RefreshComboBoxUsers()
        {
            // Naplnim si pouzivatelov do comboboxu pre prihlasenie uzivatela
            DataFromUser = _server.GetUserNickNames();
            ComboBoxFromUser.ItemsSource = DataFromUser;
            // Naplnim si pouzivatelov do comboboxu pre cieloveho pouzivatela
            DataToUser.Add("Všetci");
            DataToUser.AddRange(_server.GetUserNickNames());
            ComboBoxToUser.ItemsSource = DataToUser;
            ComboBoxToUser.SelectedIndex = 0; // Defaultne Vsetci
        }

        // OBSLUHA GUI KOMPONENTOV *********************************************************

        public void RefreshListMessages()
        {
            //try
            //{
            ListMessages = _server.GetMessages();
            ListViewMessages.ItemsSource = ListMessages;
            //} catch (Exception) { }
        }

        public void AppendMessage(Message message)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _server.SendMessage("tulpass", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", new byte[0]);
            RefreshListMessages();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new UserRegistration(_server);
            dialog.ShowDialog();
            RefreshComboBoxUsers();
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            string nickname = ComboBoxFromUser.Text;
            if (nickname.Trim() != "" && _server.LoginAs(nickname))
            {
                TextBlockLogged.Text = nickname;
            }
            else
            {
                TextBlockLogged.Text = "";
            }
        }
    }
}
