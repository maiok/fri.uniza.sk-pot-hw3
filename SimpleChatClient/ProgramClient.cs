using System;
using System.ServiceModel;
using SimpleChatLibrary;

namespace SimpleChatClient
{
    class ProgramClient
    {
        static void Main(string[] args)
        {
            // Zmenil som na duplex, pretoze potrebujem komunikovat obojsmerne
            // toto potom pojde do WPF MainWindow
            // ked sa vytvori server, vytvori sa nova instancia pre nastaveny endpoint, do ktoreho sa
            // pripoji instania klienta, cize vzdy ked budem chciet vytvorit klienta, musim vytvorit
            // novy DuplexChannelFactory
            using (var channelFactory = new DuplexChannelFactory<IChatService>(new ClientCallback(), "ChatServiceEndPoint"))
            {
                IChatService client = channelFactory.CreateChannel();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("USERLOGGED: " + client.IsUserLogged());
                    foreach (Message message in client.GetMessages())
                        Console.WriteLine(message);
                    Console.Write("Zadajte novú správu: ");
                    string text = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(text))
                        client.SendMessage("", text, new byte[0]);

                }
            }
        }
    }
}
