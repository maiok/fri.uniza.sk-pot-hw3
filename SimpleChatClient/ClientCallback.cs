using System.ServiceModel;
using System.Windows;
using SimpleChatLibrary;

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