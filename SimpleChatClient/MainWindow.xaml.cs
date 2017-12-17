using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using SimpleChatLibrary;

namespace SimpleChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IChatService Server;
        private static DuplexChannelFactory<IChatService> _channelFactory;

        private List<Message> ListMessages { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _channelFactory = new DuplexChannelFactory<IChatService>(new ClientCallback(), "ChatServiceEndPoint");
            Server = _channelFactory.CreateChannel();
        }

        public void SetListMessages()
        {
            //try
            //{
            ListViewMessages.ItemsSource = ListMessages;
            //} catch (Exception) { }
        }

        public void AppendMessage(Message message)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Server.SendMessage("tulpass", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", new byte[0]);
            ListMessages = Server.GetMessages();
            SetListMessages();
            //AppendMessage(Server.GetLastInsertMessage());
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
