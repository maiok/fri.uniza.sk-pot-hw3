using System;
using System.ServiceModel;
using System.Windows;
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

        public MainWindow()
        {
            InitializeComponent();

            _channelFactory = new DuplexChannelFactory<IChatService>(new ClientCallback(), "ChatServiceEndPoint");
            Server = _channelFactory.CreateChannel();

            Server.SendMessage("", "heureka", new byte[0]);

        }

        public void AppendMessage(Message message)
        {
            TextBoxMessageBoard.Text += message.Text + "\n";
        }

    }
}
