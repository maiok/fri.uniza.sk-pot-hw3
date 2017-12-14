using System.ServiceModel;

namespace SimpleChatLibrary
{
    public interface IClient
    {
        // Musi tu byt nejaka metoda typu OperationContract aby to vobec skompilovalo
        [OperationContract]
        void ShowMessage(Message message);

    }
}
