using System.Collections.Generic;
using System.ServiceModel;

namespace SimpleChatLibrary
{
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IChatService
    {
        [OperationContract]
        List<Message> GetMessages();

        [OperationContract]
        void SendMessage(string toUser, string text, byte[] imageBytes);

        [OperationContract]
        bool RegisterUser(string nickname);

        [OperationContract]
        bool LoginAs(string nickname);

        [OperationContract]
        bool IsUserLogged();
    }
}
