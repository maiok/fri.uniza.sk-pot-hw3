using System;
using System.ServiceModel;

namespace SimpleChatServer
{
    internal class ProgramServer
    {
        private static void Main(string[] args)
        {
            var selfHost = new ServiceHost(typeof(ChatService));
            try
            {
                selfHost.Open();
                Console.WriteLine("The service is ready. Press <ENTER> to terminate service.");
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}