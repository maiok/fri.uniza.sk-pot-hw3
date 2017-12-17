using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using SimpleChatClient;
using SimpleChatLibrary;

namespace SimpleChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        private readonly List<Message> _messages = new List<Message>();
        // Thread-Safe dictionary, musi byt nastavene ConcurrencyMode.Multiple
        private readonly ConcurrentDictionary<string, User> _registeredUsers = new ConcurrentDictionary<string, User>();
        private User _loggedUser;

        public List<Message> GetMessages()
        {
            return _messages;
        }

        public void SendMessage(string toUser, string text, byte[] imageBytes)
        {
            User user = new User();
            user.NickName = "tulpas";

            _loggedUser = user;

            Message message = new Message();
            message.FromUser = _loggedUser;
            message.ToUser = user;
            message.Text = text;
            message.ImageBytes = imageBytes;

            // Pridanie spravy do chatu
            _messages.Add(message);

            //var client = (ClientCallback) OperationContext.Current.GetCallbackChannel<IClient>();
            //client.ShowMessage(message);

            // Ak je viac ako 10 sprav, odstran stare spravy
            //while (_messages.Count > 10)
            //    _messages.RemoveAt(0);
        }

        public bool RegisterUser(string nickname)
        {
            User newUser = new User();
            newUser.Connection = OperationContext.Current.GetCallbackChannel<IClient>();
            newUser.NickName = nickname;

            return _registeredUsers.TryAdd(nickname, newUser);
        }

        public bool LoginAs(string nickname)
        {
            _loggedUser = GetUserByNickName(nickname);
            return _loggedUser != null;
        }

        public bool IsUserLogged()
        {
            return _loggedUser != null;
        }

        public User GetUserByNickName(string nickname)
        {
            foreach (var user in _registeredUsers)
            {
                if (user.Key.ToLower() == nickname.ToLower())
                {
                    return user.Value;
                }
            }

            return null;
        }

        public Message GetLastInsertMessage()
        {
            return _messages[0];
            //return null;
        }
    }

}
