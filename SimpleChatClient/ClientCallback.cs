using System;
using System.Net.Mime;
using System.ServiceModel;
using SimpleChatLibrary;
using System.Windows;
using SimpleChatApp;

namespace SimpleChatClient
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientCallback : IClient
    {
        public void ShowMessage(Message message)
        {
            if (Application.Current.MainWindow != null)
                ((MainWindow) Application.Current.MainWindow).AppendMessage(message);
        }
    }
}
